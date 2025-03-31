using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEditor.Compilation;
using UnityEngine;
using Assembly = System.Reflection.Assembly;

namespace AppCoreModule.Scripts.Services.Editor
{
    public class ServiceMenuGenerator
    {
        private const string MenuFilePath = "Assets/AppCoreModule/Scripts/Services/Editor/GenerateServiceMenu.cs";

        [InitializeOnLoad]
        public static class PreCompilationRunner
        {
            // Статический конструктор вызывается сразу при загрузке скриптов в редакторе
            static PreCompilationRunner()
            {
                // CompilationPipeline.compilationStarted += OnCompilationStarted;
                CompilationPipeline.assemblyCompilationFinished += OnAssemblyCompilationFinished;
            }

            private static void OnAssemblyCompilationFinished(string assemblyPath, CompilerMessage[] messages)
            {
                bool hasErrors = false;
                foreach (var message in messages) 
                {
                    if (message.type == CompilerMessageType.Error)
                    {
                        hasErrors = true;
                        break;
                    }
                }

                if (hasErrors)
                {
                    Debug.Log("Сборка " + assemblyPath + " завершилась с ошибками. Вызывается метод для обработки ошибок.");
                    File.Delete(MenuFilePath);
                }
                else
                {
                    Debug.Log("Сборка " + assemblyPath + " успешно скомпилирована.");
                }
            }
        }
    
        [DidReloadScripts]
        private static void GenerateServiceMenu()
        {
            var serviceTypes = GetAllServiceTypes();
            var code = GenerateCode(serviceTypes);

            File.WriteAllText(MenuFilePath, code);
            AssetDatabase.Refresh();
        }

        private static List<Type> GetAllServiceTypes()
        {
            var allTypes = Assembly.GetAssembly(typeof(BaseService)).GetTypes()
                .Where(type => type.IsSubclassOf(typeof(BaseService)) && !type.IsAbstract)
                .ToList();

            return allTypes;
        }

        private static string GenerateCode(List<Type> serviceTypes)
        {
            var code = GenerateUsings(serviceTypes);
            code += @"
            using UnityEngine;
            using UnityEditor;

            public static class GeneratedServiceMenu
            {";

            foreach (var service in serviceTypes)
            {
                code += GenerateServiceMenu(service) + "\n\n";
            }

            code += "}";

            return code;
        }

        [MenuItem("GameObject/Services/serviceType")]
        public static void serviceType_MenuItem()
        {
            var serviceType = typeof(ScreenService);
            GameObject obj = new GameObject(serviceType.Name);
            obj.AddComponent(serviceType);
            Selection.activeGameObject = obj;
            Undo.RegisterCreatedObjectUndo(obj, "Create " + serviceType.Name);
        }

        private static string GenerateServiceMenu(Type serviceType)
        {
            var codePart = $@"
            [MenuItem(""GameObject/Services/ServicesList"")]
            public static void {serviceType.Name}_MenuItem(){{";
            var instance = new GameObject(serviceType.Name);
            instance.AddComponent(serviceType);
            Selection.activeGameObject = instance;
            Undo.RegisterCreatedObjectUndo(instance, "Create" + serviceType.Name);  
                
            codePart += "}";

            return codePart;
        }

        private static string GenerateUsings(IEnumerable<Type> serviceTypes)
        {
            var result = new StringBuilder();
        
            foreach (var serviceType in serviceTypes)
            {
                result.Append($"using {serviceType.Namespace};");
            }

            return result.ToString();
        }
    }
}