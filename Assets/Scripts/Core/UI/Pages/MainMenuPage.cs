using UnityEngine;
using UniRx;
using UnityEngine.UI;

namespace Core.UI.Pages
{
    public class MainMenuPage : Page
    {
        [SerializeField] private Button _startBtn;
        [SerializeField] private Button _settingsBtn;
        [SerializeField] private Button _exitBtn;

        public void Start() {
            _startBtn.onClick.AsObservable().Subscribe(_ => Debug.Log("Start game")).AddTo(this);
            _settingsBtn.onClick.AsObservable().Subscribe(_ => UIManager.ReplacePage<SettingsPage>()).AddTo(this);
            _exitBtn.onClick.AsObservable().Subscribe(_ => Debug.Log("Should exit")).AddTo(this);
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
