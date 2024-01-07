using Infrastructure;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Metagameplay.Player
{
    public class Dash : MonoBehaviour
    {
        [Inject] IPlayerInput playerInput;
        [SerializeField]private float ForceDash = 100000f;
        [Inject] private Rigidbody _rigidbody;
        [Inject] MoveAndRotation _moveAndRotation;
        private float DashCooldown = 2f;
        private float LastTimeDash;
        private float TimerDash;

        public void makeDash()
        {
                LastTimeDash = Time.time;
                TimerDash = Time.time + 0.2f;
            _moveAndRotation.enabled = false;
        }
        private void Update()
        {
            if ((Time.time - LastTimeDash > DashCooldown))
                if (playerInput.ShiftPressed()) makeDash();
        }

        private void FixedUpdate()
        {
            if (TimerDash > Time.time)
            {
                print(_rigidbody.velocity.magnitude);
                   Vector3 dash = transform.forward * ForceDash;
                if (_rigidbody.velocity.magnitude != 0) dash = dash / _rigidbody.velocity.magnitude;
                _rigidbody.AddForce(dash, ForceMode.VelocityChange);

                if (TimerDash < Time.time + Time.fixedDeltaTime)
                {
                    _moveAndRotation.enabled = true;
                }
            }
        }
    }
}
