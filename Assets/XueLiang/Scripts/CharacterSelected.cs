using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class CharacterSelected : NetworkBehaviour
{
    public GameObject selectedBoard;
    public int ID;
    public CharacterSelected[] characters;

    [SyncVar]
    public bool selected;

    private void Start()
    {
        selectedBoard.SetActive(false);
        selected = false;
    }

    public void SelectionChecked()
    {
        if (selected == false)
        {
            if (GameManager.GM.GetPlayerID() != -1)
            {
                for (int i = 0; i < characters.Length; i++)
                {
                    if (characters[i].ID == GameManager.GM.GetPlayerID())
                    {
                        SetOtherFalse(characters[i]);
                        break;
                    }
                }
            }
            SetTrue();
            GameManager.GM.SetPlayerID(ID);
            GameManager.GM.SetPlayerIDByConnection(GameManager.GM.connectionId, ID);
        }
        else
        {
            if (GameManager.GM.GetPlayerID() == ID)
            {
                SetFalse();
                GameManager.GM.SetPlayerID(-1);
                GameManager.GM.SetPlayerIDByConnection(GameManager.GM.connectionId, -1);
            }
        }
    }
    [Command(requiresAuthority = false)]
    private void SetTrue()
    {
        selected = true;
    }
    [Command(requiresAuthority = false)]
    private void SetFalse()
    {
        selected = false;
    }
    [Command(requiresAuthority = false)]
    private void SetOtherFalse(CharacterSelected obj)
    {
        obj.selected = false;
    }

    private void Update()
    {
        if (selected)
        {
            selectedBoard.SetActive(true);
        }
        else
        {
            selectedBoard.SetActive(false);
        }
    }
}