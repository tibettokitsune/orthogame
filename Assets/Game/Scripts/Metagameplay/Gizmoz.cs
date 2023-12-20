using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gizmoz : MonoBehaviour
{
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.gray;
        Gizmos.DrawLine(transform.position,transform.position + transform.forward);
    }
}
