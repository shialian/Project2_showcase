using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class CharacterSelected : NetworkBehaviour
{
    public int id;
    public GameObject selectedBoard;

    private void Awake()
    {
        selectedBoard.SetActive(false);
    }

    public void SelectionChecked()
    {
        CharacterManager.singleton.EditSelection(GameManager.singleton.playerID, id, NetworkClient.localPlayer);
    }

    private void Update()
    {
        if (NetworkClient.ready && CharacterManager.singleton.selected.Count == 2)
        {
            selectedBoard.SetActive(CharacterManager.singleton.selected[id]);
        }
    }
}