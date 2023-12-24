using System;
using Cinemachine;
using Game.Scripts.Metagameplay.CombatSystem;
using UniRx;
using UnityEngine;

namespace Game.Scripts.UI
{
    public class BarsPanelController : MonoBehaviour
    {
        [SerializeField] private UnitBar unitHpBarPrefab;

        private CompositeDisposable _disposable = new CompositeDisposable();
        
        public void AddUnitBar(ICanBeDamaged damageUnit, Transform target)
        {
            var bar = Instantiate(unitHpBarPrefab, transform);
            bar.UpdateTarget(target);
            damageUnit.OnDamage.Subscribe(_ =>
            {
                bar.UpdateData(new float[]{_});
            }).AddTo(target);
            damageUnit.OnDeath.Subscribe(_ =>
            {
                Destroy(bar.gameObject);
            }).AddTo(target);
        }
    }
}