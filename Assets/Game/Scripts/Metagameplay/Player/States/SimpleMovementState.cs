using Infrastructure;
using UnityEngine;
using UnityHFSM;

namespace Game.Scripts.Metagameplay.Player
{
    public class SimpleMovementState : State
    {
        private MoveAndRotation _moveAndRotation;
        private IPlayerInput _input;
        private CharacterAnimationController _animationController;
        private GroundChecker _groundChecker;
        public SimpleMovementState(IPlayerInput input ,MoveAndRotation moveAndRotation, 
            CharacterAnimationController characterAnimationController, GroundChecker groundChecker)
        {
            _moveAndRotation = moveAndRotation;
            _input = input;
            _animationController = characterAnimationController;
            _groundChecker = groundChecker;
        }

        public override void OnLogic()
        {
            if(_input.JumpPressed()) _moveAndRotation.Jump();

            var direction = new Vector3(_input.Horizontal(), 0, _input.Vertical());
            _moveAndRotation.CalculateMovementDirection(direction);
            _animationController.UpdateSpeed(direction.magnitude);
            _animationController.UpdateGrounded(_groundChecker.IsGrounded);
        }
    }
}