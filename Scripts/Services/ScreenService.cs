using System;
using System.Collections.Generic;
using AppCoreModule.Scripts.UI.Screens;
using AppCoreModule.Scripts.UI.TransitEffects;
using AppCoreModule.Scripts.UI.TransitEffects.Settings;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace AppCoreModule.Scripts.Services
{
    public class ScreenService: BaseService, IDisposable
    {
        [SerializeField] protected Canvas _screenCanvas;
        [SerializeField] protected Canvas _popupCanvas;
        [SerializeField] private CanvasGroup _fadeFrame;

        private readonly Stack<BaseScreen> _screens = new();
        private TransitEffectSettings _transitEffectSettings;
        private BaseScreen _currentScreen;
        private bool _fadeInOnAwake;

        public void Init(bool fadeInOnAwake, TransitEffectSettings transitEffectSettings = default)
        {
            _transitEffectSettings = transitEffectSettings;

            if (transitEffectSettings == null)
            {
                _transitEffectSettings = new TransitEffectSettings()
                {
                    OpenScreenEffect = new DefaultOpenScreenEffect(),
                    CloseScreenEffect = new DefaultCloseScreenEffect()
                };
            }
            _fadeInOnAwake = fadeInOnAwake;
            FadeInOnAwake();
        }

        public void OpenScreen(BaseScreen baseScreenPrefab)
        {
            OpenScreenAsync(baseScreenPrefab).Forget();
        }
        
        public async UniTask OpenScreenAsync(BaseScreen baseScreenPrefab)
        {
            if (_currentScreen != null)
            {
                _screens.Push(_currentScreen);
                await _currentScreen.Close();
            }
            
            var baseScreen = InstantiateScreen(baseScreenPrefab);
            baseScreen.Init(_transitEffectSettings);
            await baseScreen.Open();
            _currentScreen = baseScreen;
        }

        protected virtual BaseScreen InstantiateScreen(BaseScreen screenPrefab)
        {
            var baseScreen = Instantiate(screenPrefab, _screenCanvas.transform);
            
            return baseScreen;
        }
        
        public void GoBack()
        {
            GoBackAsync().Forget();
        }
        
        public async UniTask GoBackAsync()
        {
            if (_screens.Count >= 1)
            {
                await _currentScreen.Close();
                var prevScreen = _screens.Pop();
                Destroy(_currentScreen.gameObject);
                _currentScreen = prevScreen;
                await prevScreen.Open();
            }
            else
            {
                Debug.LogWarning("ScrrenService: cannot go back. The last screen.");
            }
        }

        public void Dispose()
        {
        }

        private void FadeInOnAwake()
        {
            if (_fadeFrame == null) return;
            
            if (_fadeInOnAwake)
            {
                _fadeFrame.blocksRaycasts = true;
                _fadeFrame.alpha = 1f;
                _fadeFrame
                    .DOFade(0f, 0.5f)
                    .OnComplete(() => _fadeFrame.blocksRaycasts = false);
            }
            else
            {
                _fadeFrame.alpha = 0f;
                _fadeFrame.blocksRaycasts = false;
            }
        }
    }
}