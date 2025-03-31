using AppCoreModule.Scripts.Services;
using UnityEngine;

namespace AppCoreModule.Scripts.Contexts
{
    public static class ProjectContextInitializer
    {
        private static GameObject _projectContext;

        [RuntimeInitializeOnLoadMethod]
        public static void Initialize()
        {
            _projectContext = Object.Instantiate(new GameObject("ProjectContext"));
            _projectContext.AddComponent<ProjectContextBehavior>();
            Debug.Log("ProjectContext initialized.");
        }
    }

    public class ProjectContextBehavior : MonoBehaviour
    {
        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
            var projectContextPrefab = Resources.Load<ProjectContext>("ProjectContext");
            var instance = Instantiate(projectContextPrefab);
            var projectContext = instance.GetComponent<ProjectContext>();
        }

        private void Start()
        {
            
        }

        private void OnDestroy()
        {
            ServiceLocator.Clean();
        }
    }
}