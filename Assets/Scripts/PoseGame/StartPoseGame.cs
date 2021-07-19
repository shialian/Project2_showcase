using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class StartPoseGame : NetworkBehaviour
{
    public PoseGameManager GM;
    private OVRGrabbable Trigger;
    // Start is called before the first frame update
    void Start()
    {
        Trigger = this.GetComponent<OVRGrabbable>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Trigger.isGrabbed == true)
        {
            if (!GM.is_start)
            {
                Debug.Log("Start Pose Game : " + GM.name);
                StartGame();
            }
        }
    }
    void StartGame(){
        GM.StartGame();
    }
}
