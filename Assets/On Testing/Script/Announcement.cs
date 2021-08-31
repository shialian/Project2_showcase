using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Announcement : MonoBehaviour
{
    public GameObject[] annocements;
    public TextMeshProUGUI buttonText;

    private int index;

    private void Awake()
    {
        index = 0;

        annocements[1].SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            ContinueAndStart();
        }
    }

    public void ContinueAndStart()
    {
        if(index == 0)
        {
            buttonText.SetText("開始玩");
            annocements[0].SetActive(false);
            annocements[1].SetActive(true);
        }
        else
        {
            LocalPlayer localPlayer = GameManager.singleton.localPlayer.GetComponent<LocalPlayer>();
            localPlayer.StartGame();
            this.gameObject.SetActive(false);
        }
        index++;
    }
}
