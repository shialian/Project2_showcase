using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStartButton : MonoBehaviour
{
    public Transform player;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.name != "Game Trigger")
        {
            player = collision.transform.parent.parent;
        }
    }
}
