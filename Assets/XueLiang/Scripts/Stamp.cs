using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stamp : MonoBehaviour
{
    public Sprite fullColorImage;
    public GameObject sign;

    [HideInInspector]
    public bool onHover;

    private void Start()
    {
        onHover = false;
        sign.SetActive(false);
    }

    public void OnHoverEnter()
    {
        onHover = true;
        sign.SetActive(true);
    }

    public void OnHoverExit()
    {
        onHover = false;
        sign.SetActive(false);
    }

    public void Success()
    {
        transform.GetComponent<Image>().sprite = fullColorImage;
    }
}