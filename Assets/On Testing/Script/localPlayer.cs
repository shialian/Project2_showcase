using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CurvedUI;
using Mirror;

public class LocalPlayer : NetworkBehaviour
{
    public GameObject cameraAnchor;
    public GameObject character;
    public float lookDistance = 5f;

    [HideInInspector]
    public GameObject laserBeam;

    private GameObject UI;
    private CurvedUIInputModule cuiInputModule;

    private void Start()
    {
        if (!isLocalPlayer)
        {
            GetComponent<OVRPlayerController>().enabled = false;
            character.GetComponent<VRAnimatorController>().enabled = false;
            cameraAnchor.SetActive(false);
            this.enabled = false;
        }
        else
        {
            UI = GameObject.Find("UI");
            UI.SetActive(false);
            laserBeam = GameObject.Find("LaserBeam");
            laserBeam.SetActive(false);
            cuiInputModule = GameObject.Find("EventSystem").GetComponent<CurvedUIInputModule>();
            cuiInputModule.OculusCameraRig = GetComponent<OVRCameraRig>();
        }
    }

    private void Update()
    {
        if(OVRInput.GetDown(OVRInput.Button.One, OVRInput.Controller.RTouch))
        {
            if (UI.activeSelf == false)
            {
                SetUITransform();
                laserBeam.SetActive(true);
                UI.SetActive(true);
            }
            else
            {
                laserBeam.SetActive(false);
                UI.SetActive(false);
            }
        }
    }

    private void SetUITransform()
    {
        UI.transform.position = cameraAnchor.transform.position + lookDistance * cameraAnchor.transform.forward;
        UI.transform.LookAt(cameraAnchor.transform);
        UI.transform.Rotate(0f, 180f, 0f);
    }

    public void SetPositionByOther(Vector3 position)
    {
        transform.GetComponent<CharacterController>().enabled = false;
        transform.position = position;
        transform.GetComponent<CharacterController>().enabled = true;
    }
}
