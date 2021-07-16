using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using Oculus;

public class TakePictureManager : NetworkBehaviour
{
    // using diff camera on same rnader texture
    // and set these camera false through open camera get picture
    public Camera P1_Camera;
    public Camera P2_Camera;
    // the pose game need complete to take picture
    public PoseGameManager game;
    // the item grabbed to take picture 
    public OVRGrabbable TriggerItem;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        DetectGrab();
    }
    void DetectGrab(){
        if( game.is_complete && TriggerItem.isGrabbed){
            Player palyer = TriggerItem.grabbedBy.transform.root.gameObject.GetComponent<Player>();
            CmdTake(palyer.PlayerID);
        }
    }
    [Command(requiresAuthority = false)]
    public void CmdTake(int PlayerID){
        Debug.Log("Smile!");
        RpcTake(PlayerID);
    }
    [ClientRpc]
    void RpcTake(int PlayerID){
        Debug.Log("Take a picture of player" + PlayerID);
        if(PlayerID == 0){
            P1_Camera.enabled = true;
            Invoke("CloseCamera1",0.5f);
            // P1_Camera.enabled = false;
        }
        else if(PlayerID == 1){
            P2_Camera.enabled = true;
            Invoke("CloseCamera2",0.5f);
            // P2_Camera.enabled = false;
        }
    }
    void CloseCamera1(){
        P1_Camera.enabled = false;
    }
    void CloseCamera2(){
        P2_Camera.enabled = false;
    }
}
