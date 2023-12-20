using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class ObjectOfGravity : MonoBehaviour
{
    [Inject] private Antigravity _antigravity;
    public bool InSphere;
    public float Force;
    public float VerticalPosition;

    Vector3 antiGravityForce;
    Vector3 positionOfZeroGravity;
    Rigidbody _rigidbody;
    bool isGravity;

    void Start()
    {
        _antigravity.AddObject(this);
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.drag = 0.5f;
        positionOfZeroGravity = Vector3.up;

        //Force = 20;
        //VerticalPosition = 1;
    }

    void FixedUpdate()
    {
        if(isGravity && InSphere)
        {
            positionOfZeroGravity = new Vector3(transform.position.x, VerticalPosition, transform.position.z);
            antiGravityForce = positionOfZeroGravity + Vector3.up/2 - transform.position;
            _rigidbody.AddForce(antiGravityForce * Force);
        }
        float distanceToCenter = Vector3.Distance(transform.position, _antigravity._SphereGravty.transform.position);
        if (distanceToCenter > _antigravity._SphereGravty.transform.lossyScale.x / 2)
        {
            InSphere = false;
        if(CompareTag("Player")) VerticalPosition = _antigravity._SphereGravty.transform.position.y;
        }
        else if (_antigravity.IsAntigravity)
            InSphere = true;
    }

    public void SetGravity(bool value)
    {
        isGravity = value;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position+positionOfZeroGravity, transform.position + antiGravityForce);
    }
}
