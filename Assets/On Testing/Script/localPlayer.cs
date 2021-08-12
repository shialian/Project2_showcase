using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class LocalPlayer : MonoBehaviour//NetworkBehaviour
{
    public GameObject cameraAnchor;
    public GameObject character;
    public float lookDistance = 5f;

    [SerializeField]
    private GameObject UI;
    private GameObject laserBeam;

    private void Start()
    {
        UI = GameObject.Find("UI");
        UI.SetActive(false);
        laserBeam = GameObject.Find("LaserBeam");
        laserBeam.SetActive(false);
        /*if (!isLocalPlayer)
        {
            GetComponent<OVRCameraRig>().enabled = false;
            GetComponent<OVRHeadsetEmulator>().enabled = false;
            GetComponent<OVRPlayerController>().enabled = false;
            character.GetComponent<VRAnimatorController>().enabled = false;
            cameraAnchor.SetActive(false);
            this.enabled = false;
        }*/
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
}
