using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.DamageSystem;
using UniRx;
using Zenject;

namespace Core.EnemySystem
{
    public class Enemy : MonoBehaviour, IPoolable<IMemoryPool>, IDamageReceiver, IDisposable
    {
        [SerializeField] private float _startHp;

        private IMemoryPool _memoryPool;

        private ReactiveProperty<float> _hp;


        private void HandleHpChange(float hp)
        {
            if (hp <= 0)
                Die();
        }
        
        private void Die()
        {
            Dispose();
        }
        
        public void ReceiveDamage(Damage damage)
        {
            _hp.Value -= damage.Value;
        }

        public int GetLayer() => gameObject.layer;
        public int GetSelfHashCode() => gameObject.GetHashCode();

        public void OnDespawned()
        {
            _memoryPool = null;
        }

        public void OnSpawned(IMemoryPool pool)
        {
            Debug.Log(GetLayer());
            _memoryPool = pool;
            _hp = new ReactiveProperty<float>();
            _hp.Value = _startHp;
            _hp.Subscribe(HandleHpChange).AddTo(this);
        }

        
        public void Dispose()
        {
            _hp?.Dispose();
            _memoryPool.Despawn(this);
        }

    }
}