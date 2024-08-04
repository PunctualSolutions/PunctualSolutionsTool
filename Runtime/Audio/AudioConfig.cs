#region

using System.Collections.Generic;
using UnityEngine;

#endregion

namespace PunctualSolutions.Tool.Audio
{
    [CreateAssetMenu(menuName = "PunctualSolutions/Tool/Audio/AudioConfig", fileName = "Audio")]
    public class AudioConfig : ScriptableObject
    {
        [SerializeField] AudioClip[]              _BGMList;
        [SerializeField] AudioClip[]              _soundEffectList;
        public           IReadOnlyList<AudioClip> BGMList         => _BGMList;
        public           IReadOnlyList<AudioClip> SoundEffectList => _soundEffectList;
    }
}