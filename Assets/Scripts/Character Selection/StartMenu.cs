using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMenu : MonoBehaviour
{
    public GameObject characterSelection;

    private void Start()
    {
        characterSelection.transform.position = new Vector3(0, 0, 1000000f);
    }

    public void EnterCharacterSelection()
    {
        this.gameObject.SetActive(false);
        characterSelection.transform.position = new Vector3(0.25f, 1f, 0.25f);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
