using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fan : MonoBehaviour
{
    [SerializeField][Range(50,200)] int ForceFan = 50;
    Rigidbody _rigibody;
    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<Rigidbody>() != null)
        {
            _rigibody = other.GetComponent<Rigidbody>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        _rigibody = null;
    }

    private void FixedUpdate()
    {
        if(_rigibody != null)
        {
            _rigibody.AddForce(transform.right* ForceFan);
        }
    }
}
