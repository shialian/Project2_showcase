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
    
    public TriggerItem trigger;

    public AudioSource source;
    public AudioClip completedSound;

    [SyncVar]
    public bool taskComplete = false;

    private bool lightPointTriggered = false;

    private void Awake()
    {
        bwIcon.SetActive(true);
        colorIcon.SetActive(false);
        rideSign.GetComponent<RawImage>().material = notClearSign;
    }

    private void Start()
    {
        for(int i = 0; i < lightPoints.Length; i++)
        {
            lightPoints[i].SetLightPointID(i);
            if (trigger != null)
            {
                lightPoints[i].gameObject.SetActive(false);
            }
        }
    }

    private void Update()
    {
        if (trigger != null)
        {
            if (trigger.startOperation && lightPointTriggered == false)
            {
                for (int i = 0; i < lightPoints.Length; i++)
                {
                    lightPoints[i].gameObject.SetActive(true);
                }
                lightPointTriggered = true;
            }
        }
    }

    [Command(requiresAuthority = false)]
    public void CmdCheckTaskComplete(int id)
    {
        bool checkComplete = true;
        RpcDisableLightPoint(id);
        for (int i = 0; i < lightPoints.Length; i++)
        {
            if (lightPoints[i].completed == false && i != id)
            {
                checkComplete = false;
            }
        }
        taskComplete = checkComplete;
        if (taskComplete)
        {
            RpcSetComplete();
        }
    }

    [ClientRpc]
    public void RpcDisableLightPoint(int i)
    {
        lightPoints[i].completed = true;
        source.PlayOneShot(completedSound);
        lightPoints[i].gameObject.SetActive(false);
    }

    [ClientRpc]
    public void RpcSetComplete()
    {
        bwIcon.SetActive(false);
        colorIcon.SetActive(true);
        rideSign.GetComponent<RawImage>().material = clearSign;
        trigger.SetRideStart(false);
    }
}
