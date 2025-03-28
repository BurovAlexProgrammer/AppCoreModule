using Cysharp.Threading.Tasks;
using UnityEngine;

namespace AppCoreModule.Windows
{
    [RequireComponent(typeof(RectTransform))]
    public class BaseWindow : MonoBehaviour
    {
        private IWindowTransitEffect _openTransitEffect;
        private IWindowTransitEffect _closeTransitEffect;

        private bool _initialized;

        public void Init()
        {
            var rectTransform = GetComponent<RectTransform>();
            _openTransitEffect = new DefaultOpenWindowEffect(rectTransform);
            _closeTransitEffect = new DefaultCloseWindowEffect(rectTransform);
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