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
        [Inject] private CharacterAnimationController _animationController;
        [Inject] private GroundChecker _groundChecker;
        private StateMachine _fsm;
        private void Start()
        {
            _fsm = new StateMachine();
            DashState dashState = new DashState(transform, _playerInput, _moveAndRotation);
            _fsm.AddState("SimpleMovement",new SimpleMovementState(_playerInput, _moveAndRotation, _animationController, _groundChecker));
            _fsm.AddState("AntigravityMovement",new AntigravityMovementState(_playerInput, _moveAndRotation, _antigravity, _animationController));
            _fsm.AddState("DashMovement", dashState);

            _fsm.AddTransition(new Transition("SimpleMovement", "DashMovement",
                t => _playerInput.ShiftPressed()));
            _fsm.AddTransition(new Transition("DashMovement", "SimpleMovement",
                t => dashState.IsDashFinished()));

            _fsm.AddTransition(new Transition("SimpleMovement", "AntigravityMovement", 
                t => _playerInput.RPressed()));
            _fsm.AddTransition(new Transition( "AntigravityMovement","SimpleMovement", 
                t => _playerInput.RPressed() 
                     || !_antigravity.IsObjectInAntigravityZone(transform.position)));

            _fsm.SetStartState("SimpleMovement");
            _fsm.Init();
        }

        private void Update()
        {
            _fsm.OnLogic();
        }
    }
}