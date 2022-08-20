namespace Core.DamageSystem
{
    public interface IDamageReceiver
    {
        public void ReceiveDamage(Damage damage);

        public int GetLayer();

        public int GetSelfHashCode();
    }
}