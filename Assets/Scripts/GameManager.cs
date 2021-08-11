using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class GameManager : NetworkBehaviour
{
    public int connectionId;
    public static GameManager singleton;
    public NetManager networkManager;
    public SyncDictionary<int, int> connection = new SyncDictionary<int, int>();

    [SyncVar]
    public bool loadScene;
    [SyncVar]
    public string sceneName;

    private List<int> keys;

    /* new added start */
    public SyncList<int> spawnCharcterID = new SyncList<int>();
    public int playerID;
    private bool playerAdded;
    /* new added end*/

    private void Awake()
    {
        singleton = this;
        playerAdded = false;
        playerID = -1;
    }

    private void Start()
    {
        DontDestroyOnLoad(this);
        connectionId = -1;
        loadScene = false;
    }

    private void Update()
    {
        if (playerAdded == false && NetworkClient.ready)
        {
            NewPlayerAdded();
            playerAdded = true;
        }
        if(playerID == -1)
        {
            Invoke("SetPlayerID", 0.1f);
        }
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

    /* new added start */

    [Command(requiresAuthority = false)]
    public void NewPlayerAdded()
    {
        spawnCharcterID.Add(0);
    }

    private void SetPlayerID()
    {
        playerID = spawnCharcterID.Count - 1;
    }

    [Command(requiresAuthority = false)]
    public void LoadSceneByName(string name)
    {
        NetManager.singleton.ServerChangeScene(name);
    }

    /* new added end */
}
