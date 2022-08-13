using System;
using System.Collections;
using System.Collections.Generic;
using ProjectRunner.Core.WeaponSystem;
using UnityEngine;

namespace ProjectRunner.Core.WeaponSystem
{
    public class Shotgun : MonoBehaviour, IWeapon
    {
        [SerializeField] private GameObject _projectilePrefab;
        [SerializeField] private float _shootingImpulseForce;
        [Tooltip("Time between two shots in seconds")]
        [SerializeField] private float _shootingCooldown;

        [Space]
        [SerializeField] private GameObject _shootingPoint;

        [SerializeField] private Animator _animator;
        
        private float _timeFromLastShot;
        private static readonly int FireHash = Animator.StringToHash("Fire");

        public void Fire()
        {
            if (Time.time < _shootingCooldown + _timeFromLastShot) return;
            
            SpawnBullet();
            _animator.SetTrigger(FireHash);
            _timeFromLastShot = Time.time;
        }

        private void SpawnBullet()
        {
            var projectile = Instantiate(_projectilePrefab, _shootingPoint.transform.position,
                _shootingPoint.transform.rotation);
            projectile.GetComponent<Rigidbody>()
                .AddForce(_shootingImpulseForce * projectile.transform.forward, ForceMode.Impulse);
        }
    }
}