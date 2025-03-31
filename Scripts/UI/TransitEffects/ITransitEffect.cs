using Cysharp.Threading.Tasks;

namespace AppCoreModule.Scripts.UI.TransitEffects
{
    public interface ITransitEffect
    {
        public UniTask RunAsync();
    }
}