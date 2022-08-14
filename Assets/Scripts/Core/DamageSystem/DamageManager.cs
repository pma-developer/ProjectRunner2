namespace Core.DamageSystem
{
    public class DamageManager
    {
        public bool TryDealDamage(Damage damage, IDamageReceiver damageReceiver)
        {
            damageReceiver.ReceiveDamage(damage);
            return true;
        }
    }
}