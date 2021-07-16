using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IconHover : MonoBehaviour
{
    [HideInInspector]
    public bool hover;

    private void Start()
    {
        hover = false;
    }

    public void OnHover()
    {
        hover = true;
    }

    public void ExitHover()
    {
        hover = false;
    }
}
