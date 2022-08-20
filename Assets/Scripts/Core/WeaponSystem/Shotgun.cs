using Core.DamageSystem;
using Core.WeaponSystem.Projectiles.Factories;
using ProjectRunner.Core.WeaponSystem;
using UnityEngine;
using Zenject;

namespace Core.WeaponSystem
{
    public class Shotgun : MonoBehaviour, IWeapon
    {
        [SerializeField] private float _shootingImpulseForce;

        [Tooltip("Time between two shots in seconds")] [SerializeField]
        private float _shootingCooldown;

        [Space] [SerializeField] private GameObject _shootingPoint;

        [SerializeField] private Rigidbody _movingParent;
        [SerializeField] private Animator _animator;

        private ProjectileFactory _projectilePoolFactory;
        private float _timeFromLastShot;
        private static readonly int FireHash = Animator.StringToHash("Fire");

        [Inject]
        public void Construct(ProjectileFactory projectilePoolFactory)
        {
            _projectilePoolFactory = projectilePoolFactory;
        }

        public void Fire()
        {
            if (Time.time < _shootingCooldown + _timeFromLastShot) return;

            SpawnBullet();
            _animator.SetTrigger(FireHash);
            _timeFromLastShot = Time.time;
        }

        private void SpawnBullet()
        {
            var projectile =
                _projectilePoolFactory.Create(ProjectileType.Bullet, new Damage(5, 0, gameObject.GetHashCode(),TargetsType.AllExceptSelf));
            var projectileTF = projectile.transform;
            projectileTF.position = _shootingPoint.transform.position;
            projectileTF.rotation = _shootingPoint.transform.rotation;

            projectile.AddForce(_shootingImpulseForce * projectileTF.forward + _movingParent.velocity);
        }
    }
}