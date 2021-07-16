using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextManager : MonoBehaviour
{
    public TextMeshProUGUI text;
    public IconHover[] icons;
    public string[] names;

    private bool nothingHover;

    private void Start()
    {
        text.SetText(" ");
        nothingHover = true;
    }

    private void Update()
    {
        HoverEevent();
    }

    private void HoverEevent()
    {
        nothingHover = true;
        for (int i = 0; i < icons.Length; i++)
        {
            if (icons[i].hover)
            {
                nothingHover = false;
                text.SetText(names[i]);
                break;
            }
        }
        if (nothingHover)
        {
            text.SetText(" ");
        }
    }
}
