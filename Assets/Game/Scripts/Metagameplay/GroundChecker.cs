using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class GroundChecker : MonoBehaviour
{
    public bool IsGrounded { get; private set; }
    
    public UnityAction<bool> OnGround;

    private void OnTriggerStay(Collider other)
    {
        OnGround?.Invoke(true);
        IsGrounded = true;
    }

    private void OnTriggerExit(Collider other)
    {
        OnGround?.Invoke(false);
        IsGrounded = false;
    }
}
