using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Announcement : MonoBehaviour
{
    public TextMeshProUGUI buttonText;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            ContinueAndStart();
        }
    }

    public void ContinueAndStart()
    {
        LocalPlayer localPlayer = GameManager.singleton.localPlayer.GetComponent<LocalPlayer>();
        localPlayer.StartGame();
        this.gameObject.SetActive(false);
    }

    public void ReStart()
    {
        GameManager.singleton.isEnding = false;
        GameManager.singleton.LoadSceneByName("Theme");
    }

    public void EndGame()
    {
        Application.Quit();
    }
}
