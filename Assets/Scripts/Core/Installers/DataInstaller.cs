using UnityEngine;
using Zenject;
using Core.Data;

namespace Core.Installers
{
    public class DataInstaller : Installer<DataInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<SettingsModel>().AsSingle();
        }
    }
}
