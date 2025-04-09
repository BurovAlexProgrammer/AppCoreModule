using AppCoreModule.Scripts.UI.TransitEffects.Settings;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace AppCoreModule.Scripts.UI.Screens
{
    [RequireComponent(typeof(RectTransform))]
    public class BaseScreen : MonoBehaviour
    {
        protected TransitEffectSettings _transitEffectSettings;
        
        private bool _initialized;

        public void BaseInit(TransitEffectSettings transitEffectSettings)
        {
            _transitEffectSettings = transitEffectSettings;
            Init();
            _initialized = true;
        }

        protected virtual void Init()
        {
        }

        public async UniTask Open()
        {
            if (!_initialized)
            {
                Debug.LogError($"Window [{gameObject.name}] is not initialized before open.");
                return;
            }
            
            gameObject.SetActive(true);
            await _transitEffectSettings.OpenScreenEffect.RunAsync(gameObject);
        }

        public async UniTask Close()
        {
            if (!_initialized)
            {
                Debug.LogError($"Window [{gameObject.name}] is not initialized before close.");
                return;
            }
            
            await _transitEffectSettings.CloseScreenEffect.RunAsync(gameObject);
            gameObject.SetActive(false);
        }
    }
}