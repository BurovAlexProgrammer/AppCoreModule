using System.Collections.Generic;
using Cysharp.Threading.Tasks;

namespace AppCoreModule.Windows
{
    public class WindowService
    {
        private readonly Queue<BaseWindow> _windows = new();

        public void Init()
        {
        }

        public void OpenWindow(BaseWindow baseWindow)
        {
            _windows.Enqueue(baseWindow);
            baseWindow.Init();
            baseWindow.Open().Forget();
        }

        public void GoBack()
        {
            
        }
    }
}