using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class CharacterSelection : NetworkBehaviour
{
    public int id;
    public GameObject selectedBoard;
    public AudioSource source;
    public AudioClip selectedSound;

    private void Awake()
    {
        selectedBoard.SetActive(false);
    }

    public void Clicked()
    {
        CharacterManager.singleton.EditSelectionAndPlayCheck(GameManager.singleton.playerID, this);
    }

    private void Update()
    {
        if (NetworkClient.ready)
        {
            GoldFinger();
        }
    }

    private void GoldFinger()
    {
        if (Input.GetKeyDown(KeyCode.A) && id == 1)
        {
            CharacterManager.singleton.EditSelectionAndPlayCheck(GameManager.singleton.playerID, this);
        }
        if (Input.GetKeyDown(KeyCode.B) && id == 0)
        {
            CharacterManager.singleton.EditSelectionAndPlayCheck(GameManager.singleton.playerID, this);
        }
    }

    [ClientRpc]
    public void Selected()
    {
        selectedBoard.SetActive(true);
        source.PlayOneShot(selectedSound);
    }

    [ClientRpc]
    public void Unselected()
    {
        selectedBoard.SetActive(false);
    }
}