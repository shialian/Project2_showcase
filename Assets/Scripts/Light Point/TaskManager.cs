using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskManager : MonoBehaviour
{
    public Task[] tasks;
    public GameObject successUI;
    public GameObject guideMap;

    private bool taskFinish;

    private void Awake()
    {
        taskFinish = true;
    }

    private void Start()
    {
        successUI.SetActive(false);
    }

    private void Update()
    {
        taskFinish = true;
        foreach(Task task in tasks)
        {
            if(task.taskComplete == false)
            {
                taskFinish = false;
            }
        }
        if (taskFinish && successUI.activeSelf == false)
        {
            GameManager.singleton.isEnding = true;
            LocalPlayer localPlayer = GameManager.singleton.localPlayer.GetComponent<LocalPlayer>();
            localPlayer.SetUITransform(successUI.transform.parent);
            localPlayer.showLaserBeam = true;
            localPlayer.GetComponent<OVRPlayerController>().enabled = false;
            successUI.SetActive(true);
            guideMap.SetActive(false);
            this.transform.gameObject.SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            GameManager.singleton.isEnding = true;
            LocalPlayer localPlayer = GameManager.singleton.localPlayer.GetComponent<LocalPlayer>();
            localPlayer.SetUITransform(successUI.transform.parent);
            localPlayer.showLaserBeam = true;
            localPlayer.GetComponent<OVRPlayerController>().enabled = false;
            successUI.SetActive(true);
            guideMap.SetActive(false);
            this.transform.gameObject.SetActive(false);
        }
    }
}
