using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchMap : MonoBehaviour
{
    public GameObject instructionDark;
    public GameObject instructionLight;
    public GameObject mapDark;
    public GameObject mapLight;

    public GameObject map;
    public GameObject instruction;

    public void SwitchOn(string name)
    {
        switch (name)
        {
            case "map":
                mapDark.SetActive(false);
                instructionLight.SetActive(false);
                mapLight.SetActive(true);
                instructionDark.SetActive(true);
                instruction.SetActive(false);
                map.SetActive(true);
                break;
            case "instructions":
                mapLight.SetActive(false);
                instructionDark.SetActive(false);
                mapDark.SetActive(true);
                instructionLight.SetActive(true);
                map.SetActive(false);
                instruction.SetActive(true);
                break;
        }
    }
}
