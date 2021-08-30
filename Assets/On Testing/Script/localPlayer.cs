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

    private GameObject guideMap;
    private GameObject dialogue;
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
            guideMap = GameObject.Find("Guide Map");
            guideMap.SetActive(false);

            dialogue = GameObject.Find("Dialogue");
            SetUITransform(dialogue.transform);
            dialogue.SetActive(true);

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
            if (guideMap.activeSelf == false)
            {
                SetUITransform(guideMap.transform);
                laserBeam.SetActive(true);
                guideMap.SetActive(true);
            }
            else
            {
                laserBeam.SetActive(false);
                guideMap.SetActive(false);
            }
        }
        if(OVRInput.GetDown(OVRInput.Button.Two, OVRInput.Controller.RTouch))
        {
            if (guideMap.activeSelf)
            {
                SetUITransform(guideMap.transform);
            }
            if (dialogue.activeSelf)
            {
                SetUITransform(dialogue.transform);
            }
        }
    }

    private void SetUITransform(Transform ui)
    {
        ui.position = cameraAnchor.transform.position + lookDistance * cameraAnchor.transform.forward;
        ui.LookAt(cameraAnchor.transform);
        ui.Rotate(0f, 180f, 0f);
    }

    public void SetPositionByOther(Vector3 position)
    {
        transform.GetComponent<CharacterController>().enabled = false;
        transform.position = position;
        transform.GetComponent<CharacterController>().enabled = true;
    }
}
