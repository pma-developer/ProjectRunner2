using UnityEngine;
using Zenject;

namespace Core.Installers
{
    public class MainMenuInstaller : MonoInstaller
    {
        [SerializeField] private GameObject _uiManagerPrefab;


        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<UIManager>().FromComponentInNewPrefab(_uiManagerPrefab).AsSingle();
        }
    }
}