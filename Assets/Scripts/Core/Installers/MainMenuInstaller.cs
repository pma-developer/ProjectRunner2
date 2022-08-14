using UnityEngine;
using Zenject;

namespace Core.Installers
{
    public class MainMenuInstaller : MonoInstaller
    {
        [SerializeField]
        private GameObject uiManagerPrefab;
        

        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<UIManager>().FromComponentInNewPrefab(uiManagerPrefab).AsSingle();
        }
    }
}
