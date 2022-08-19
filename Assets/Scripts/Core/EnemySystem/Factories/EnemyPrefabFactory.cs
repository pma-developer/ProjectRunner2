using System.Collections;
using System.Collections.Generic;
using Core.EnemySystem;
using UnityEngine;
using Zenject;

namespace Core.EnemySystem.Factories
{
    public class EnemyPrefabFactory : IFactory<Enemy>
    {
        private readonly DiContainer _container;
        private readonly GameObject _prefab;
        private readonly Transform _parent;


        public EnemyPrefabFactory(DiContainer container, GameObject prefab, Transform parent)
        {
            _container = container;
            _prefab = prefab;
            _parent = parent;
        }

        public Enemy Create()
        {
            return _container.InstantiatePrefabForComponent<Enemy>(_prefab, _parent);
        }
    }
}