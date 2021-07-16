﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ChangeUI : MonoBehaviour
{
    public GameObject currentUI;
    public GameObject targetUI;

    public void Changed()
    {
        currentUI.SetActive(false);
        targetUI.SetActive(true);
    }
}