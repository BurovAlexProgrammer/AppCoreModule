using UnityEngine;

namespace AppCoreModule.Scripts.Audio
{
    public abstract class AudioEvent : ScriptableObject
    {
        public abstract void Play(AudioSource audioSource);
    }
}
