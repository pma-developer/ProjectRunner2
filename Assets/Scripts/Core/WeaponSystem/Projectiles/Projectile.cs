using System;
using System.Collections;
using System.Collections.Generic;
using Core.DamageSystem;
using Unity.VisualScripting;
using UnityEngine;
using Zenject;

namespace Core.WeaponSystem
{
    public class Projectile : MonoBehaviour, IPoolable<Damage, IMemoryPool>, IDisposable
    {
        private IMemoryPool _memoryPool;
        private DamageManager _damageManager;
        
        private Damage _damage;

        private Rigidbody _rigidbody;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        public void AddForce(Vector3 force)
        {
            _rigidbody.AddForce(force, ForceMode.Impulse);
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.TryGetComponent<IDamageReceiver>(out var damageReceiver))
            {
                _damageManager.TryDealDamage(_damage, damageReceiver);
            }
            _memoryPool.Despawn(this);
        }

        [Inject]
        public void Construct(DamageManager damageManager)
        {
            _damageManager = damageManager;
        }

        public void OnDespawned()
        {
            _memoryPool = null;
            _damage = null;
            _rigidbody.velocity = Vector3.zero;
        }

        public void OnSpawned(Damage damage, IMemoryPool memoryPool)
        {
            _memoryPool = memoryPool;
            _damage = damage;
        }

        public void Dispose()
        {
            _memoryPool.Despawn(this);
        }

        public class Factory : PlaceholderFactory<Damage, Projectile>
        {
        }
    }
}
