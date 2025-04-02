using System;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace AppCoreModule.Scripts.UI.TransitEffects
{
    public class DefaultCloseScreenEffect : ITransitEffect
    {
        private const float Time = 0.25f;

        public async UniTask RunAsync(GameObject targetGameObject)
        {
            var rectTransform = targetGameObject.GetComponent<RectTransform>();
            rectTransform.localScale = Vector3.one;
            await rectTransform.DOScale(Vector2.zero, Time).SetEase(Ease.InOutCubic).AsyncWaitForCompletion();
        }
    }
}