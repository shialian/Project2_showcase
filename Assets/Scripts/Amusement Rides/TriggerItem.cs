﻿using System.Collections;
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("GameStarter"))
        {
            GameStartButton button = other.GetComponent<GameStartButton>();
            other.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            localPlayer = GameManager.singleton.localPlayer;
            if (button.player == localPlayer)
            {
                SetTriggeredPlayer(localPlayer);
                SetAttachedPlayer(localPlayer);
                if (hideLaserBeam == false)
                {
                    localPlayer.GetComponent<LocalPlayer>().showLaserBeam = true;
                }
            }
            else
            {
                //SetAttachedPlayer(localPlayer);
            }
            SetRideStart(true);
        }
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
