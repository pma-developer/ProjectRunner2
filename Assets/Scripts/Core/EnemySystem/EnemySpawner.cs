using System;
using System.Collections;
using System.Collections.Generic;
using Core.EnemySystem;
using UniRx;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Transform _firstBoundTF;
    [SerializeField] private Transform _secondBoundTF;
    [SerializeField] private float _spawnY;
    [SerializeField] private float _spawnCooldownInMilliseconds;

    private Enemy.Factory _enemyFactory;

    [Inject]
    private void Construct(Enemy.Factory enemyFactory)
    {
        _enemyFactory = enemyFactory;
    }

    private void Start()
    {
        Observable.Interval(TimeSpan.FromMilliseconds(_spawnCooldownInMilliseconds))
            .Subscribe(x => SpawnEnemyAtRandomPosition()).AddTo(this);
    }

    private Vector3 GetRandomPositionFromSortedBounds(Vector3 leftBottom, Vector3 rightTop)
    {
        var randomX = Random.Range(leftBottom.x, rightTop.x);
        var randomY = Random.Range(leftBottom.y, rightTop.y);
        var randomZ = Random.Range(leftBottom.z, rightTop.z);

        return new Vector3(randomX, randomY, randomZ);
    }

    private void SpawnEnemyAtRandomPosition()
    {
        var firstBoundPosition = _firstBoundTF.position;
        var secondBoundPosition = _secondBoundTF.position;

        var leftBottomX = Math.Min(firstBoundPosition.x, secondBoundPosition.x);
        var leftBottomZ = Math.Min(firstBoundPosition.z, secondBoundPosition.z);

        var rightTopX = Math.Max(firstBoundPosition.x, secondBoundPosition.x);
        var rightTopZ = Math.Max(firstBoundPosition.z, secondBoundPosition.z);

        var leftBottomPosition = new Vector3(leftBottomX, _spawnY, leftBottomZ);
        var rightTopPosition = new Vector3(rightTopX, _spawnY, rightTopZ);

        var enemy = _enemyFactory.Create();
        enemy.transform.position = GetRandomPositionFromSortedBounds(leftBottomPosition, rightTopPosition);
    }
}