using System.Collections.Generic;
using Core.DamageSystem;
using UnityEngine;
using Zenject;

namespace Core.WeaponSystem.Projectiles.Factories
{
    public class ProjectileFactory : PlaceholderFactory<ProjectileType, Damage, Projectile>
    {
    }

    public class ProjectileIFactory : IFactory<ProjectileType, Damage, Projectile>, IInitializable
    {
        private DiContainer _diContainer;
        private ProjectileTypesPreset _projectileTypesPreset;

        private Dictionary<ProjectileType, MonoPoolableMemoryPool<Damage, IMemoryPool, Projectile>> _projectilePools;


        [Inject]
        private void Construct(DiContainer diContainer, ProjectileTypesPreset projectileTypesPreset)
        {
            _diContainer = diContainer;
            _projectileTypesPreset = projectileTypesPreset;
            _projectilePools = new();
            Initialize();
        }

        private GameObject GetEmptyGameObject(string name)
        {
            var parentGameObject = new GameObject(name);
            parentGameObject.name = name;

            return parentGameObject;
        }

        public void Initialize()
        {
            foreach (var (projectileType, projectilePrefab) in _projectileTypesPreset.ProjectilePrefabs)
            {
                var settings = new MemoryPoolSettings(
                    _projectileTypesPreset.InitialPoolSizes.GetValue(projectileType),
                    int.MaxValue,
                    PoolExpandMethods.Double
                );

                var parentGameObject = GetEmptyGameObject(projectileType.ToString());

                var projectilePool = _diContainer.Instantiate<MonoPoolableMemoryPool<Damage, IMemoryPool, Projectile>>(
                    new object[]
                    {
                        settings,
                        new ProjectilePrefabFactory(_diContainer, projectilePrefab, parentGameObject.transform)
                    }
                );

                _projectilePools.Add(projectileType, projectilePool);
            }
        }

        public Projectile Create(ProjectileType projectileType, Damage damage)
        {
            var pool = _projectilePools[projectileType];
            return pool.Spawn(damage, pool);
        }
    }
}