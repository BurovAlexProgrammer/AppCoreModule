using System;
using System.Collections.Generic;
using AppCoreModule.Scripts.UI.Screens;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Object = UnityEngine.Object;

namespace AppCoreModule.Scripts.Services
{
    public class ScreenService: BaseService, IDisposable
    {
        [SerializeField] private Canvas _screenCanvas;
        
        private readonly Stack<BaseScreen> _screens = new();

        public void Init(Canvas screenCanvas)
        {
            _screenCanvas = screenCanvas;
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
    }
}