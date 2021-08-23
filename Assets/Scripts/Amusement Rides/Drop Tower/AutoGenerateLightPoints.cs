using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class AutoGenerateLightPoints : NetworkBehaviour
{
    public Transform lightPointPrefab = null;
    public Transform[] prefabs = null;
    public TriggerItem skyDrop;
    public int lightPointNum = 0;
    public Vector3 offsetMin = Vector3.zero;
    public Vector3 offsetMax = Vector3.zero;

    [SyncVar]
    public Vector3 offset;

    private void Update()
    {
        if (isServer && skyDrop.startOperation && this.GetComponent<Task>().lightPoints.Length == 0)
        {
            SetTaskLightPointNum();
            SetLightPointPositions();
        }
    }

    private void SetLightPointPositions()
    {
        for(int i = 0; i < lightPointNum; i++)
        {
            int randomIndex = Random.Range(0, prefabs.Length);
            offset.x = Random.Range(offsetMin.x, offsetMax.x);
            offset.y = Random.Range(offsetMin.y, offsetMax.y);
            offset.z = Random.Range(offsetMin.z, offsetMax.z);
            Vector3 lightPointPosition = prefabs[randomIndex].position + offset;
            InstanticalLightPoint(lightPointPosition, i);
        }
    }

    [ClientRpc]
    public void SetTaskLightPointNum()
    {
        this.GetComponent<Task>().lightPoints = new LightPoint[lightPointNum];
    }

    [ClientRpc]
    public void InstanticalLightPoint(Vector3 Position, int index)
    {
        Transform lightPoint = Instantiate(lightPointPrefab, Position, Quaternion.identity);
        lightPoint.GetComponent<LightPoint>().vibrationTime = 0.01f;
        lightPoint.parent = transform;
        this.GetComponent<Task>().lightPoints[index] = lightPoint.GetComponent<LightPoint>();
    }
}
