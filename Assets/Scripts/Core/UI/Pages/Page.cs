using UnityEngine;
using Zenject;

namespace Core.UI.Pages
{
    [RequireComponent(typeof(Canvas))]
    public abstract class Page : MonoBehaviour, IPage
    {
        protected UIManager UIManager;
        private Canvas _canvas;
        
        [Inject]
        private void Init(UIManager uiManager)
        {
            UIManager = uiManager;
        }

        public virtual void Open()
        {
            gameObject.SetActive(true);
        }

        public virtual void Close()
        {
            gameObject.SetActive(false);
        }

        public void Start()
        {
            _canvas = GetComponent<Canvas>();
        }
    }
}
