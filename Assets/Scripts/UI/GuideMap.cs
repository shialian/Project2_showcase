using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GuideMap : MonoBehaviour
{
    public Dialogue dialogue;
    public TMP_FontAsset font;

    public void EnterEnding()
    {
        LocalPlayer localPlayer = GameManager.singleton.localPlayer.GetComponent<LocalPlayer>();
        dialogue.gameObject.SetActive(true);
        localPlayer.SetUITransform(dialogue.transform);
        dialogue.ChangeScript("Assets/Dialogue/End_Finish.txt", font);
        GameManager.singleton.isEnding = true;
        this.gameObject.SetActive(false);
    }
}
