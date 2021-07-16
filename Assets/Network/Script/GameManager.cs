using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class GameManager : NetworkBehaviour
{
    public int playerID = -1;
    public int connectionId;
    public static GameManager GM;
    public NetManager networkManager;
    public SyncDictionary<int, int> connection = new SyncDictionary<int, int>();

    [SyncVar]
    public bool loadScene;
    [SyncVar]
    public string sceneName;

    private List<int> keys;

    private void Start()
    {
        DontDestroyOnLoad(this);
        GM = this;
        connectionId = -1;
        loadScene = false;
    }

    private void Update()
    {
        if (loadScene && isServer)
        {
            LoadSceneByName(sceneName);
        }
    }

    public void LoadSceneByName(string name)
    {
        loadScene = false;
        networkManager.ChangeScene(name);
    }

    
    public int GetPlayerID()
    {
        return playerID;
    }

    public void SetPlayerID(int ID)
    {
        playerID = ID;
    }

    [Command(requiresAuthority = false)]
    public void SetPlayerIDByConnection(int conn, int id)
    {
        if (connection.ContainsKey(conn) == false)
        {
            connection.Add(conn, id);
        }
        else
        {
            connection[conn] = id;
        }
    }

    public void SetConnectionID()
    {
        keys = new List<int>(connection.Keys);
        connectionId = keys[keys.Count - 1];
    }
    // send another player who don't trigger the game to facility
    public void SendAnotherPlayer(int tirggerPlayerID,GameObject facility,Vector3 position){
        Player[] PlayerList = FindObjectsOfType<Player>();
        Player SendTarget = null;
        for(int i = 0 ; i < PlayerList.Length ; i++){
            if(PlayerList[i].PlayerID != tirggerPlayerID){
                SendTarget = PlayerList[i];
                break;
            }
        }
        Debug.Log("Who be send? : " + SendTarget);
        if(SendTarget != null){
            Debug.Log("Sending! " + facility.name + " " + position);
            SendTarget.CmdAttach(facility,position); 
        }
    }
    // send player back to ground
    public void SendPlayerBack(int targetPlayerID,Vector3 position,bool is_origin){
        Player[] PlayerList = FindObjectsOfType<Player>();
        Player SendTarget = null;
        Debug.Log("Send Back! " + targetPlayerID + " " + position + " " + is_origin);
        for(int i = 0 ; i < PlayerList.Length ; i++){
            if(PlayerList[i].PlayerID == targetPlayerID){
                SendTarget = PlayerList[i];
                break;
            }
        }
        if(SendTarget != null){
            // set is_origin to true let player back to origin position
            SendTarget.CmdDetach(position,is_origin);
        }
    }
}
