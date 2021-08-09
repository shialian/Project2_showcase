using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Minimap : MonoBehaviour, IScrollHandler, IDragHandler
{
    public Camera minimapCamera;

    public void OnPointerEnter(GameObject icon)
    {
        icon.SetActive(true);
    }

    public void OnPointerExit(GameObject icon)
    {
        icon.SetActive(false);
    }

    public void OnScroll(PointerEventData eventData)
    {
        minimapCamera.orthographicSize -= eventData.scrollDelta.y;
        minimapCamera.orthographicSize = Mathf.Clamp(minimapCamera.orthographicSize, 4, 25);
    }

    public void OnDrag(PointerEventData eventData)
    {
        float size = minimapCamera.orthographicSize;
        Vector3 position = minimapCamera.transform.position;
        position -= new Vector3(eventData.delta.x, 0, eventData.delta.y) * 0.05f;


        float diff = size + Mathf.Abs(position.x) - 25;

        if (size + Mathf.Abs(position.x) > 25)
        {
            if (position.x >= 0)
                position -= new Vector3(diff, 0, 0);
            else
                position += new Vector3(diff, 0, 0);
        }

        diff = size + Mathf.Abs(position.z) - 25;
        if (size + Mathf.Abs(position.z) > 25)
        {
            if (position.z >= 0)
                position -= new Vector3(0, 0, diff);
            else
                position += new Vector3(0, 0, diff);
        }

        minimapCamera.transform.position = position;

    }
}
