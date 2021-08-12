using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Minimap : MonoBehaviour, IScrollHandler, IDragHandler
{
    public Camera minimapCamera;

    private float size;

    private void Awake()
    {
        size = minimapCamera.orthographicSize;
    }

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
        minimapCamera.orthographicSize = Mathf.Clamp(minimapCamera.orthographicSize, 16, size);
    }

    public void OnDrag(PointerEventData eventData)
    {
        float size = minimapCamera.orthographicSize;
        Vector3 position = minimapCamera.transform.position;
        position -= new Vector3(eventData.delta.x, 0, eventData.delta.y) * 0.05f;


        float diff = size + Mathf.Abs(position.x) - size;

        if (size + Mathf.Abs(position.x) > size)
        {
            if (position.x >= 0)
                position -= new Vector3(diff, 0, 0);
            else
                position += new Vector3(diff, 0, 0);
        }

        diff = size + Mathf.Abs(position.z) - size;
        if (size + Mathf.Abs(position.z) > size)
        {
            if (position.z >= 0)
                position -= new Vector3(0, 0, diff);
            else
                position += new Vector3(0, 0, diff);
        }

        minimapCamera.transform.position = position;
    }
}
