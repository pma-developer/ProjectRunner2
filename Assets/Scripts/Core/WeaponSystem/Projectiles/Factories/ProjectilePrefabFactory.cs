using System.Collections;
using System.Collections.Generic;
using Core.WeaponSystem;
using UnityEngine;
using Zenject;

namespace Core.WeaponSystem.Projectiles.Factories
{
    public class ProjectilePrefabFactory : IFactory<Projectile>
    {
        private readonly DiContainer _container;
        private readonly GameObject _prefabToSpawn;
        private readonly Transform _parent;

        public ProjectilePrefabFactory(DiContainer container, GameObject prefabToSpawn, Transform parent)
        {
            _container = container;
            _prefabToSpawn = prefabToSpawn;
            _parent = parent;
        }

        public Projectile Create() =>
            _container.InstantiatePrefabForComponent<Projectile>(_prefabToSpawn, _parent);
    }
}