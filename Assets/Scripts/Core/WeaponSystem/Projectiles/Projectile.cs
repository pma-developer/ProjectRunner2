using System;
using Core.DamageSystem;
using UniRx;
using UnityEngine;
using Zenject;

namespace Core.WeaponSystem.Projectiles
{
    public class Projectile : MonoBehaviour, IPoolable<Damage, IMemoryPool>, IDisposable
    {
        [SerializeField] private float _maxLifetime;

        private IMemoryPool _memoryPool;
        private DamageManager _damageManager;

        private Damage _damage;

        private Rigidbody _rigidbody;

        private CompositeDisposable _disposables;
        private Subject<Unit> _disposeSubject;


        public void AddForce(Vector3 force)
        {
            _rigidbody.AddForce(force, ForceMode.Impulse);
        }

        protected void GetDisposed() => _disposeSubject.OnNext(Unit.Default);

        protected void DamageIfDamageReceiver(GameObject gameObjectToDamage)
        {
            if (gameObjectToDamage.TryGetComponent<IDamageReceiver>(out var damageReceiver))
            {
                _damageManager.TryDealDamage(_damage, damageReceiver);
            }
            else if (gameObjectToDamage.transform.parent is not null &&
                     gameObjectToDamage.transform.parent.TryGetComponent<IDamageReceiver>(out var parentDamageReceiver))
            {
                _damageManager.TryDealDamage(_damage, parentDamageReceiver);
            }
        }

        protected virtual void OnCollisionEnter(Collision collision)
        {
            DamageIfDamageReceiver(collision.gameObject);
            GetDisposed();
        }

        [Inject]
        public void Construct(DamageManager damageManager)
        {
            _damageManager = damageManager;
        }

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _disposables = new();
            _disposeSubject = new();
        }

        public void OnDespawned()
        {
            _rigidbody.velocity = Vector3.zero;
            _disposables.Clear();
            _memoryPool = null;
            _damage = null;
        }

        public void OnSpawned(Damage damage, IMemoryPool memoryPool)
        {
            _memoryPool = memoryPool;
            _damage = damage;
            Observable.Amb(
                Observable.Timer(TimeSpan.FromSeconds(_maxLifetime)).AsUnitObservable(),
                _disposeSubject.AsObservable()
            ).Subscribe(_ => Dispose()).AddTo(_disposables);
        }

        public void Dispose()
        {
            _memoryPool.Despawn(this);
        }
    }
}