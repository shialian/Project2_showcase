using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugLog : MonoBehaviour
{
    public Transform leftController;
    public Transform rightController;
    private void Update()
    {
        if (OVRInput.Get(OVRInput.Button.Two))
        {
            Debug.LogError("left: " + leftController.position + " right:" + rightController.position);
        }
    }
}
