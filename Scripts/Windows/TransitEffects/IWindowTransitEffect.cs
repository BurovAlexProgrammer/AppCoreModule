using Cysharp.Threading.Tasks;

namespace AppCoreModule.Windows
{
    public interface IWindowTransitEffect
    {
        public UniTask RunAsync();
    }
}