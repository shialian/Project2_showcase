using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class LocalPlayer : NetworkBehaviour
{
    public GameObject cameraAnchor;
    public GameObject character;

    // Start is called before the first frame update
    void Start()
    {
        if (!isLocalPlayer)
        {
            GetComponent<OVRCameraRig>().enabled = false;
            GetComponent<OVRHeadsetEmulator>().enabled = false;
            GetComponent<OVRPlayerController>().enabled = false;
            GetComponent<CharacterController>().enabled = false;
            character.GetComponent<VRAnimatorController>().enabled = false;
            cameraAnchor.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}
