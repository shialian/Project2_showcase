using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Mirror;

public class GameManager : NetworkBehaviour
{
    public static GameManager singleton;
    public NetManager networkManager;
    public SyncDictionary<int, int> connection = new SyncDictionary<int, int>();
    public bool isEnding = false;

    private List<int> keys;

    /* new added start */
    public SyncList<int> spawnCharcterID = new SyncList<int>();
    public int playerID;
    public Transform localPlayer = null;
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
        if(NetworkClient.localPlayer != null && localPlayer == null && SceneManager.GetActiveScene().name == "Theme")
        {
            localPlayer = NetworkClient.localPlayer.transform;
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
