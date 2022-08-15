using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.DamageSystem
{
    public class Damage
    {
        public readonly float Value;
        public readonly int OwnerLayer;

        public Damage(float value, int ownerLayer)
        {
            Value = value;
            OwnerLayer = ownerLayer;
        }
    }
}
