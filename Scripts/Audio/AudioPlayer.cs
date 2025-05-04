using MyBox;
using UnityEngine;

namespace AppCoreModule.Scripts.Audio
{
    internal class AudioPlayer : MonoBehaviour
    {
        [SerializeField] private AudioSource _audioSource;
        [SerializeField, ReadOnly] private bool _loop;
        [SerializeField, ReadOnly] private AudioClip _audioClip;

        private void OnValidate()
        {
            _audioSource ??= GetComponent<AudioSource>();
        }

        internal void Play(AudioEvent audioEvent, bool loop = false)
        {
            if (_audioSource == null)
            {
                Debug.LogError("AudioPlayer: AudioSource is not assigned");
                return;
            }

            audioEvent.Play(_audioSource, loop);
        }
            
        internal void Play(AudioClip audioClip, bool loop = false)
        {
            _audioClip = audioClip;
            _audioSource.clip = _audioClip;
            _loop = loop;
            _audioSource.loop = _loop;
            _audioSource.PlayOneShot(audioClip);
        }
            
        internal void Stop()
        {
            if (_audioSource.isPlaying)
            {
                _audioSource.Stop();
            }
        }
    }
}