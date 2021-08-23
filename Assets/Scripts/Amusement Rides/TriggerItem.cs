using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class TriggerItem : NetworkBehaviour
{
    [SyncVar]
    public bool startOperation = false;
    [SyncVar]
    public Transform attachedPlayer = null;
    [SyncVar]
    public Transform triggeredPlayer = null;

    public bool hideLaserBeam = false;

    private Transform localPlayer = null;
    private GameObject laserBeam;

    private void Start()
    {
        laserBeam = null;
    }

    private void Update()
    {
        if(hideLaserBeam == false && triggeredPlayer != null && triggeredPlayer == localPlayer && laserBeam != null)
        {
            laserBeam.SetActive(startOperation);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        localPlayer = GameManager.singleton.localPlayer;
        if (other.transform.parent.parent == localPlayer)
        {
            SetTriggeredPlayer(localPlayer);
            SetAttachedPlayer(localPlayer);
        }
        else
        {
            SetAttachedPlayer(localPlayer);
        }
        laserBeam = localPlayer.GetComponent<LocalPlayer>().laserBeam;
        SetRideStart(true);
    }

    [Command(requiresAuthority = false)]
    public void SetTriggeredPlayer(Transform player)
    {
        triggeredPlayer = player;
    }

    [Command(requiresAuthority = false)]
    public void SetAttachedPlayer(Transform player)
    {
        attachedPlayer = player;
    }

    [Command(requiresAuthority = false)]
    public void SetRideStart(bool flag)
    {
        startOperation = flag;
    }
}
