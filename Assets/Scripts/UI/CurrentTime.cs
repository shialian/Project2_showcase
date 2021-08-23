using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CurrentTime : MonoBehaviour
{
    private TextMeshProUGUI textUI;

    private void Awake()
    {
        textUI = GetComponent<TextMeshProUGUI>();
    }

    private void FixedUpdate()
    {
        int time = (int)Time.fixedTime * 6;
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
    }
}
