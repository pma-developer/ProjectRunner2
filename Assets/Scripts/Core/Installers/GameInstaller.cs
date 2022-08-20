using Core.DamageSystem;
using Core.EnemySystem;
using Core.EnemySystem.Factories;
using Core.WeaponSystem;
using Core.WeaponSystem.Projectiles;
using Core.WeaponSystem.Projectiles.Factories;
using UnityEngine;
using Zenject;
using Zenject.SpaceFighter;

namespace Core.Installers
{
    [CreateAssetMenu(fileName = "GameInstaller", menuName = "Installers/GameInstaller")]
    public class GameInstaller : ScriptableObjectInstaller
    {
        [SerializeField] private DamageLayersPreset _damageLayersPreset;
        [SerializeField] private EnemyTypesPreset _enemyTypesPreset; 
        [SerializeField] private ProjectileTypesPreset _projectileTypesPreset;

        public override void InstallBindings()
        {
            Container.Bind<DamageLayersPreset>().FromInstance(_damageLayersPreset).AsSingle();
            Container.Bind<ProjectileTypesPreset>().FromInstance(_projectileTypesPreset).AsSingle();
            Container.Bind<EnemyTypesPreset>().FromInstance(_enemyTypesPreset).AsSingle();
            
            Container.Bind<DamageManager>().AsSingle();


            Container.BindFactory<ProjectileType, Damage, Projectile, ProjectileFactory>()
                .FromIFactory(factory => factory.To<ProjectileIFactory>().AsCached().NonLazy());

            
            Container.BindFactory<EnemyType, Enemy, EnemyFactory>()
                .FromIFactory(factory => factory.To<EnemyIFactory>().AsCached().NonLazy());
        }
    }
}