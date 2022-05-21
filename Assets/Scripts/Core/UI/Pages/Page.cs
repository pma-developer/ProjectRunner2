using UnityEngine;
using Zenject;

namespace Core.UI.Pages
{
    [RequireComponent(typeof(Canvas))]
    public abstract class Page : MonoBehaviour, IPage, IInitializable
    {
        protected UIManager UIManager;
        private Canvas _canvas;
        
        [Inject]
        private void Init(UIManager uiManager)
        {
            UIManager = uiManager;
        }
        
        public abstract void Open();

        public abstract void Close();
        
        public void Initialize()
        {
            _canvas = GetComponent<Canvas>();
        }
    }
}