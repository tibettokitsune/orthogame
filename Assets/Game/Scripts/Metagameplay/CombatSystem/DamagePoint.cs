using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Scripts.Metagameplay.CombatSystem
{
    public enum DamagePointsType
    {
        LeftHand, RightHand, LeftLeg, RightLeg
    }
    public class DamagePoint : MonoBehaviour
    {
        public DamagePointsType pointType;
        private List<ICanBeDamaged> _damagedElements = new List<ICanBeDamaged>();
        private bool _isActive;
        private float _damage;
        
        public void UpdateState(bool state)
        {
            _isActive = state;
            if (!_isActive) _damagedElements = new List<ICanBeDamaged>();
        }

        public void UpdateDamageValue(float value) => _damage = value;

        private void OnTriggerEnter(Collider other)
        {
            if(!_isActive) return;

            var dmg = other.GetComponent<ICanBeDamaged>();
            if (dmg != null)
            {
                if(_damagedElements.Contains(dmg)) return;
                    
                dmg.GetDamage(_damage);
                _damagedElements.Add(dmg);
            }
        }
    }
}