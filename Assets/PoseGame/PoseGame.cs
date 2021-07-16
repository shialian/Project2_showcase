using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PoseGame : NetworkBehaviour
{
    // if the arm length setting over 0.5 the north direction will be too hard
    // because the head position is close to top not at prefab center 
    public double ArmLength = 0.4;
    // is_pass flag will be init in Start() no mater what value is setted 
    // give public to be accessible & visable in unity interface for debug
    [SyncVar]
    public bool is_pass = false;
    public Direction left = Direction.NONE;
    public Direction right = Direction.NONE;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Arm length : " +  ArmLength);
        // init stage value
        is_pass = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    // determine the direction of angle
    // is_stretch flag determine whether the player stretch the arm
    public Direction DetermineDir(float angle,bool is_stretch){
        if(!is_stretch)
            return Direction.NONE;
        if ((angle > 337.5f) || (angle <= 22.5f))
            return Direction.N;
        else if ((angle > 22.5f) && (angle <= 67.5f))
            return Direction.NE;
        else if ((angle > 67.5f) && (angle <= 112.5f))
            return Direction.E;
        else if ((angle > 112.5f) && (angle <= 157.5f))
            return Direction.SE;
        else if ((angle > 157.5f) && (angle <= 202.5f))
            return Direction.S;
        else if ((angle > 202.5f) && (angle <= 247.5f))
            return Direction.SW;
        else if ((angle > 247.5f) && (angle <= 292.5f))
            return Direction.W;
        else if((angle > 292.5f) && (angle <= 337.5f))
            return Direction.NW;
        return Direction.NONE;
    }
    [Command(requiresAuthority = false)]
    public void PassGame(){
        is_pass = true;
        return;
    }
}
