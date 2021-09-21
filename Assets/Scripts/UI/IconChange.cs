using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IconChange : MonoBehaviour
{
    public Sprite[] icons;

    private Image image;

    private void Start()
    {
        image = GetComponent<Image>();
    }

    private void Update()
    {
        if(image.sprite == null && GameManager.singleton.localPlayer)
        {
            image.sprite = icons[GameManager.singleton.playerID];
        }
    }
}
