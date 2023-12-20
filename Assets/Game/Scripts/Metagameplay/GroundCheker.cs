using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class GroundCheker : MonoBehaviour
{
    //public bool isGround { private set; get; }
    public UnityAction<bool> OnGround;

    private void OnTriggerStay(Collider other) => OnGround?.Invoke(true);

    private void OnTriggerExit(Collider other) => OnGround?.Invoke(false);
}
