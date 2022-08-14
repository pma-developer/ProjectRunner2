using System;
using System.Collections;
using System.Collections.Generic;
using Core.DamageSystem;
using ProjectRunner.Core.WeaponSystem;
using UnityEngine;
using Zenject;

namespace Core.WeaponSystem
{
    public class Shotgun : MonoBehaviour, IWeapon
    {
        
        [SerializeField] private float _shootingImpulseForce;
        [Tooltip("Time between two shots in seconds")]
        [SerializeField] private float _shootingCooldown;

        [Space]
        [SerializeField] private GameObject _shootingPoint;

        [SerializeField] private Rigidbody _movingParent;
        [SerializeField] private Animator _animator;

        private Projectile.Factory _projectileFactory;
        private float _timeFromLastShot;
        private static readonly int FireHash = Animator.StringToHash("Fire");

        [Inject]
        public void Construct(Projectile.Factory projectileFactory)
        {
            Debug.Log("injected bullet factory");
            _projectileFactory = projectileFactory;
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
            var projectile = _projectileFactory.Create(new Damage(5,0));
            var projectileTF = projectile.transform;
            projectileTF.position = _shootingPoint.transform.position;
            projectileTF.rotation = _shootingPoint.transform.rotation;
            
            projectile.AddForce(_shootingImpulseForce * projectileTF.forward + _movingParent.velocity);
        }
    }
}