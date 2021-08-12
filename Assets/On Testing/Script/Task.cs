using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Task : MonoBehaviour
{
    public LightPoint[] lightPoints;
    public GameObject bwIcon;
    public GameObject colorIcon;
    public Material notClearSign;
    public Material clearSign;
    public GameObject rideSign;

    private bool taskComplete;

    private void Awake()
    {
        taskComplete = true;
        bwIcon.SetActive(true);
        colorIcon.SetActive(false);
        rideSign.GetComponent<RawImage>().material = notClearSign;
    }

    public void CheckTaskComplete()
    {
        for(int i =0; i < lightPoints.Length; i++)
        {
            if(lightPoints[i].loadingCircle.fillAmount < 1)
            {
                taskComplete = false;
                break;
            }
        }
        if (taskComplete)
        {
            bwIcon.SetActive(false);
            colorIcon.SetActive(true);
            rideSign.GetComponent<RawImage>().material = clearSign;
        }
    }
}
