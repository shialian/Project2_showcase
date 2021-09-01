using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CurrentTime : MonoBehaviour
{
    public Dialogue dialogue;
    public TMP_FontAsset font;

    public Material sunset;
    public Light directional;

    private TextMeshProUGUI textUI;
    private Color dayColor;
    private Color sunsetColor;

    private void Awake()
    {
        textUI = GetComponent<TextMeshProUGUI>();
        ColorUtility.TryParseHtmlString("#FFEBC2", out dayColor);
        ColorUtility.TryParseHtmlString("#763409", out sunsetColor);
    }

    private void FixedUpdate()
    {
        int time = (int)Time.fixedTime * 42;
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

        if(hour == 16)
        {
            RenderSettings.skybox = sunset;
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

        directional.color = Color.Lerp(dayColor, sunsetColor, (float)time / 25200f);
    }
}
