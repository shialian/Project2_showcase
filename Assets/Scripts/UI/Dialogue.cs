using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;

public class Dialogue : MonoBehaviour
{
    public GameObject[] backgrounds;
    public GameObject announcement;
    public TextMeshProUGUI characterName;
    public TextMeshProUGUI scriptText;

    private StreamReader reader = new StreamReader("Assets/Dialogue/Start.txt");
    private string[] script;
    private int index;
    private GameObject currentBackgound;

    private void Awake()
    {
        script = reader.ReadToEnd().Split('\n');
        index = 0;

        for(int i = 0; i < backgrounds.Length; i++)
        {
            backgrounds[i].SetActive(false);
        }
        currentBackgound = backgrounds[0];

        Continue();
    }

    private void Update()
    {
        if(gameObject.activeSelf == false)
        {
            reader.Close();
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            Continue();
        }
    }

    public void Continue()
    {
        if(index >= script.Length && GameManager.singleton.isEnding == false)
        {
            announcement.SetActive(true);
            this.gameObject.SetActive(false);
            return;
        }
        
        string currentText = script[index];
        string character = currentText.Substring(0, 2);
        currentText = currentText.Substring(3, currentText.Length - 3);
        SetCharacter(character);
        SetText(currentText, character);
        index++;
    }

    private void SetCharacter(string character)
    {
        currentBackgound.SetActive(false);
        switch (character)
        {
            case "爸爸":
                currentBackgound = backgrounds[2];
                break;
            case "媽媽":
                currentBackgound = backgrounds[1];
                break;
            case "哥哥":
                currentBackgound = backgrounds[3];
                break;
            case "妹妹":
                currentBackgound = backgrounds[0];
                break;
        }
        currentBackgound.SetActive(true);
    }

    private void SetText(string text, string character)
    {
        characterName.SetText(character);
        scriptText.SetText(text);
    }

    public void ChangeScript(string path, TMP_FontAsset font)
    {
        reader = new StreamReader(path);
        script = reader.ReadToEnd().Split('\n');
        scriptText.font = font;

        for (int i = 0; i < backgrounds.Length; i++)
        {
            backgrounds[i].SetActive(false);
        }

        index = 0;
        Continue();
    }
}
