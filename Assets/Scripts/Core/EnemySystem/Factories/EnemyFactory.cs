using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Core.EnemySystem.Factories
{
    public class EnemyFactory : PlaceholderFactory<EnemyType, Enemy>
    {
    }

    public class EnemyIFactory : IFactory<EnemyType, Enemy>, IInitializable
    {
        private DiContainer _container;
        private EnemyTypesPreset _enemyTypesPreset;

        private Dictionary<EnemyType, MonoPoolableMemoryPool<IMemoryPool, Enemy>> _enemyPools;

        [Inject]
        private void Construct(DiContainer container, EnemyTypesPreset enemyTypesPreset)
        {
            _container = container;
            _enemyTypesPreset = enemyTypesPreset;
            _enemyPools = new();
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
            foreach (var (enemyType, enemyPrefab) in _enemyTypesPreset.EnemyPrefabs)
            {
                var poolSettings = new MemoryPoolSettings(
                    _enemyTypesPreset.InitialPoolSizes.GetValue(enemyType),
                    int.MaxValue,
                    PoolExpandMethods.Double
                );

                var parentGameObject = GetEmptyGameObject(enemyType.ToString());

                var pool = _container.Instantiate<MonoPoolableMemoryPool<IMemoryPool, Enemy>>(new object[]
                    {
                        poolSettings,
                        new EnemyPrefabFactory(_container, enemyPrefab, parentGameObject.transform)
                    }
                );

                _enemyPools.Add(enemyType, pool);
            }
        }

        public Enemy Create(EnemyType enemyType)
        {
            var pool = _enemyPools[enemyType];
            return pool.Spawn(pool);
        }
    }
}