using Core.Installers;
using UnityEngine;
using Zenject;

namespace Core.UI.Pages
{
    public class MainMenuPage : Page
    {
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
