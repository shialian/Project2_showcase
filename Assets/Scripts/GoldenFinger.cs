using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldenFinger : MonoBehaviour
{
    public DropTowerControl skyDrop;
    public Gondora gondora;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            skyDrop.GoldenFinger();
        }
        if (Input.GetKeyDown(KeyCode.G))
        {
            gondora.GoldenFinger();
        }
    }
}
