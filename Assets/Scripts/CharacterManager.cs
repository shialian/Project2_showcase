﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class CharacterManager : NetworkBehaviour
{
    public static CharacterManager singleton;
    public GameObject playButton;
    public SyncList<bool> selected = new SyncList<bool>();
    public SyncList<NetworkIdentity> selectedByPlayer = new SyncList<NetworkIdentity>();

    private void Awake()
    {
        playButton.SetActive(false);
        singleton = this;
    }

    private void Start()
    {
        if (isServer)
        {
            selected.Add(false);
            selected.Add(false);
            selectedByPlayer.Add(null);
            selectedByPlayer.Add(null);
        }
    }

    private void Update()
    {
        SelectionManagement();
    }

    private void SelectionManagement()
    {
        if(selected[0] && selected[1])
        {
            playButton.SetActive(true);
        }
        else
        {
            playButton.SetActive(false);
        }
    }

    [Command(requiresAuthority = false)]
    public void EditSelection(int id, NetworkIdentity localPlayer)
    {
        if (selected[id] && selectedByPlayer[id] != localPlayer)
            return;
        selected[id] = !selected[id];
        if (selected[id])
        {
            for(int i =0; i < selectedByPlayer.Count; i++)
            {
                if (selectedByPlayer[i] == localPlayer)
                {
                    selectedByPlayer[i] = null;
                    selected[i] = false;
                }
            }
            selectedByPlayer[id] = localPlayer;
        }
        else
        {
            selectedByPlayer[id] = null;
        }
    }
}