using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class DropTowerControl : NetworkBehaviour
{
    public TriggerItem trigger;
    public Task task;

    private Transform localPlayer;

    private void Awake()
    {
        localPlayer = null;
    }


    private void FixedUpdate()
    {
        if(NetworkClient.ready && localPlayer == null)
        {
            localPlayer = GameManager.singleton.localPlayer;
        }
        if (trigger.startOperation && trigger.attachedPlayer == localPlayer)
        {
            if (OVRInput.Get(OVRInput.Button.Three))
            {
                ChairMoving(new Vector3(0f, 0f, 10f * Time.fixedDeltaTime));
            }
            else
            {
                ChairMoving(new Vector3(0f, 0f, -10f * Time.fixedDeltaTime));
            }
        }
        if (task.taskComplete && trigger.triggeredPlayer == localPlayer)
        {
            ResetAll();
        }
    }

    [Command(requiresAuthority = false)]
    public void ChairMoving(Vector3 offset)
    {
        if(offset.z > 0 &&transform.localPosition.z <= 10f)
        {
            transform.localPosition += offset;
        }
        if (offset.z < 0 && transform.localPosition.z >= -15.28f)
        {
            transform.localPosition += offset;
        }
    }

    private void ResetAll()
    {
        transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, -15.28f);
        localPlayer.parent = null;
        localPlayer.GetComponent<LocalPlayer>().SetPositionByOther(trigger.transform.position - trigger.transform.forward);
        localPlayer.GetComponent<LocalPlayer>().showLaserBeam = false;
        localPlayer.GetComponent<OVRPlayerController>().enabled = true;
        trigger.SetRideStart(false);
        trigger.transform.parent.gameObject.SetActive(false);
        this.enabled = false;
    }
}
