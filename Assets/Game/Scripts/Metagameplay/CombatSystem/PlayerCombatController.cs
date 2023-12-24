using System;
using System.Collections.Generic;
using Game.Scripts.Metagameplay.CombatSystem.States;
using Game.Scripts.Metagameplay.Player;
using Infrastructure;
using UnityEngine;
using UnityHFSM;
using Zenject;

namespace Game.Scripts.Metagameplay.CombatSystem
{
    public class PlayerCombatController : MonoBehaviour
    {
        [Inject] private IPlayerInput _playerInput;
        [Inject] private CharacterAnimationController _animationController;
        [Inject] private List<DamagePoint> _damagePoints;
        private StateMachine _fsm;

        private PunchState _punchState;
        private void Start()
        {
            _fsm = new StateMachine();

            _fsm.AddState("Idle", new State());
            _punchState = new PunchState(_animationController, _damagePoints);
            _fsm.AddState("Punch", _punchState);
            
            _fsm.AddTransition("Idle", "Punch", v => _playerInput.AttackPressed());
            _fsm.AddTransition("Punch","Idle",  
                v => _punchState.IsReadyToSwitch() && !_playerInput.AttackPressed());

            _fsm.Init();
        }

        private void Update()
        {
            _fsm.OnLogic();
        }
    }
}