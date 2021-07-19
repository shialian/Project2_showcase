using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Mirror;
using System.Linq;

public class Player : NetworkBehaviour
{
    public GameObject Camera;
    // player ID which needs to be setted uniquely between diff palyer
    public int PlayerID;
    public GameObject character;
    Transform LeftHand;
    Transform RightHand;
    Transform Center;
    Vector3 OriginPosition;
    // variable use for tempUI to debug
    Direction left_dir;
    Direction right_dir;
    float angle = 0;
    void Start()
    {
        GetController();
        //close other player's camera
        if(!isLocalPlayer){
            Debug.LogWarning(this);
            //Destroy(Camera);
            GetComponent<OVRCameraRig>().enabled = false;
            GetComponent<OVRHeadsetEmulator>().enabled = false;
            GetComponent<OVRPlayerController>().enabled = false;
            GetComponent<CharacterController>().enabled = false;
            character.GetComponent<VRRig>().enabled = false;
            character.GetComponent<VRFootIK>().enabled = false;
            //Camera.GetComponent<AudioListener>().enabled = false;
            //Camera.GetComponent<Camera>().enabled = false;
            this.enabled = false;
        }
        if (!isLocalPlayer)
        {
            Camera.SetActive(false);
        }
        /*GetComponent<OVRCameraRig>().disableEyeAnchorCameras = false;
        Camera.GetComponent<Camera>().enabled = true;*/
        GameObject start = GameObject.Find("Player" + PlayerID + " Start");
        Debug.Log("Start pos :" + start.name + " " + start.transform.position);
        transform.position = start.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        ButtonEvent();
        SendPosInfo();
    }
    void ButtonEvent(){
        OVRInput.Update();
        OVRInput.FixedUpdate();
        Stamp_UI_Control UI = GetComponent<Stamp_UI_Control>();
        // trigger event
        if (OVRInput.Get(OVRInput.RawAxis1D.RIndexTrigger) > 0.5f){
            Debug.Log("Start !!!");
            // FindObjectOfType<PoseGameManager>().StartGame();
            // CmdAttach(GameObject.Find("T"),new Vector3(3,0,0));
        }
        // X button to close UI
        else if( OVRInput.Get(OVRInput.Button.One, OVRInput.Controller.LTouch))
        {
            UI.CloseUI();
        }
        // A button to open UI
        else if( OVRInput.Get(OVRInput.Button.One, OVRInput.Controller.RTouch))
        {
            UI.OpenUI();
        }
    }

    void GetController(){
        LeftHand = this.transform.Find("TrackingSpace/LeftHandAnchor");
        RightHand = this.transform.Find("TrackingSpace/RightHandAnchor");
        Center = this.transform.Find("TrackingSpace/CenterEyeAnchor");
    }
    // determine whther the player make a right pose
    public bool DeterminePose(PoseGame Game){
        // using squre to eliminate calc
        double r = Math.Pow(Game.ArmLength,2);
        //Debug.Log("Player " + PlayerID + " left hand : " + LeftHand + " right  hand : " + RightHand + " center : " + Center);
        Vector3 L_Vector3 = LeftHand.position - Center.position;
        Vector3 R_Vector3 = RightHand.position - Center.position;
        Vector2 L = new Vector2(L_Vector3.x,L_Vector3.y);
        Vector2 R = new Vector2(R_Vector3.x,R_Vector3.y);
        Vector2 Origin = new Vector2(1,0);
        float L_angle = CalcAngle(L);
        float R_angle = CalcAngle(R);
        bool is_L_stretch = false;
        bool is_R_stretch = false;
        // use to determine whether the controller is show on player vision
        /*Transform L_controller = this.transform.Find("TrackingSpace/LeftHandAnchor/LeftControllerAnchor/OVRControllerPrefab");
        Transform R_controller = this.transform.Find("TrackingSpace/RightHandAnchor/RightControllerAnchor/OVRControllerPrefab");
        // Debug.Log("L : " + CheckController(L_controller) + " ,R : " + CheckController(R_controller));
        if( CheckController(L_controller) && L.sqrMagnitude > r )
            is_L_stretch = true;
        if( CheckController(R_controller) && R.sqrMagnitude > r )
            is_R_stretch = true;*/
        if ( L.sqrMagnitude > r)
            is_L_stretch = true;
        if ( R.sqrMagnitude > r)
            is_R_stretch = true;
        // Debug.Log("Arm cmp : " + R.sqrMagnitude.ToString() + " , " + r);
        angle = R_angle;
        left_dir = Game.DetermineDir(L_angle,is_L_stretch);
        right_dir = Game.DetermineDir(R_angle,is_R_stretch);
        if (left_dir == Game.left && right_dir == Game.right){
            Debug.Log("Yes!Yes!Yes!");
            Game.PassGame();
            return true;
        }
        else{
            Debug.Log("NO!NO!NO! L : " + left_dir + " / R : " + right_dir);
            return false;
        }
    }
    // 0 degree means controller on the top of head
    float CalcAngle(Vector2 v){
        float value = (float)((Mathf.Atan2(v.x, v.y) / Math.PI) * 180f);
        if(value < 0) value += 360f;
        return value;
    }
    bool CheckController(Transform controller){
        for(int i = 0 ; i < controller.childCount ; i++)
            if(controller.GetChild(i).gameObject.activeInHierarchy)
                return true;
        return false;
    }
    // Attach player to target GameObject's local position
    [Command(requiresAuthority = false)]
    public void CmdAttach(GameObject target,Vector3 position){
        if(target == null){
            Debug.Log("[Player:Attach]:Target net exist can't attach to");
            return;
        }
        // when player not yet be sent
        if (gameObject.transform.parent == null)
        {
            Debug.Log("[Player:Attach]:Attach to " + OriginPosition);
            RpcAttach(target, position);
        }
    }
    [ClientRpc]
    private void RpcAttach(GameObject target,Vector3 position){
        OriginPosition = transform.position;
        transform.SetParent(target.transform);
        //GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        transform.localPosition = position;
    }
    // Detach player to target position(if set is_origin flag palyer is set to origin position where trigger Attach)
    // when is_origin set true, ignore input position
    [Command(requiresAuthority = false)]
    public void CmdDetach(Vector3 position,bool is_origin){
        if(gameObject.transform.parent == null){
            Debug.Log("[Player:Detaach]:Player's parent doesn't exict.Player don't need to detach");
            return;
        }
        transform.SetParent(null);
        RpcDetach(position,is_origin);
    }
    [ClientRpc]
    public void RpcDetach(Vector3 position,bool is_origin){
        transform.SetParent(null);
        //GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        if(is_origin){
            Debug.Log("Go back to : " + OriginPosition);
            transform.position = OriginPosition;
            transform.rotation = Quaternion.identity;
        }
        else{
            transform.position = position;
        }
    }
    // the info to debug (show on "Develop_scene" UI)
    void SendPosInfo(){
        if (FindObjectOfType<TempUI>() == null)
            return;
        TempUI.LeftPos = LeftHand.position;
        TempUI.RightPos = RightHand.position;
        TempUI.CenterPos = Center.position;
        TempUI.l_dir = left_dir;
        TempUI.r_dir = right_dir;
        TempUI.theta = angle;
    }
}
