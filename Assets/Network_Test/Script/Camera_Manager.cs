using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Camera_Manager : NetworkBehaviour
{
    public Camera cma;
    void Awake()
    {
        Debug.Log(this.isLocalPlayer + " " + isLocalPlayer);
        if(!this.isLocalPlayer)
        {
            Debug.Log("Not Your camera");
            cma.enabled = false;
        }
    }
}
