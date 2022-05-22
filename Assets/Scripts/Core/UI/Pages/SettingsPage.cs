using UnityEngine;
using UniRx;
using TMPro;
using UnityEngine.UI;
using Zenject;
using Core.Data;

namespace Core.UI.Pages
{
    public class SettingsPage : Page
    {
        private SettingsModel _settings;

        [SerializeField]
        private Button _toggleAudioBtn;

        [Inject]
        public void Init(SettingsModel settings)
        {
            _settings = settings;
        }

        public void Start()
        {
            var toggleAudioBtnText = _toggleAudioBtn.GetComponentInChildren<TextMeshProUGUI>();
            _settings.MuteAudio.SubscribeWithState(toggleAudioBtnText, (x, t) =>
            {
                if (x) t.text = "Unmute";
                else t.text = "Mute";
            });
            _toggleAudioBtn.onClick.AsObservable().Subscribe(val =>
            {
                _settings.MuteAudio.Value = !_settings.MuteAudio.Value;
            }).AddTo(this);
        }

        public override void Open()
        {
            gameObject.SetActive(true);
        }

        public override void Close()
        {
            gameObject.SetActive(false);
        }
    }
}
