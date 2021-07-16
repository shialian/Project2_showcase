using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public struct PlayerMessage : NetworkMessage
{
    public int num;
}

public class MyNetworkManager : NetworkManager
{
    private int player_count = 0;
    public static MyNetworkManager NM;
    public override void OnStartServer()
    {
        base.OnStartServer();
        NM = this;
        // NetworkServer.RegisterHandler<PlayerMessage>(OnCreateCharacter);
    }
    public override void OnClientConnect(NetworkConnection conn)
    {
        base.OnClientConnect(conn);

        // you can send the message here, or wherever else you want
        PlayerMessage characterMessage = new PlayerMessage
        {
            num = player_count
        };
        conn.Send(characterMessage);
    }
    public override void OnServerAddPlayer(NetworkConnection conn){
        Debug.Log("Player NO." + conn.connectionId);
        NM.playerPrefab = spawnPrefabs[player_count++];
        base.OnServerAddPlayer(conn);
    }

    public override void OnServerChangeScene(string newSceneName){
        player_count = 0;
    }

    /*public hello_message(){
        
    }*/

    /*public override void OnServerConnect(NetworkConnection conn){
        Debug.Log("Player NO." + player_count);
        NM.playerPrefab = spawnPrefabs[player_count++];
    }*/

    void OnCreateCharacter(NetworkConnection conn,PlayerMessage msg)
    {
        // playerPrefab is the one assigned in the inspector in Network
        // Manager but you can use different prefabs per race for example
        GameObject gameobject = Instantiate(spawnPrefabs[player_count],startPositions[player_count]);
        Debug.Log("Player NO. : " + player_count++);
        GameObject obj = conn.identity.gameObject;
        // Apply data from the message however appropriate for your game
        // Typically Player would be a component you write with syncvars or properties
        ReplacePlayer(conn,gameObject);

        // call this to use this gameobject as the primary controller
        // NetworkServer.AddPlayerForConnection(conn, gameobject);
    }
    // Update is called once per frame
    public void ReplacePlayer(NetworkConnection conn, GameObject newPrefab)
    {
        // Cache a reference to the current player object
        GameObject oldPlayer = conn.identity.gameObject;

        // Instantiate the new player object and broadcast to clients
        NetworkServer.ReplacePlayerForConnection(conn, Instantiate(newPrefab));

        // Remove the previous player object that's now been replaced
        NetworkServer.Destroy(oldPlayer);
    }
}
