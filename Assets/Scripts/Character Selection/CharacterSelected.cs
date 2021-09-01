using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class CharacterSelected : NetworkBehaviour
{
    public int id;
    public GameObject selectedBoard;
    public AudioSource source;
    public AudioClip selectedSound;

    private bool soundPlayed;

    private void Awake()
    {
        selectedBoard.SetActive(false);
        soundPlayed = false;
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

            if (CharacterManager.singleton.selected[id] && soundPlayed == false)
            {
                source.PlayOneShot(selectedSound);
                soundPlayed = true;
            }
            else if (CharacterManager.singleton.selected[id] == false && soundPlayed == true)
            {
                soundPlayed = false;
            }
        }
    }
}