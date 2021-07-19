using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FitThePlane : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        Ray ray = new Ray(transform.position, Vector3.down);
        RaycastHit hit;
        Physics.Raycast(ray, out hit);
        transform.position = hit.point;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
