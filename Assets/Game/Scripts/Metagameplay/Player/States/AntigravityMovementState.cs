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
        private CharacterAnimationController _animationController;
        private float _enterTime;
        private const float Delay = 1f;
        private bool _isActive;
        public AntigravityMovementState(IPlayerInput input ,MoveAndRotation moveAndRotation, Antigravity antigravity, 
            CharacterAnimationController animationController)
        {
            _moveAndRotation = moveAndRotation;
            _input = input;
            _antigravity = antigravity;
            _animationController = animationController;
        }

        public override void OnLogic()
        {
            if(Time.time - _enterTime < Delay && !_isActive) return;
            if (!_isActive)
            {
                _isActive = true;
                _animationController.SetLayerWeight(CharacterAnimationLayer.Fly, 1f);
                _antigravity.SetGravity();
            }
            if(_input.JumpPressed()) _moveAndRotation.Jump();
            var dir = new Vector3(_input.Horizontal(), _input.Height(), _input.Vertical());
            _moveAndRotation.CalculateMovementDirection(dir);
            _animationController.UpdateSpeed(dir.magnitude);
        }

        public override void OnEnter()
        {
            _animationController.HeroPower();
            _enterTime = Time.time;
            _isActive = false;
        }

        public override void OnExit()
        {
            _antigravity.SetGravity();
            
            _animationController.SetLayerWeight(CharacterAnimationLayer.Fly, 0f);

        }
    }
}