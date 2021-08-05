using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LightPoint : MonoBehaviour
{
    public float vibrationTime = 2.0f;
    public Image loadingBar;
    public Transform FX;

    [SerializeField]
    private Transform lookatTarget;
    private bool setVirbration = false;

    private void Awake()
    {
        lookatTarget = GameObject.Find("CenterEyeAnchor").transform;
        loadingBar.fillAmount = 0;
    }

    private void FixedUpdate()
    {
        if(setVirbration && loadingBar.fillAmount < 1)
        {
            loadingBar.fillAmount += Time.fixedDeltaTime / vibrationTime;
            FX.localRotation = Quaternion.Euler(0, 180, loadingBar.fillAmount * 360f);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        setVirbration = true;
        transform.LookAt(lookatTarget.position);
        if (loadingBar.fillAmount < 1)
        {
            for (int i = 0; i <= vibrationTime; i++)
            {
                Invoke("SetVirbration", i);
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (loadingBar.fillAmount >= 1)
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
            OVRInput.SetControllerVibration(10.0f, 0.3f, OVRInput.Controller.RTouch);
        }
    }

    private void StopVirbration()
    {
        OVRInput.SetControllerVibration(0.0f, 0.0f, OVRInput.Controller.RTouch);
    }
}
