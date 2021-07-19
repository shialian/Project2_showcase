using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class NetManager : NetworkManager
{
    public static NetManager NM;

    private int currentConnectionNumber = 0;
    private List<int> currentConnection = new List<int>();

    public override void Awake() {
        NM = this;
        base.Awake();
    }

    public override void LateUpdate()
    {
        if (NetworkClient.ready)
        {
            if (currentConnectionNumber != currentConnection.Count)
            {
                currentConnectionNumber++;
                GameManager.GM.SetPlayerIDByConnection(currentConnection[currentConnectionNumber - 1], -1);
            }
            if (GameManager.GM.connectionId == -1)
            {
                GameManager.GM.SetConnectionID();
                GameManager.GM.SetPlayerIDByConnection(GameManager.GM.connectionId, GameManager.GM.playerID);
            }
        }
        base.LateUpdate();
    }

    public override void OnServerConnect(NetworkConnection conn)
    {
        currentConnection.Add(conn.connectionId);
    }

    //init server
    public override void OnStartServer()
    {
        base.OnStartServer();
    }
    // assign diff player prefab to diff client
    public override void OnServerAddPlayer(NetworkConnection conn){
        if(GameObject.FindObjectOfType<CharacterManager>() == null)
        {
            playerPrefab = spawnPrefabs[GameManager.GM.connection[conn.connectionId]];
        }
        //avoid overflow
        base.OnServerAddPlayer(conn);
    }

    // initial necessary value when scene change
    public override void OnServerChangeScene(string newSceneName){

    }
    public void Start_Server(){
        NM.StartHost();
        Debug.LogError("host");
    }   
    // Start client only
    public void Start_Cient(){
        Debug.Log("Connect to : " + networkAddress);
        NM.StartClient();
    }

    public void ChangeScene(string sceneName)
    {
        ServerChangeScene(sceneName);
    }
}
