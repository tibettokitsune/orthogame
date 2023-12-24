using System;
using Game.Scripts.UI;
using UniRx;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Metagameplay.CombatSystem
{
    public class DamageUnit  : MonoBehaviour, ICanBeDamaged
    {
        [Inject] private BarsPanelController _barsPanelController;
        public ReactiveCommand OnDeath { get; } = new ReactiveCommand();
        public ReactiveCommand<float> OnDamage { get; } = new ReactiveCommand<float>();
        public Guid ID { get; private set; }
        
        
        public float Health { get; private set; }
        private float _startHealth;

        private void Start()
        {
            ID = Guid.NewGuid();
            Health = 10f;
            _startHealth = Health;
            _barsPanelController.AddUnitBar(this, transform);
        }
        public void GetDamage(float value)
        {
            Health -= value;
            OnDamage.Execute(Health / _startHealth);
            if (Health <= 0) Death();
            
            Debug.Log($"Unit with id {ID} got {value} damage");
        }

        private void Death()
        {
            OnDeath.Execute();
            Destroy(gameObject);
        }
    }
}