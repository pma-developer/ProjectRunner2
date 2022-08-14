using Core.DamageSystem;
using Core.WeaponSystem;
using UnityEngine;
using Zenject;

namespace Core.Installers
{
    [CreateAssetMenu(fileName = "GameInstaller", menuName = "Installers/GameInstaller")]
    public class GameInstaller : ScriptableObjectInstaller
    {
        [SerializeField] private string projectilesPoolName;
        [SerializeField] private GameObject projectilePrefab;

        public override void InstallBindings()
        {
            Container.Bind<DamageManager>().AsSingle();
            Container.BindFactory<Damage, Projectile, Projectile.Factory>()
                .FromMonoPoolableMemoryPool(
                    factory =>
                        factory.WithInitialSize(200)
                            .FromComponentInNewPrefab(projectilePrefab)
                            .UnderTransformGroup(projectilesPoolName)
                );
        }
    }
}