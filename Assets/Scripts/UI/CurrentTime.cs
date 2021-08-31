using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CurrentTime : MonoBehaviour
{
    public Dialogue dialogue;
    public TMP_FontAsset font;

    private TextMeshProUGUI textUI;

    private void Awake()
    {
        textUI = GetComponent<TextMeshProUGUI>();
    }

    private void FixedUpdate()
    {
        //int time = (int)Time.fixedTime * 6;
        int time = 25080 + (int)Time.fixedTime * 6;
        int minute = time / 60;
        int hour = minute / 60 + 10;
        minute %= 60;
        if (minute < 10)
        {
            textUI.SetText("現在時間 " + hour + ":0" + minute);
        }
        else
        {
            textUI.SetText("現在時間 " + hour + ":" + minute);
        }
        
        if(hour == 17 && minute == 0)
        {
            LocalPlayer localPlayer = GameManager.singleton.localPlayer.GetComponent<LocalPlayer>();
            dialogue.gameObject.SetActive(true);
            localPlayer.SetUITransform(dialogue.transform);
            dialogue.ChangeScript("Assets/Dialogue/End_Not_Finish.txt", font);
            GameManager.singleton.isEnding = true;
            this.transform.parent.gameObject.SetActive(false);
        }
    }
}
