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
        [SerializeField] private ProjectilePrefabsContainer _projectilePrefabsContainer;
        [SerializeField] private GameObject _projectilePrefab;
        [SerializeField] private GameObject _enemyPrefab;

        public override void InstallBindings()
        {
            Container.Bind<DamageManager>().AsSingle();
            
            Container.BindFactory<Damage, Projectile, Projectile.PoolFactory>()
                .FromMonoPoolableMemoryPool(
                    factory =>
                        factory.WithInitialSize(200)
                            .FromComponentInNewPrefab(_projectilePrefab)
                            .UnderTransformGroup(_projectilesPoolName)
                );
            
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