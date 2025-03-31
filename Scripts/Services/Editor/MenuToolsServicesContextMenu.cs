using System;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace AppCoreModule.Scripts.Services.Editor
{
    public static class MenuToolsServicesContextMenu
    {
        private const string menuPath = "GameObject/Services/";

        // Ищем все классы, унаследованные от BaseService
        [InitializeOnLoadMethod]
        public static void RegisterServices()
        {
            var baseType = typeof(BaseService);
            var serviceTypes = Assembly.GetAssembly(baseType)
                .GetTypes()
                .Where(t => t.IsClass && !t.IsAbstract && baseType.IsAssignableFrom(t));

            foreach (var serviceType in serviceTypes)
            {
                string path = menuPath + serviceType.Name; // Создаём путь в меню
                MenuCommandService.AddDynamicMenuItem(path, () => CreateService(serviceType));
            }
        }

        private static void CreateService(Type serviceType)
        {
            GameObject obj = new GameObject(serviceType.Name);
            obj.AddComponent(serviceType);
            Selection.activeGameObject = obj;
            Undo.RegisterCreatedObjectUndo(obj, "Create " + serviceType.Name);
            Debug.Log(serviceType.Name + " создан в сцене!");
        }
    }

// Вспомогательный класс для динамического добавления пунктов меню
    public static class MenuCommandService
    {
        public static void AddDynamicMenuItem(string path, Action action)
        {
            var method = typeof(MenuItem).GetMethod("AddMenuItemWithFunction", BindingFlags.NonPublic | BindingFlags.Static);
            method?.Invoke(null, new object[] { path, false, action });
        }
    }
}