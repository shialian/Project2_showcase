using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Minimap : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IScrollHandler, IDragHandler
{
    public Camera minimapCamera;

    public GameObject[] icons;
    public GameObject[] signs;

    private bool mouseHover;

    private void Awake()
    {
        mouseHover = false;
        DisableSigns();
    }

    private void Update()
    {
        if (mouseHover)
        {
            // 上下 316 17 左右 253 552 中間 403 166
            Vector2 mousePosition = Input.mousePosition;
            Vector2 localMousePosition = (new Vector2(403f, 166f) - mousePosition) / 150f;
            float size = minimapCamera.orthographicSize;
            Vector3 raycastPosition = new Vector3(size * localMousePosition.x, minimapCamera.transform.position.y, size * localMousePosition.y);
            Ray ray = new Ray(raycastPosition, Vector3.down);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 200f))
            {
                if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Sign"))
                {
                    for (int i = 0; i < signs.Length; i++)
                    {
                        if (hit.collider.name == signs[i].name)
                        {
                            icons[i].SetActive(true);
                        }
                    }
                }
            }
            else
            {
                DisableSigns();
            }
        }
    }

    private void DisableSigns()
    {
        for (int i = 0; i < signs.Length; i++)
        {
            icons[i].SetActive(false);
        }
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

    public void OnPointerEnter(PointerEventData eventData)
    {
        mouseHover = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        mouseHover = false;
    }
}
