using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CurrentTime : MonoBehaviour
{
    public Timer timer;

    private TextMeshProUGUI textUI;

    private void Start()
    {
        textUI = GetComponent<TextMeshProUGUI>();
    }

    private void FixedUpdate()
    {
        if (timer.minute < 10)
        {
            textUI.SetText("現在時間 " + timer.hour + ":0" + timer.minute);
        }
        else
        {
            textUI.SetText("現在時間 " + timer.hour + ":" + timer.minute);
        }
    }
}
