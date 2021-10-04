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
    public bool multiPlayerTask = false;
    
    public TriggerItem trigger;

    public AudioSource source;
    public AudioClip completedSound;

    [SyncVar]
    public bool taskComplete = false;
    public SyncList<bool> playerReady= new SyncList<bool>();

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
        if (isServer)
        {
            playerReady.Add(false);
            playerReady.Add(false);
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

    public void CheckAndSetVibration(Collider other)
    {
        bool allAreTriggered = true;
        if(GameManager.singleton.playerID == 0)
        {
            if(lightPoints[0].isTriggered == false || lightPoints[1].isTriggered == false)
            {
                allAreTriggered = false;
            }
        }
        else
        {
            if (lightPoints[2].isTriggered == false || lightPoints[3].isTriggered == false)
            {
                allAreTriggered = false;
            }
        }
        if (allAreTriggered)
        {
            PlayerTriggerReady(GameManager.singleton.playerID, true);
        }
        else
        {
            PlayerTriggerReady(GameManager.singleton.playerID, false);
        }
    }

    [Command(requiresAuthority = false)]
    public void PlayerTriggerReady(int id, bool flag)
    {
        playerReady[id] = flag;
    }

    [Command(requiresAuthority = false)]
    public void CmdCheckTaskComplete(int id)
    {
        bool checkComplete = true;
        RpcDisableLightPoint(id);
        if (multiPlayerTask)
        {
            if (GameManager.singleton.playerID == 0)
            {
                if (lightPoints[0].gameObject.activeSelf || lightPoints[1].gameObject.activeSelf)
                {
                    checkComplete = false;
                }
            }
            if (GameManager.singleton.playerID == 1)
            {
                if (lightPoints[2].gameObject.activeSelf || lightPoints[3].gameObject.activeSelf)
                {
                    checkComplete = false;
                }
            }
        }
        else
        {
            for (int i = 0; i < lightPoints.Length; i++)
            {
                if (lightPoints[i].completed == false && i != id)
                {
                    checkComplete = false;
                }
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
    }
}
