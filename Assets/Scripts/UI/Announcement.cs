using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Announcement : MonoBehaviour
{
    public GameObject annocement;
    public TextMeshProUGUI buttonText;

    private int index;

    private void Awake()
    {
        index = 0;
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
        LocalPlayer localPlayer = GameManager.singleton.localPlayer.GetComponent<LocalPlayer>();
        localPlayer.StartGame();
        this.gameObject.SetActive(false);
    }
}
