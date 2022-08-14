using System;
using System.Collections;
using System.Collections.Generic;
using ProjectRunner.Core.WeaponSystem;
using UnityEngine;

namespace ProjectRunner.Core.WeaponSystem
{
    public class WeaponInputController : MonoBehaviour
    {
        [SerializeField] private GameObject _weaponGameObject;
        private IWeapon _weapon;

        private void Awake()
        {
            _weapon = _weaponGameObject.GetComponent<IWeapon>();
        }

        private void FireOnFireButtonClick()
        {
            if (Input.GetButton("Fire1"))
            {
                _weapon.Fire();
            }
        }

        public void Update()
        {
            FireOnFireButtonClick();
        }
    }
}