using System;
using System.Collections.Generic;
using AppCoreModule.Scripts.UI.Screens;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using Object = UnityEngine.Object;

namespace AppCoreModule.Scripts.Services
{
    public class ScreenService: BaseService, IDisposable
    {
        [SerializeField] private Canvas _screenCanvas;
        [SerializeField] private Canvas _popupCanvas;
        [SerializeField] private CanvasGroup _fadeFrame;

        private readonly Stack<BaseScreen> _screens = new();
        private bool _fadeInOnAwake;

        public void Init(bool fadeInOnAwake)
        {
            _fadeInOnAwake = fadeInOnAwake;
            FadeInOnAwake();
        }

        public void OpenWindow(BaseScreen baseScreenPrefab)
        {
            var baseScreen = Object.Instantiate(baseScreenPrefab, _screenCanvas.transform);
            _screens.Push(baseScreen);
            baseScreen.Init();
            baseScreen.Open().Forget();
        }

        public void GoBack()
        {
            
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