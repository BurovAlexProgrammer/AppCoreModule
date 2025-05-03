using AppCoreModule.Scripts.Audio;
using UnityEngine;

namespace AppCoreModule.Scripts.Services
{
    public class AudioService : MonoBehaviour
    {
        [SerializeField] private AudioPlayer _sfxPlayer;
        [SerializeField] private AudioPlayer _musicPlayer;

        public void PlaySfx(AudioEvent audioEvent)
        {
            _sfxPlayer.Play(audioEvent);
        }
        
        public void PlaySfx(AudioClip audioClip, bool loop = false)
        {
            _sfxPlayer.Play(audioClip, loop);
        }
        
        public void PlayMusic(AudioEvent audioEvent)
        {
            _musicPlayer.Play(audioEvent);
        }
        
        public void PlayMusic(AudioClip audioClip, bool loop = false)
        {
            _musicPlayer.Play(audioClip, loop);
        }
        
        public void StopSfx()
        {
            _sfxPlayer.Stop();
        }
        
        public void StopMusic()
        {
            _musicPlayer.Stop();
        }


    }
}