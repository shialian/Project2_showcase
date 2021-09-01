using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class GondoraControl : NetworkBehaviour
{
    public TriggerItem trigger;
    public Vector3 initialPosistion = Vector3.zero;
    public float speedFactor = 5f;
    public Task task;

    private Transform localPlayer;
    private bool onTheGondora;

    private void Awake()
    {
        localPlayer = null;
        onTheGondora = false;
    }

    private void FixedUpdate()
    {
        if(NetworkClient.ready && localPlayer == null)
        {
            localPlayer = GameManager.singleton.localPlayer;
        }
        if (trigger.startOperation && trigger.attachedPlayer == localPlayer)
        {
            if (onTheGondora == false)
            {
                localPlayer.GetComponent<LocalPlayer>().SetPositionByOther(initialPosistion);
                localPlayer.GetComponent<OVRPlayerController>().enabled = false;
                localPlayer.GetComponent<CharacterController>().enabled = false;
                localPlayer.parent = this.transform;
                onTheGondora = true;
            }
            if (OVRInput.Get(OVRInput.Button.Three))
            {
                Swing(1);
            }
            else if (OVRInput.Get(OVRInput.Button.Four))
            {
                Swing(-1);
            }
        }
        if (task.taskComplete)
        {
            ResetAll();
        }
    }

    private void Swing(int direction)
    {
        Vector3 rotation = this.transform.rotation.eulerAngles;
        transform.Rotate(direction * speedFactor * Time.fixedDeltaTime, 0f, 0f);
        rotation = this.transform.rotation.eulerAngles;
        if(rotation.x > 70f && rotation.x < 290f && direction == 1)
        {
            rotation.x = 70f;
            transform.rotation = Quaternion.Euler(rotation);
        }
        if (rotation.x < 290f && rotation.x > 70 && direction == -1)
        {
            rotation.x = 290f;
            transform.rotation = Quaternion.Euler(rotation);
        }
    }

    private void ResetAll()
    {
        trigger.SetRideStart(false);
        transform.rotation = Quaternion.identity;
        localPlayer.parent = null;
        localPlayer.GetComponent<LocalPlayer>().SetPositionByOther(trigger.transform.position + trigger.transform.forward);
        trigger.gameObject.SetActive(false);
        localPlayer.GetComponent<OVRPlayerController>().enabled = true;
        localPlayer.GetComponent<CharacterController>().enabled = true;
        this.enabled = false;
    }
}
