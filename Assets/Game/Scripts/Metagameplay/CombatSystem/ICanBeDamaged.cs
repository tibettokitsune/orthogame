using System;

namespace Game.Scripts.Metagameplay.CombatSystem
{
    public interface ICanBeDamaged
    {
        public Guid ID { get; }

        public float Health { get; }
        public void GetDamage(float value);
    }
}