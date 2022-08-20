using System;
using UnityEngine;
using Zenject;

namespace Core.DamageSystem
{
    public class DamageManager
    {
        private DamageLayersPreset _damageLayersPreset;

        [Inject]
        private void Construct(DamageLayersPreset damageLayersPreset)
        {
            _damageLayersPreset = damageLayersPreset;
        }

        public bool TryDealDamage(Damage damage, IDamageReceiver damageReceiver)
        {
            switch (damage.TargetsType)
            {
                case TargetsType.All:
                    damageReceiver.ReceiveDamage(damage);
                    return true;
                case TargetsType.AllExceptAllies:
                {
                    var damageDealerAllies =
                        _damageLayersPreset.LayerAllies.GetValue(1 << damage.OwnerLayer);
                    var isReceiverAllyForDealer = (1 << damageReceiver.GetLayer() & damageDealerAllies) != 0;
                    if (isReceiverAllyForDealer == false)
                    {
                        damageReceiver.ReceiveDamage(damage);
                        return true;
                    }

                    break;
                }
                case TargetsType.AllExceptSelf when damage.OwnerHashCode != damageReceiver.GetSelfHashCode():
                    damageReceiver.ReceiveDamage(damage);
                    return true;
                default:
                    Debug.LogError("Unhandled TargetType");
                    throw new ArgumentOutOfRangeException();
            }

            return false;
        }
    }
}