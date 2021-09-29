using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public Material blendedSkybox;
    public Light directional;
    public float totoalTimeInMinute = 10f;
    public GameObject failUI;
    public GameObject guideMap;

    public int hour, minute;

    private float timer;
    private float timePassingSpeed;
    private Color dayColor;
    private Color sunsetColor;

    private void Awake()
    {
        ColorUtility.TryParseHtmlString("#FFEBC2", out dayColor);
        ColorUtility.TryParseHtmlString("#763409", out sunsetColor);
        timePassingSpeed = 360 / totoalTimeInMinute;
    }

    private void Start()
    {
        failUI.SetActive(false);
        timer = 0;
    }

    private void FixedUpdate()
    {
        if (GameManager.singleton.gameStart)
        {
            timer += Time.fixedDeltaTime * timePassingSpeed;
        }
        int time = (int)timer;
        minute = time / 60;
        hour = minute / 60 + 11;
        minute %= 60;

        if (hour >= 17 && minute >= 0)
        {
            GameManager.singleton.isEnding = true;
            LocalPlayer localPlayer = GameManager.singleton.localPlayer.GetComponent<LocalPlayer>();
            localPlayer.SetUITransform(failUI.transform.parent);
            localPlayer.showLaserBeam = true;
            localPlayer.GetComponent<OVRPlayerController>().enabled = false;
            failUI.SetActive(true);
            guideMap.SetActive(false);
            this.transform.gameObject.SetActive(false);
        }

        directional.color = Color.Lerp(dayColor, sunsetColor, (float)time / 25200f);
        blendedSkybox.SetFloat("_Blend", (timer / (60f * timePassingSpeed)) / totoalTimeInMinute);
        RenderSettings.skybox = blendedSkybox;
    }
}
