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
        private readonly Queue<BaseScreen> _screens = new();
        private Canvas _screenCanvas;

        public void Init(Canvas screenCanvas)
        {
            _screenCanvas = screenCanvas;
        }

        public void OpenWindow(BaseScreen baseScreenPrefab)
        {
            var baseScreen = Object.Instantiate(baseScreenPrefab, _screenCanvas.transform);
            _screens.Enqueue(baseScreen);
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