using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayButton : MonoBehaviour
{
    public string sceneName;

    private void Update()
    {
        GoldFinger();
    }

    public void GoToPlayGame()
    {
        GameManager.singleton.LoadSceneByName(sceneName);
    }

    private void GoldFinger()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            GoToPlayGame();
        }
    }
}
