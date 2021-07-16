using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Debug.LogError(hit.gameObject);
    }
}
