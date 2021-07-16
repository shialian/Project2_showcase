﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class CharacterManager : NetworkBehaviour
{
    public CharacterSelected[] characters;
    public GameObject playButton;

    private int count;

    private void Start()
    {
        playButton.SetActive(false);
    }

    private void Update()
    {
        SelectionManagement();
    }

    private void SelectionManagement()
    {
        count = 0;
        for(int i = 0; i < characters.Length; i++)
        {
            if (characters[i].selected)
                count++;
        }
        if(count == characters.Length)
        {
            playButton.SetActive(true);
        }
    }
}