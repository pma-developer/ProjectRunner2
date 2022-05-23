using Core.Data;
using UnityEngine;
using Zenject;

namespace Core.Installers
{
    [CreateAssetMenu(fileName = "DataInstaller", menuName = "Installers/DataInstaller")]
    public class DataInstaller : ScriptableObjectInstaller<DataInstaller>
    {
        [SerializeField]
        private DataPaths _pathsConfig;
        
        public override void InstallBindings()
        {
            Container.Bind<DataPaths>().FromInstance(_pathsConfig).AsSingle();
            Container.BindInterfacesAndSelfTo<Repository>().AsSingle();
            
            Container.BindInterfacesAndSelfTo<SettingsModel>().AsSingle();
        }
    }
}
