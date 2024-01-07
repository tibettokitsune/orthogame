using Infrastructure;
using System.Collections;
using System.Collections.Generic;
using Game.Scripts.Metagameplay.Player;
using UnityEngine;
using UnityHFSM;
using Zenject;
using UnityEngine.Events;
public class DashState : State
{
    private IPlayerInput _playerInput;
    private Rigidbody _rigidbody;
    [SerializeField] private float ForceDash = 100;
    private float DashCooldown = 0.2f;
    private float LastTimeDash;
    private float TimerDash;
    private Transform _transform;
    private StateMachine _fsm;
    private MoveAndRotation _moveAndRotation;
    private CharacterAnimationController _animationController;
    public DashState(Transform transform, IPlayerInput playerInput, MoveAndRotation moveAndRotation,
        CharacterAnimationController animationController)
    {
        _transform = transform;
        _playerInput = playerInput;
        _moveAndRotation = moveAndRotation;
        _rigidbody = _transform.GetComponent<Rigidbody>();
        _animationController = animationController;
    }

    public override void OnExit()
    {
        if (Time.time - LastTimeDash > DashCooldown) LastTimeDash = Time.time;
    }

    public override void OnEnter()
    {
        TimerDash = Time.time + 0.2f;
        _animationController.DashAnimation();
    }
    public override void OnLogic()
    {
        if (!IsDashFinished())
        {
            Vector3 direction = new Vector3(_playerInput.Horizontal(), 0, _playerInput.Vertical());
            direction = _moveAndRotation.CalculateDirection(direction);

            Vector3 dash = direction * ForceDash;
            if (_rigidbody.velocity.magnitude != 0) dash = dash / _rigidbody.velocity.magnitude;
            _rigidbody.AddForce(direction, ForceMode.VelocityChange);
        }
    }

    public bool IsDashFinished()
    {
        return (Time.time - LastTimeDash < DashCooldown || TimerDash < Time.time);
    }
}
