using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.DamageSystem
{
    public class Damage
    {
        public readonly float Value;
        public readonly TargetsType TargetsType;
        public readonly int OwnerHashCode;
        public readonly int OwnerLayer;

        public Damage(float value, int ownerLayer, int ownerHashCode, TargetsType targetsType)
        {
            Value = value;
            OwnerLayer = ownerLayer;
            OwnerHashCode = ownerHashCode;
            TargetsType = targetsType;
        }
    }
}
