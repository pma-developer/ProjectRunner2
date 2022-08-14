using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.DamageSystem
{
    public class Damage
    {
        private float _value;
        private int _ownerLayer;

        public Damage(float value, int ownerLayer)
        {
            _value = value;
            _ownerLayer = ownerLayer;
        }
    }
}
