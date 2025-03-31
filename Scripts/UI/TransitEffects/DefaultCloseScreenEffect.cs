using System;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace AppCoreModule.Scripts.UI.TransitEffects
{
    public class DefaultCloseScreenEffect : ITransitEffect
    {
        private Action<RectTransform> _effectAction;
        private readonly RectTransform _rectTransform;

        private const float Time = 0.25f; 

        public DefaultCloseScreenEffect(RectTransform rectTransform)
        {
            _rectTransform = rectTransform;
        }

        public async UniTask RunAsync()
        {
            _rectTransform.localScale = Vector3.one;
            _rectTransform.DOScale(Vector2.zero, Time).SetEase(Ease.InOutCubic);
        }
    }
}