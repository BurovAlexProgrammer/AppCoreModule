using System;
using AppCoreModule.Scripts.Extensions;
using Main.Extension.Attributes;
using UnityEngine;

namespace AppCoreModule.Scripts.Audio
{
    [Serializable]
    [CreateAssetMenu(menuName = "Custom/Audio/Simple Audio Event")]
    public class SimpleAudioEvent : AudioEvent
    {
        [SerializeField] private AudioClip[] _audioClips;
        [SerializeField] private RangedFloat _volume = new (0.8f, 1f);
        [SerializeField] [MinMaxRange(0,2)] private RangedFloat _pitch = new (0.8f, 1.2f);
        
        public override void Play(AudioSource audioSource, bool loop = false)
        {
            if (_audioClips.Length == 0) throw new Exception("No audio clips on AudioEvent");

            audioSource.clip = _audioClips.GetRandomItem();
            audioSource.volume = _volume.GetRandomValue();
            audioSource.pitch = _pitch.GetRandomValue();
            audioSource.loop = loop;
            audioSource.PlayOneShot(audioSource.clip);
        }
    }
}