using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CurrentTime : MonoBehaviour
{
    public int totoalTimeInMinute = 10;
    public Dialogue dialogue;
    public TMP_FontAsset font;

    public Material blendedSkybox;
    public Light directional;

    private TextMeshProUGUI textUI;
    private Color dayColor;
    private Color sunsetColor;
    private int timePassingSpeed;

    private void Awake()
    {
        textUI = GetComponent<TextMeshProUGUI>();
        ColorUtility.TryParseHtmlString("#FFEBC2", out dayColor);
        ColorUtility.TryParseHtmlString("#763409", out sunsetColor);
        timePassingSpeed = 360 / totoalTimeInMinute;
    }

    private void FixedUpdate()
    {
        int time = (int)Time.fixedTime * timePassingSpeed;
        int minute = time / 60;
        int hour = minute / 60 + 11;
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
        blendedSkybox.SetFloat("_Blend", (Time.fixedTime / 60) / totoalTimeInMinute);
        RenderSettings.skybox = blendedSkybox;
    }
}
