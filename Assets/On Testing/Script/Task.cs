using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

public class Task : NetworkBehaviour
{
    public LightPoint[] lightPoints;
    public GameObject bwIcon;
    public GameObject colorIcon;
    public Material notClearSign;
    public Material clearSign;
    public GameObject rideSign;

    [SyncVar, HideInInspector]
    public bool taskComplete;

    private void Awake()
    {
        taskComplete = true;
        bwIcon.SetActive(true);
        colorIcon.SetActive(false);
        rideSign.GetComponent<RawImage>().material = notClearSign;
    }

    [Command(requiresAuthority = false)]
    public void CmdCheckTaskComplete()
    {
        for(int i = 0; i < lightPoints.Length; i++)
        {
            if (lightPoints[i].loadingCircle.fillAmount < 1)
            {
                taskComplete = false;
            }
            else
            {
                RpcDisableLightPoint(i);
            }
        }
        if (taskComplete)
        {
            RpcSetComplete();
        }
    }

    [ClientRpc]
    public void RpcDisableLightPoint(int i)
    {
        lightPoints[i].gameObject.SetActive(false);
    }

    [ClientRpc]
    public void RpcSetComplete()
    {
        bwIcon.SetActive(false);
        colorIcon.SetActive(true);
        rideSign.GetComponent<RawImage>().material = clearSign;
    }
}
