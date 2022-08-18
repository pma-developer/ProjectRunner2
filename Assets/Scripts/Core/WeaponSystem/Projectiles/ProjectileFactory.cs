using System.Collections;
using System.Collections.Generic;
using Core.DamageSystem;
using Core.Utils;
using Core.WeaponSystem;
using UnityEngine;
using Zenject;

public class ProjectileFactory : PlaceholderFactory<ProjectileType, Damage, Projectile>
{
}

public class ProjectileIFactory : IFactory<ProjectileType, Damage, Projectile>, IInitializable
{
    private DiContainer _diContainer;
    private ProjectilePrefabsPreset _projectilePrefabsPreset;

    private Dictionary<ProjectileType, MonoPoolableMemoryPool<Damage, IMemoryPool, Projectile>> _projectilePools;


    [Inject]
    private void Construct(DiContainer diContainer, ProjectilePrefabsPreset projectilePrefabsPreset)
    {
        _diContainer = diContainer;
        _projectilePrefabsPreset = projectilePrefabsPreset;
        _projectilePools = new();
        Debug.Log("INITED");
        Initialize();
    }

    public void Initialize()
    {
        foreach (var (projectileType, projectilePrefab) in _projectilePrefabsPreset.ProjectilePrefabs)
        {
            var settings = new MemoryPoolSettings(
                _projectilePrefabsPreset.InitialPoolSizes.GetValue(projectileType),
                int.MaxValue,
                PoolExpandMethods.Double
            );

            var parentGameObject = new GameObject(projectileType.ToString());
            parentGameObject.name = projectileType.ToString();
            var projectilePool = _diContainer.Instantiate<MonoPoolableMemoryPool<Damage, IMemoryPool, Projectile>>(
                new object[]
                {
                    settings, new ProjectilePrefabFactory(_diContainer, projectilePrefab, parentGameObject.transform)
                });

            _projectilePools.Add(projectileType, projectilePool);
        }
    }

    public Projectile Create(ProjectileType projectileType, Damage damage)
    {
        var pool = _projectilePools[projectileType];
        return pool.Spawn(damage, pool);
    }
}