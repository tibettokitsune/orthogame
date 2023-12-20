using System;
using System.Collections;
using System.Collections.Generic;
using Infrastructure;
using UnityEngine;
using Zenject;

public class Stick : MonoBehaviour
{
    [Inject] private Antigravity _antigravity;
    [Inject] private IPlayerInput _playerInput;
    [SerializeField]Rigidbody playerRB;
    [SerializeField]bool isStick;
    Vector3 _bufferPosition;
    Vector3 _bufferRotation;
    private void Start()
    {
        // InputSystem.instance.ButtonE += Cling;
        // InputSystem.instance.Jump += Jump;
        // InputSystem.instance.ButtonR += () => 
        // {
        //     if (playerRB != null) playerRB.GetComponent<MoveAndRotation>().enabled = true;
        //     SetStick(false);
        // };
    }

    private void Update()
    {
        if(_playerInput.EPressed()) Cling();
        if(_playerInput.JumpPressed()) Jump();
        if (_playerInput.RPressed())
        {
            if (playerRB != null) playerRB.GetComponent<MoveAndRotation>().enabled = true;
            SetStick(false);
        }
    }

    void Cling()
    {
        if (playerRB != null)
        {
            SetStick(!isStick);
            playerRB.GetComponent<MoveAndRotation>().enabled = !isStick;

            if (isStick == false)
            {
            }

            _bufferPosition = transform.InverseTransformPoint(playerRB.position);
            _bufferRotation = transform.InverseTransformDirection(playerRB.transform.forward);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerRB = other.GetComponent<Rigidbody>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (playerRB != null) playerRB.GetComponent<MoveAndRotation>().enabled = true;
            SetStick(false);
            playerRB = null;

        }
    }

    private void Jump()
    {
        if (isStick && !_antigravity.IsAntigravity)
        {
            playerRB.GetComponent<MoveAndRotation>().enabled = true;

            SetStick(false);
            playerRB.AddForce(Vector3.up * 200, ForceMode.Impulse);
        }
    }

    private void FixedUpdate()
    {
        if(isStick && playerRB!= null)
        {
            playerRB.MovePosition(transform.TransformPoint(_bufferPosition));
            playerRB.transform.forward = transform.TransformDirection(_bufferRotation);
            playerRB.velocity = Vector3.zero;
        }
    }

    void SetStick(bool value)
    {
        isStick = value;
        _antigravity.isStick = value;
    }
}
