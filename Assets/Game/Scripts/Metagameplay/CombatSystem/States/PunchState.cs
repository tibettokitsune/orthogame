using System.Collections.Generic;
using Game.Scripts.Metagameplay.Player;
using UnityEngine;
using UnityHFSM;

namespace Game.Scripts.Metagameplay.CombatSystem.States
{
    public class PunchState : State
    {
        private float _punchStartTime;
        private float _lastExitTime;

        private int _numberIfSeries;
        private const float DelayBeforeSeriesStop = 0.5f;
        private const float TimeBeforeExit = 1f;
        private const int LimitInSeries = 2;

        private CharacterAnimationController _animationController;
        private List<DamagePoint> _damagePoints;
        public PunchState(CharacterAnimationController animationController, List<DamagePoint> damagePoints)
        {
            _animationController = animationController;
            _damagePoints = damagePoints;
        }
        public override void OnEnter()
        {
            _punchStartTime = Time.time;
            if (_punchStartTime - _lastExitTime > DelayBeforeSeriesStop)
            {
                _numberIfSeries = 0;
            }
            else
            {
                _numberIfSeries++;
                if (_numberIfSeries > LimitInSeries) _numberIfSeries = 0;
                _numberIfSeries = Mathf.Min(_numberIfSeries, LimitInSeries);
            }
            _animationController.SetLayerWeight(CharacterAnimationLayer.Combat, 1f);
            _animationController.PunchAnimation(_numberIfSeries);
        }

        public override void OnExit()
        {
            _lastExitTime = Time.time;
            _animationController.SetLayerWeight(CharacterAnimationLayer.Combat, 0f);
        }

        public bool IsReadyToSwitch() => Time.time - _punchStartTime > TimeBeforeExit;
    }
}