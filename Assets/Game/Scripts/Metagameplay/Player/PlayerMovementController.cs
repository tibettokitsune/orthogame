using System;
using Infrastructure;
using UnityEngine;
using UnityHFSM;
using Zenject;

namespace Game.Scripts.Metagameplay.Player
{
    public class PlayerMovementController : MonoBehaviour
    {
        [Inject] private IPlayerInput _playerInput;
        [Inject] private MoveAndRotation _moveAndRotation;
        [Inject] private Antigravity _antigravity;
        private StateMachine _fsm;
        private void Start()
        {
            _fsm = new StateMachine();
            _fsm.AddState("SimpleMovement",new SimpleMovementState(_playerInput, _moveAndRotation));
            _fsm.AddState("AntigravityMovement",new AntigravityMovementState(_playerInput, _moveAndRotation, _antigravity));

            _fsm.AddTransition(new Transition("SimpleMovement", "AntigravityMovement", 
                t => _playerInput.RPressed()));
            _fsm.AddTransition(new Transition( "AntigravityMovement","SimpleMovement", 
                t => _playerInput.RPressed()));
            _fsm.AddTransition(new Transition( "AntigravityMovement","SimpleMovement", 
                t => !_antigravity.IsObjectInAntigravityZone(transform.position)));

            _fsm.SetStartState("SimpleMovement");
            _fsm.Init();
        }

        private void Update()
        {
            _fsm.OnLogic();
        }
    }
}