using System;
using Core.DamageSystem;
using UniRx;
using UnityEngine;
using Zenject;

namespace Core.WeaponSystem
{
    public abstract class Projectile : MonoBehaviour, IPoolable<Damage, IMemoryPool>, IDisposable
    {
        [SerializeField] private float _maxLifetime;

        private IMemoryPool _memoryPool;
        private DamageManager _damageManager;

        private Damage _damage;

        private Rigidbody _rigidbody;

        private CompositeDisposable _disposables;
        protected Subject<Unit> _disposeSubject;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _disposables = new();
            _disposeSubject = new();
        }

        public void AddForce(Vector3 force)
        {
            _rigidbody.AddForce(force, ForceMode.Impulse);
        }

        protected virtual void OnCollisionEnter(Collision collision)
        {
            if (collision.transform.parent.TryGetComponent<IDamageReceiver>(out var damageReceiver))
            {
                _damageManager.TryDealDamage(_damage, damageReceiver);
            }
            
            _disposeSubject.OnNext(Unit.Default);
        }

        [Inject]
        public void Construct(DamageManager damageManager)
        {
            Debug.Log("injected");
            _damageManager = damageManager;
        }

        public void OnDespawned()
        {
            _disposables.Clear();
            _memoryPool = null;
            _damage = null;
            _rigidbody.velocity = Vector3.zero;
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

        public class PoolFactory : PlaceholderFactory<Damage, Projectile>
        {
        }
    }
    
    public class ProjectileFactory : IFactory<ProjectileType, Damage, Projectile>
    {
        private DiContainer _container;

        public ProjectileFactory(DiContainer container)
        {
            _container = container;
        }

        public Projectile Create(ProjectileType param1, Damage param2)
        {
            throw new NotImplementedException();
        }
    }
}
