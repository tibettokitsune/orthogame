using Infrastructure;
using UnityEngine;
using UnityHFSM;

namespace Game.Scripts.Metagameplay.Player
{
    public class SimpleMovementState : State
    {
        private MoveAndRotation _moveAndRotation;
        private IPlayerInput _input;
        public SimpleMovementState(IPlayerInput input ,MoveAndRotation moveAndRotation)
        {
            _moveAndRotation = moveAndRotation;
            _input = input;
        }

        public override void OnLogic()
        {
            if(_input.JumpPressed()) _moveAndRotation.Jump();
            
            _moveAndRotation.CalculateMovementDirection(
                new Vector3(_input.Horizontal(), _input.Height(), _input.Vertical()));
        }
    }
}