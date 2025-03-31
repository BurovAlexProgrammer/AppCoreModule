using AppCoreModule.Scripts.UI.TransitEffects;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace AppCoreModule.Scripts.UI.Screens
{
    [RequireComponent(typeof(RectTransform))]
    public class BaseScreen : MonoBehaviour
    {
        private ITransitEffect _openTransitEffect;
        private ITransitEffect _closeTransitEffect;

        private bool _initialized;

        public void Init()
        {
            var rectTransform = GetComponent<RectTransform>();
            _openTransitEffect = new DefaultOpenScreenEffect(rectTransform);
            _closeTransitEffect = new DefaultCloseScreenEffect(rectTransform);
            _initialized = true;
        }

        public async UniTask Open()
        {
            if (!_initialized)
            {
                Debug.LogError($"Window [{gameObject.name}] is not initialized before open.");
                return;
            }
            
            await _openTransitEffect.RunAsync();
        }

        public async UniTask Close()
        {
            if (!_initialized)
            {
                Debug.LogError($"Window [{gameObject.name}] is not initialized before close.");
                return;
            }
            
            await _closeTransitEffect.RunAsync();
        }
    }
}