#if AppCoreTest
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
    /// <summary>
    /// Generates context menu for creating services 
    /// </summary>
    [InitializeOnLoad]
    public static class ServiceMenuGenerator
    {
        private const string MenuFilePath = "Assets/AppCoreModule/Scripts/Services/Editor/GeneratedServiceMenu.cs";
        private const string CompiledWithErrorPrefsKey = "ServiceMenuGenerator_CompiledWithError";
        private const string CompileErrorMessagePrefsKey = "ServiceMenuGenerator_CompiledErrorMessage";
        private const string GeneratedCodePrefsKey = "ServiceMenuGenerator_GeneratedCode";

        private static bool CompiledWithError
        {
            get => PlayerPrefs.GetInt(CompiledWithErrorPrefsKey, 0) == 1;
            set => PlayerPrefs.SetInt(CompiledWithErrorPrefsKey, value ? 1 : 0);
        }
        
        static ServiceMenuGenerator()
        {
            CompilationPipeline.assemblyCompilationFinished += OnAssemblyCompilationFinished;
        }

        private static void OnAssemblyCompilationFinished(string assemblyPath, CompilerMessage[] messages)
        { 
            if (messages.Any(x => x.type == CompilerMessageType.Error))
            {
                Debug.Log($"Сборка {assemblyPath} завершилась с ошибками.");
                File.Delete(MenuFilePath);
                CompiledWithError = true; 
                PlayerPrefs.SetString(CompileErrorMessagePrefsKey, messages.First(x => x.type == CompilerMessageType.Error).message);
            }
            else 
            {
                Debug.Log($"Сборка {assemblyPath} успешно скомпилирована.");
                PlayerPrefs.SetString(CompileErrorMessagePrefsKey, null);
                PlayerPrefs.SetString(GeneratedCodePrefsKey, null);
            }
        }

        [DidReloadScripts]
        private static void GenerateServiceMenu()
        {
            if (CompiledWithError)
            {
                CompiledWithError = false;
                Debug.LogError(PlayerPrefs.GetString(CompileErrorMessagePrefsKey));
                Debug.LogError($"ServiceMenuGenerator: Generated code: \n{PlayerPrefs.GetString(GeneratedCodePrefsKey)}");
                Debug.Log("ServiceMenuGenerator: generated file was removed.");
                return;
            }

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

            PlayerPrefs.SetString(GeneratedCodePrefsKey, code);
            return code;
        }

        // [MenuItem("GameObject/Services/serviceType")]
        // public static void serviceType_MenuItem()
        // {
        //     var serviceType = typeof(ScreenService);
        //     GameObject obj = new GameObject(serviceType.Name);
        //     obj.AddComponent(serviceType);
        //     Selection.activeGameObject = obj;
        //     Undo.RegisterCreatedObjectUndo(obj, "Create " + serviceType.Name);
        // }

        private static string GenerateServiceMenu(Type serviceType)
        {
            var codePart = $@"
            [MenuItem(""GameObject/Services/{serviceType.Name}"")]
            public static void {serviceType.Name}_MenuItem(){{
                var instance = new GameObject(""{serviceType.Name}"");
                instance.AddComponent<{serviceType.Name}>();
                Selection.activeGameObject = instance;
                Undo.RegisterCreatedObjectUndo(instance, ""Create {serviceType.Name}""); 
            }}";

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
#endif