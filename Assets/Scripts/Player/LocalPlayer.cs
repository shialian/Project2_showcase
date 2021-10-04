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
    [HideInInspector]
    public bool showLaserBeam = false;

    private GameObject guideMap;
    private GameObject dialogue;
    private GameObject annocements;
    private CurvedUIInputModule cuiInputModule;

    private void Start()
    {
        if (!isLocalPlayer)
        {
            GetComponent<OVRPlayerController>().enabled = false;
            character.GetComponent<VRAnimatorController>().enabled = false;
            GetComponent<OVRHeadsetEmulator>().enabled = false;
            this.transform.GetChild(0).GetChild(4).gameObject.SetActive(false);
            this.transform.GetChild(0).GetChild(5).gameObject.SetActive(false);
            cameraAnchor.SetActive(false);
            this.enabled = false;
        }
        else
        {
            GetComponent<OVRPlayerController>().enabled = false;

            guideMap = GameObject.Find("GuideMap");
            guideMap.SetActive(false);

            dialogue = GameObject.Find("Dialogue");
            SetUITransform(dialogue.transform);
            if (GameManager.singleton.isEnding)
            {
                dialogue.SetActive(true);
            }
            else
            {
                dialogue.SetActive(false);
            }

            annocements = GameObject.Find("Announcement");
            SetUITransform(annocements.transform);
            if (GameManager.singleton.isEnding)
            {
                annocements.SetActive(false);
            }
            else
            {
                annocements.SetActive(true);
            }

            laserBeam = GameObject.Find("LaserBeam");
            laserBeam.SetActive(true);

            cuiInputModule = GameObject.Find("EventSystem").GetComponent<CurvedUIInputModule>();
            cuiInputModule.OculusCameraRig = GetComponent<OVRCameraRig>();
        }
    }

    private void Update()
    {
        if(OVRInput.GetDown(OVRInput.Button.One, OVRInput.Controller.RTouch) && GameManager.singleton.isEnding == false)
        {
            if (guideMap.activeSelf == false)
            {
                SetUITransform(guideMap.transform);
                guideMap.SetActive(true);
            }
            else
            {
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
            if (annocements.activeSelf)
            {
                SetUITransform(annocements.transform);
            }
        }
        if(guideMap.activeSelf || dialogue.activeSelf || annocements.activeSelf || showLaserBeam)
        {
            laserBeam.SetActive(true);
        }
        else
        {
            laserBeam.SetActive(false);
        }
        if(isLocalPlayer && cuiInputModule.OculusCameraRig == null)
        {
            cuiInputModule.OculusCameraRig = GetComponent<OVRCameraRig>();
        }
    }

    public void SetUITransform(Transform ui)
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

    public void StartGame()
    {
        GetComponent<OVRPlayerController>().enabled = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.LogError(collision.collider);
    }
}
