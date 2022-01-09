﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class CharacterManager : NetworkBehaviour
{
    public static CharacterManager singleton;
    public GameObject playButton;
    public SyncList<CharacterSelection> playerSelected = new SyncList<CharacterSelection>();

    private void Awake()
    {
        playButton.SetActive(false);
        singleton = this;
    }

    private void Start()
    {
        if (isServer)
        {
            playerSelected.Add(null);
            playerSelected.Add(null);
        }
    }

    

    [Command(requiresAuthority = false)]
    public void EditSelectionAndPlayCheck(int playerID, CharacterSelection character)
    {
        if (CanBeSelected(playerID, character) == false)
            return;
        if (playerSelected[playerID] == character)
        {
            playerSelected[playerID].Unselected();
            playerSelected[playerID] = null;
            GameManager.singleton.spawnCharcterID[playerID] = -1;
        }
        else
        {
            if (playerSelected[playerID] != null)
            {
                playerSelected[playerID].Unselected();
            }
            character.Selected();
            GameManager.singleton.spawnCharcterID[playerID] = character.id;
            playerSelected[playerID] = character;
        }
        PlayCheck();
    }

    private bool CanBeSelected(int playerID, CharacterSelection character)
    {
        for(int i = 0; i < playerSelected.Count; i++)
        {
            if(playerSelected[i] == character && i != playerID)
            {
                return false;
            }
        }
        return true;
    }

    private void PlayCheck()
    {
        if(playerSelected[0] != null && playerSelected[1] != null)
        {
            SetPlayButtonActiveState(true);
        }
        else
        {
            SetPlayButtonActiveState(false);
        }
    }

    [ClientRpc]
    private void SetPlayButtonActiveState(bool activeState)
    {
        playButton.SetActive(activeState);
    }
}