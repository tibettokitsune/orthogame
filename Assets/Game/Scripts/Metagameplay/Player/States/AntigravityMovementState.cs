using Infrastructure;
using UnityEngine;
using UnityHFSM;

namespace Game.Scripts.Metagameplay.Player
{
    public class AntigravityMovementState : State
    {
        private MoveAndRotation _moveAndRotation;
        private IPlayerInput _input;
        private Antigravity _antigravity;
        public AntigravityMovementState(IPlayerInput input ,MoveAndRotation moveAndRotation, Antigravity antigravity)
        {
            _moveAndRotation = moveAndRotation;
            _input = input;
            _antigravity = antigravity;
        }

        public override void OnLogic()
        {
            if(_input.JumpPressed()) _moveAndRotation.Jump();
            
            _moveAndRotation.CalculateMovementDirection(
                new Vector3(_input.Horizontal(), _input.Height(), _input.Vertical()));
        }

        public override void OnEnter()
        {
            _antigravity.SetGravity();
        }

        public override void OnExit()
        {
            _antigravity.SetGravity();
        }
    }
}