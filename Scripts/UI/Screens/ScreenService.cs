using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace AppCoreModule.Scripts.UI.Screens
{
    public class ScreenService
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
    }
}