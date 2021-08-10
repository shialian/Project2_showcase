using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testing : MonoBehaviour
{
    public GameObject trackingSpace;
    public NetManager net;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            trackingSpace.SetActive(true);
            //net.Start_Server();
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            trackingSpace.SetActive(true);
            //net.Start_Cient();
        }
    }
}
