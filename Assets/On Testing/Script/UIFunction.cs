using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIFunction : MonoBehaviour
{
    public GameObject menu;
    public GameObject previous;
    public GameObject guideMap;

    private void Awake()
    {
        menu.SetActive(true);
        previous.SetActive(false);
        guideMap.SetActive(false);
    }

    public void MenuClicked()
    {
        ShowGuideMap();
        menu.SetActive(false);
        previous.SetActive(true);
    }

    public void ShowGuideMap()
    {
        guideMap.SetActive(true);
    }

    public void BeckWard()
    {
        guideMap.SetActive(false);
        menu.SetActive(true);
        previous.SetActive(false);
    }
}
