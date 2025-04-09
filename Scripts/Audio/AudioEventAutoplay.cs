using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Triggers;
using UnityEngine;

namespace AppCoreModule.Scripts.Audio
{
    [RequireComponent(typeof(AudioSource))]
    public class AudioEventAutoplay : MonoBehaviour
    {
        [SerializeField] private Behaviours _behaviour;
        [SerializeField] private AudioEvent _audioEvent;
        [SerializeField] private float _delay;
    
        private AudioSource _audioSource;
        private Collider _collider;
    
        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
            _collider = GetComponent<Collider>();
        }

        private void Start()
        {
            switch (_behaviour)
            {
                case Behaviours.OnStart:
                    Play();
                    break;
                case Behaviours.OnCollideOnce:
                    if (_collider == null) Debug.LogError("Collider not found. (Click for info)", gameObject);
                    CheckColliding();
                    break;
                case Behaviours.WithDelay:
                    PlayWithDelay().Forget();
                    break;
            }
        }

        private async void CheckColliding()
        {
            var trigger = this.GetAsyncCollisionEnterTrigger();
            var handler = trigger.GetOnCollisionEnterAsyncHandler();
            await handler.OnCollisionEnterAsync();
            Play();
        }

        private async UniTask PlayWithDelay()
        {
            await UniTask.WaitForSeconds(_delay);
        
            Play();
        }

        private void Play()
        {
            _audioEvent.Play(_audioSource);
        }

        private enum Behaviours
        {
            OnStart,
            OnCollideOnce,
            WithDelay,
        }
    }
}

