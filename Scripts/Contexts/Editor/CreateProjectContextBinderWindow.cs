#if UNITY_EDITOR

using System.IO;
using AppCoreModule.Scripts.Contexts.Const;
using UnityEditor;
using UnityEngine;

namespace AppCoreModule.Scripts.Contexts.Editor
{
    public static class MenuToolsProjectContextCreator
    {
        [MenuItem("Tools/AppCore/Create ProjectContext")]
        private static void CreateProjectContext()
        {
            // Проверяем, существует ли папка Resources
            if (!AssetDatabase.IsValidFolder(Common.folderPath))
            {
                AssetDatabase.CreateFolder("Assets", "Resources");
                Debug.Log("Создана папка Resources");
            }
            
            if (File.Exists(Common.assetPath))
            {
                Debug.LogWarning("ProjectContext уже существует в Resources!");
                OpenPrefab(Common.assetPath); // Открываем уже существующий префаб
                return;
            }

            // Проверяем, существует ли уже такой префаб
            if (File.Exists(Common.assetPath))
            {
                Debug.LogWarning("ProjectContext уже существует в Resources!");
                return;
            }

            // Создаём новый объект
            var projectContext = new GameObject("ProjectContext");
            projectContext.AddComponent<ProjectContext>(); // Добавляем нужный компонент

            // Создаём префаб в папке Resources
            PrefabUtility.SaveAsPrefabAsset(projectContext, Common.assetPath);
            Object.DestroyImmediate(projectContext); // Удаляем временный объект из сцены

            Debug.Log("ProjectContext создан в Resources: " + Common.assetPath);
            OpenPrefab(Common.assetPath);
        }
        
        
        private static void OpenPrefab(string path)
        {
            GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(path);
            if (prefab != null)
            {
                AssetDatabase.OpenAsset(prefab); // Открываем в Prefab Mode
                Debug.Log("Открыт префаб: " + path);
            }
            else
            {
                Debug.LogError("Не удалось открыть префаб: " + path);
            }
        }
    }
}

#endif
