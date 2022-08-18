using Core.DamageSystem;
using Core.EnemySystem;
using Core.WeaponSystem;
using UnityEngine;
using Zenject;

namespace Core.Installers
{
    [CreateAssetMenu(fileName = "GameInstaller", menuName = "Installers/GameInstaller")]
    public class GameInstaller : ScriptableObjectInstaller
    {
        [SerializeField] private string _projectilesPoolName;
        [SerializeField] private string _enemiesPoolName;
        [SerializeField] private ProjectilePrefabsPreset _projectilePrefabsPreset;
        [SerializeField] private GameObject _projectilePrefab;
        [SerializeField] private GameObject _enemyPrefab;

        public override void InstallBindings()
        {
            Container.Bind<DamageManager>().AsSingle();

            Container.Bind<ProjectilePrefabsPreset>().FromInstance(_projectilePrefabsPreset).AsSingle();

            // Container.BindFactory<ProjectileType, Damage, Projectile, ProjectileFactory>().FromFactory<ProjectileIFactory>().NonLazy();
            Container.BindFactory<ProjectileType, Damage, Projectile, ProjectileFactory>()
                .FromIFactory(projectileFactory => projectileFactory.To<ProjectileIFactory>().AsCached().NonLazy());

            Container.BindFactory<Enemy, Enemy.Factory>()
                .FromMonoPoolableMemoryPool(
                    factory =>
                        factory.WithInitialSize(20)
                            .FromComponentInNewPrefab(_enemyPrefab)
                            .UnderTransformGroup(_enemiesPoolName)
                );
        }
    }
}