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

        [Header("Volume")]
        [SerializeField]
        private Slider _volumeSlider;
        [SerializeField]
        private TextMeshProUGUI _volumeValueText;

        [Header("Back")]
        [SerializeField]
        private Button _backToMainMenuBtn;

        [Inject]
        public void Init(SettingsModel settings)
        {
            _settings = settings;
        }

        public void Start()
        {
            _settings.AudioVolume.SubscribeWithState(
                _volumeValueText,
                (volumeLevel, t) => t.text = (volumeLevel == 0) ? "Muted" : volumeLevel.ToString()
            ).AddTo(this);
            _volumeSlider.onValueChanged.AsObservable().Subscribe(val => _settings.AudioVolume.Value = val).AddTo(this);
            _settings.AudioVolume.SubscribeWithState(_volumeSlider, (volume, slider) => slider.value = volume);
            _backToMainMenuBtn.onClick.AsObservable().Subscribe(_ => UIManager.ReplacePage<MainMenuPage>()).AddTo(this);
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
