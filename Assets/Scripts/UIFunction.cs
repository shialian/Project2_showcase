using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIFunction : MonoBehaviour
{
    public GameObject menu;
    public GameObject previous;
    public GameObject[] buttons;
    public GameObject guideMap;
    public GameObject stamp;

    private void Awake()
    {
        previous.SetActive(false);
        HideButtons();
        guideMap.SetActive(false);
        stamp.SetActive(false);
    }

    public void MenuClicked()
    {
        ShowAllButtons();
        menu.SetActive(false);
        previous.SetActive(true);
    }

    public void ShowGuideMap()
    {
        guideMap.SetActive(true);
        HideButtons();
    }

    public void ShowStamp()
    {
        stamp.SetActive(true);
        HideButtons();
    }

    public void BeckWard()
    {
        if(guideMap.activeSelf)
        {
            ShowAllButtons();
            guideMap.SetActive(false);
        }
        else if (stamp.activeSelf)
        {
            ShowAllButtons();
            stamp.SetActive(false);
        }
        else
        {
            HideButtons();
            menu.SetActive(true);
            previous.SetActive(false);
        }
    }

    private void ShowAllButtons()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].gameObject.SetActive(true);
        }
    }

    private void HideButtons()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].gameObject.SetActive(false);
        }
    }
}
