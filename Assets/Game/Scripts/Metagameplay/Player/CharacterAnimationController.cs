using UnityEngine;
using Zenject;

namespace Game.Scripts.Metagameplay.Player
{
    public enum CharacterAnimationLayer
    {
        Default = 0,
        Fly = 1
    }
    public class CharacterAnimationController : MonoBehaviour
    {
        [Inject] private Animator _animator;
        private static readonly int Speed = Animator.StringToHash("Speed");
        private static readonly int Power = Animator.StringToHash("Power");
        private static readonly int OnGround = Animator.StringToHash("OnGround");

        private void OnValidate()
        {
            if (!_animator) _animator = GetComponent<Animator>();
        }

        public void UpdateSpeed(float speed) => _animator.SetFloat(Speed, speed);

        public void UpdateGrounded(bool grounded) => _animator.SetBool(OnGround, grounded);

        public void HeroPower() => _animator.SetTrigger(Power);

        public void DisableHeroPower()
        {
            throw new System.NotImplementedException();
        }

        public void SetLayerWeight(CharacterAnimationLayer currentLayer, float p1)
        {
            _animator.SetLayerWeight((int)currentLayer, p1);
        }
    }
}