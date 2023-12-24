using System;
using UniRx;

namespace Game.Scripts.Metagameplay.CombatSystem
{
    public interface ICanBeDamaged
    {
        public ReactiveCommand OnDeath { get; }
        public ReactiveCommand<float>  OnDamage{ get; }
        public Guid ID { get; }

        public float Health { get; }
        public void GetDamage(float value);
    }
}