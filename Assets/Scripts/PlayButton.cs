using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayButton : NetworkBehaviour
{
    public string sceneName;

    [Command(requiresAuthority = false)]
    public void GoToPlayGame()
    {
        GameManager.GM.loadScene = true;
        GameManager.GM.sceneName = sceneName;
    }
}
