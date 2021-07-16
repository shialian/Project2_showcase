using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

[DisallowMultipleComponent]
[RequireComponent(typeof(NetworkManager))]
public class MyHUD : MonoBehaviour
{
    NetworkManager manager;
    void Awake()
    {
        manager = GetComponent<NetworkManager>();
    }
    // Start server + client
    public void Start_Server(){
        manager.StartHost();
    }
    // Start client only
    public void Start_Cient(){
        Debug.Log("Connect to : " + manager.networkAddress);
        manager.StartClient();
    }
}
