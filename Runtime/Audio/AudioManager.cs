#region

using System.Collections.Generic;
using UnityEngine;

#endregion

namespace PunctualSolutions.Tool.Audio
{
    public class AudioManager : MonoSingleton<AudioManager>
    {
        [SerializeField] AudioConfig           _audioConfig;
        AudioSource                            bgmComponent;
        readonly Dictionary<string, AudioClip> _bgmList = new();
        AudioSource                            soundEffectsComponent;
        readonly Dictionary<string, AudioClip> soundEffects = new();
        public   float                         BGMVolume          => bgmComponent.volume;
        public   float                         SoundEffectsVolume => soundEffectsComponent.volume;

        public override void Awake()
        {
            base.Awake();
            foreach (var audioClip in _audioConfig.BGMList) _bgmList.Add(audioClip.name, audioClip);
            foreach (var audioClip in _audioConfig.SoundEffectList) soundEffects.Add(audioClip.name, audioClip);
            bgmComponent          = gameObject.AddComponent<AudioSource>();
            bgmComponent.loop     = true;
            
            soundEffectsComponent = gameObject.AddComponent<AudioSource>();
        }

        public void PlayBgm(string inName)
        {
            if (!_bgmList.TryGetValue(inName, out var bgm)) return;
            PlayBgm(bgm);
        }

        public void PlayBgm(AudioClip clip)
        {
            if (!clip) return;
            bgmComponent.clip = clip;
            bgmComponent.Play();
        }

        public void StopBgm() => bgmComponent.Stop();

        public void PlaySe(string seName)
        {
            if (!soundEffects.TryGetValue(seName, out var effect)) return;
            PlaySe(effect);
        }

        public void PlaySe(AudioClip clip)
        {
            if (!clip) return;
            soundEffectsComponent.PlayOneShot(clip);
        }

        public void SetVolume(float bgmVolume, float seVolume)
        {
            bgmComponent.volume          = bgmVolume;
            soundEffectsComponent.volume = seVolume;
        }
    }
}