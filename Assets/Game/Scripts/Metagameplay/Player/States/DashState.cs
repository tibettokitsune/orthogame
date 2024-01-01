using Infrastructure;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityHFSM;
using Zenject;
using UnityEngine.Events;
public class DashState : State
{
    public IPlayerInput _playerInput;
    public Rigidbody _rigidbody;
    [SerializeField] private float ForceDash = 10;
    private float DashCooldown = 0.5f;
    private float LastTimeDash;
    private float TimerDash;
    private Transform _transform;
    private StateMachine _fsm;
    private MoveAndRotation _moveAndRotation;
    public DashState(Transform transform, StateMachine fsm, IPlayerInput playerInput, MoveAndRotation moveAndRotation)
    {
        _transform = transform;
        _fsm = fsm;
        _playerInput = playerInput;
        _moveAndRotation = moveAndRotation;
        _rigidbody = _transform.GetComponent<Rigidbody>();
    }

    public override void OnExit()
    {
        if (Time.time - LastTimeDash > DashCooldown) LastTimeDash = Time.time;
    }

    public override void OnEnter()
    {
        TimerDash = Time.time + 0.2f;
    }
    public override void OnLogic()
    {
        Debug.Log(Time.time - LastTimeDash);
        if (Time.time - LastTimeDash < DashCooldown || TimerDash < Time.time)
            _fsm.Trigger("DashStateExit");
        else
        {
            Vector3 direction = new Vector3(_playerInput.Horizontal(), 0, _playerInput.Vertical());
            direction = _moveAndRotation.CalculateDirection(direction);
            _rigidbody.AddForce(direction.normalized * ForceDash / _rigidbody.velocity.magnitude, ForceMode.VelocityChange);
        }
    }
}
