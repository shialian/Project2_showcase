using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Announcement : MonoBehaviour
{
    public TextMeshProUGUI buttonText;
    public GameObject[] announcement;
    public GameObject dialogue;

    private bool isInro = true;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            ContinueAndStart();
        }
    }

    public void IntroOrStart()
    {
        if (isInro)
        {
            EndIntroduction();
        }
        else
        {
            ContinueAndStart();
        }
    }

    public void EndIntroduction()
    {
        buttonText.SetText("開始玩");
        announcement[0].SetActive(false);
        announcement[1].SetActive(true);
        isInro = false;
        dialogue.SetActive(true);
        this.gameObject.SetActive(false);
    }

    public void ContinueAndStart()
    {
        LocalPlayer localPlayer = GameManager.singleton.localPlayer.GetComponent<LocalPlayer>();
        localPlayer.StartGame();
        GameManager.singleton.GameStart(true);
        this.gameObject.SetActive(false);
    }

    public void ReStart()
    {
        GameManager.singleton.isEnding = false;
        GameManager.singleton.GameStart(false);
        GameManager.singleton.LoadSceneByName("Theme");
    }

    public void EndGame()
    {
        Application.Quit();
    }
}
