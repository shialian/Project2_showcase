using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class TriggerItem : NetworkBehaviour
{
    [SyncVar]
    public bool startOperation = false;
    public Transform attachedPlayer = null;
    public Transform triggeredPlayer = null;

    public bool hideLaserBeam = false;

    private bool isTriggered = false;
    private Transform localPlayer = null;

    private void Update()
    {
        if(GameManager.singleton && localPlayer == null)
        {
            localPlayer = GameManager.singleton.localPlayer;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("GameStarter"))
        {
            GameStartButton button = other.GetComponent<GameStartButton>();
            other.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            isTriggered = true;
            CmdSetPlayers();
            if (hideLaserBeam == false)
            {
                localPlayer.GetComponent<LocalPlayer>().showLaserBeam = true;
            }
            SetRideStart(true);
        }
    }

    [Command(requiresAuthority = false)]
    public void CmdSetPlayers()
    {
        RpcSetPlayers();
    }

    [ClientRpc]
    public void RpcSetPlayers()
    {
        if (isTriggered)
        {
            triggeredPlayer = localPlayer;
        }
        else
        {
            attachedPlayer = localPlayer;
        }
    }

    [Command(requiresAuthority = false)]
    public void SetRideStart(bool flag)
    {
        startOperation = flag;
    }
}
