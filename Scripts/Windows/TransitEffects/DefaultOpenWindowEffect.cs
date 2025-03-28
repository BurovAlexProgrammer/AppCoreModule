using System;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace AppCoreModule.Windows
{
    public class DefaultOpenWindowEffect : IWindowTransitEffect
    {
        private Action<RectTransform> _effectAction;
        private readonly RectTransform _rectTransform;

        private const float Time = 0.25f; 

        public DefaultOpenWindowEffect(RectTransform rectTransform)
        {
            _rectTransform = rectTransform;
        }

        public async UniTask RunAsync()
        {
            _rectTransform.localScale = Vector3.zero;
            await _rectTransform.DOScale(Vector2.one, Time).SetEase(Ease.InOutCubic).AsyncWaitForCompletion();
        }
    }
}