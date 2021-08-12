using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugLog : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        Debug.LogError(collision.collider);
    }
}
