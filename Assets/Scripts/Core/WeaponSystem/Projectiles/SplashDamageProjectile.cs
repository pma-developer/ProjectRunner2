using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.WeaponSystem.Projectiles
{
    public class SplashDamageProjectile : Projectile
    {
        private float _damageSphereRadius;

        protected override void OnCollisionEnter(Collision collision)
        {
            var objectsInRadius = Physics.OverlapSphere(collision.GetContact(0).point, _damageSphereRadius);

            foreach (var objectInRadius in objectsInRadius)
            {
                DamageIfDamageReceiver(objectInRadius.gameObject);
            }
            
            base.OnCollisionEnter(collision);
        }
    }
}