using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

public class LightPoint : MonoBehaviour
{
    public float vibrationTime = 2.0f;
    public Image loadingCircle;
    public Transform FX;

    [SerializeField]
    private Transform lookatTarget = null;
    private bool setVirbration = false;
    private Transform localPlayer;
    private GameObject laserBeam;

    private void Awake()
    {
        loadingCircle.fillAmount = 0;
        laserBeam = null;
    }

    private void FixedUpdate()
    {
        if(localPlayer == null && GameManager.singleton.localPlayer)
        {
            localPlayer = GameManager.singleton.localPlayer;
        }
        if(lookatTarget == null)
        {
            Invoke("SetLookTarget", 0.1f);
        }
        if (setVirbration && loadingCircle.fillAmount < 1)
        {
            loadingCircle.fillAmount += Time.fixedDeltaTime / vibrationTime;
            FX.localRotation = Quaternion.Euler(0, 180, loadingCircle.fillAmount * 360f);
        }
        else if(loadingCircle.fillAmount >= 1)
        {
            transform.parent.GetComponent<Task>().CmdCheckTaskComplete();
        }
    }

    private void SetLookTarget()
    {
        lookatTarget = GameObject.Find("CenterEyeAnchor").transform;
    }

    private void OnTriggerEnter(Collider other)
    {
        setVirbration = true;
        transform.LookAt(lookatTarget.position);
        if (loadingCircle.fillAmount < 1)
        {
            for (int i = 0; i <= vibrationTime; i++)
            {
                Invoke("SetVirbration", i);
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (loadingCircle.fillAmount >= 1)
        {
            setVirbration = false;
            StopVirbration();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        setVirbration = false;
        StopVirbration();
    }

    private void SetVirbration()
    {
        if (setVirbration)
        {
            laserBeam = localPlayer.GetComponent<LocalPlayer>().laserBeam;
            if(laserBeam.transform.parent.name == "RightHandAnchor")
                OVRInput.SetControllerVibration(10.0f, 0.3f, OVRInput.Controller.RTouch);
            else
                OVRInput.SetControllerVibration(10.0f, 0.3f, OVRInput.Controller.LTouch);
        }
    }

    private void StopVirbration()
    {
        laserBeam = localPlayer.GetComponent<LocalPlayer>().laserBeam;
        if (laserBeam.transform.parent.name == "RightHandAnchor")
            OVRInput.SetControllerVibration(0.0f, 0.0f, OVRInput.Controller.RTouch);
        else
            OVRInput.SetControllerVibration(0.0f, 0.0f, OVRInput.Controller.LTouch);
    }
}
