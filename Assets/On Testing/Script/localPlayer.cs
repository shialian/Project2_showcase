using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class LocalPlayer : NetworkBehaviour
{
    public GameObject cameraAnchor;
    public GameObject character;

    void Start()
    {
        if (!isLocalPlayer)
        {
            Debug.LogError(character);
            GetComponent<OVRCameraRig>().enabled = false;
            GetComponent<OVRHeadsetEmulator>().enabled = false;
            GetComponent<OVRPlayerController>().enabled = false;
            character.GetComponent<VRAnimatorController>().enabled = false;
            cameraAnchor.SetActive(false);
        }
    }
}
