using Cysharp.Threading.Tasks;
using PunctualSolutions.Tool.Addressables;
using QFramework;
using UnityEngine;
using UnityEngine.UI;

namespace PunctualSolutions.Tool.UGUI
{
    public class ButtonEx : Button
    {
        [SerializeField]
        AudioClip _audioClip;

        protected override void Start()
        {
            base.Start();
            onClick.AddListener(UniTask.UnityAction(async () =>
            {
                if (_audioClip)
                {
                    AudioKit.PlayVoice(_audioClip);
                    return;
                }

                var audioClip = await UGUIManager.Instance.Settings.DefaultButtonClickSound.Get();
                if (audioClip) AudioKit.PlayVoice(audioClip);
            }));
        }
    }
}