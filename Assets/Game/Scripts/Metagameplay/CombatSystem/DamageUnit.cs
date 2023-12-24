using System;
using UnityEngine;

namespace Game.Scripts.Metagameplay.CombatSystem
{
    public class DamageUnit  : MonoBehaviour, ICanBeDamaged
    {
        public Guid ID { get; private set; }

        private void Start()
        {
            ID = Guid.NewGuid();
            Health = 10f;
        }

        public float Health { get; private set; }
        public void GetDamage(float value)
        {
            Health -= value;
            if (Health <= 0) Death();
            
            Debug.Log($"Unit with id {ID} got {value} damage");
        }

        private void Death()
        {
            Destroy(gameObject);
        }
    }
}