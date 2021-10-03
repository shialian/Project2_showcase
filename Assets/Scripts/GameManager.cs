using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Mirror;

public class GameManager : NetworkBehaviour
{
    public static GameManager singleton;
    public NetManager networkManager;
    public bool isEnding = false;

    [SyncVar]
    public bool gameStart = false;

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

        ///// 以下金手指 /////
        GoldenFinger_FinalUI();      
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


    [Command(requiresAuthority = false)]
    public void GameStart(bool flag)
    {
        gameStart = flag;
    }
    /* new added end */

    /* Golden Finger Start*/
    private void GoldenFinger_FinalUI()
    {
        if (SceneManager.GetActiveScene().name == "Theme")
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {
                GameObject fail = GameObject.Find("Game Over UI").transform.Find("Fail").gameObject;
                LocalPlayer local = localPlayer.GetComponent<LocalPlayer>();
                local.SetUITransform(fail.transform.parent);
                local.showLaserBeam = true;
                fail.SetActive(true);
                isEnding = true;
            }
            if (Input.GetKeyDown(KeyCode.X))
            {
                GameObject success = GameObject.Find("Game Over UI").transform.Find("Success").gameObject;
                LocalPlayer local = localPlayer.GetComponent<LocalPlayer>();
                local.SetUITransform(success.transform.parent);
                local.showLaserBeam = true;
                success.SetActive(true);
                isEnding = true;
            }
        }
    }
    /* Golden Finger End*/
}
