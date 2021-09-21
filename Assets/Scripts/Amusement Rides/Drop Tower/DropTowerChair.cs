using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropTowerChair : MonoBehaviour
{
    public void Chair()
    {
        Transform localPlayer = GameManager.singleton.localPlayer;
        localPlayer.parent = transform;
        localPlayer.GetComponent<LocalPlayer>().SetPositionByOther(transform.position);
    }
}
