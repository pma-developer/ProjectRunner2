using UnityEngine;
using Zenject;

namespace Core.Installers
{
    public class GameInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            PagesInstaller.Install(Container);
            Container.Bind<UIManager>().AsSingle();
        }
    }
}