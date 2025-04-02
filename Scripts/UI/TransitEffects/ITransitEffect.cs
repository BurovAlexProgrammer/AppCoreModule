using Cysharp.Threading.Tasks;
using UnityEngine;

namespace AppCoreModule.Scripts.UI.TransitEffects
{
    public interface ITransitEffect
    {
        public UniTask RunAsync(GameObject targetGameObject);
    }
}