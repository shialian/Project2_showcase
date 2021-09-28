using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToTheEnd : MonoBehaviour
{
    public void ToTheEndScene(string name)
    {
        GameManager.singleton.LoadSceneByName(name);
    }
}
