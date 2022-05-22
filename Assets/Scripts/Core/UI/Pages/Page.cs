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
            Debug.Log("Inject");
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

        public virtual void Start()
        {
            Debug.Log("start");
            _canvas = GetComponent<Canvas>();
        }
    }
}
