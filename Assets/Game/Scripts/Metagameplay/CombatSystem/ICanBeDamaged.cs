namespace Game.Scripts.Metagameplay.CombatSystem
{
    public interface ICanBeDamaged
    {
        public float Health { get; }
        public void GetDamage(float value);
    }
}