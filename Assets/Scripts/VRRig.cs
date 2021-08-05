using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class VRMap
{
    public Transform vrTarget;
    public Transform rigTarget;
    public Vector3 trackingPositionOffset;
    public Vector3 trackingRotationOffset;

    public void Map()
    {
        rigTarget.position = vrTarget.TransformPoint(trackingPositionOffset);
        rigTarget.rotation = vrTarget.rotation * Quaternion.Euler(trackingRotationOffset);
    }
}

public class VRRig : MonoBehaviour
{
    public VRMap head;
    public VRMap leftHand;
    public VRMap rightHand;

    public Transform headConstraint;
    public float turnSmoothness;
    private Vector3 headBodyOffset;

    // Start is called before the first frame update
    void Start()
    {
        headBodyOffset = transform.position - headConstraint.position;
        head.vrTarget.forward = Vector3.forward;
    }
    
    private void FixedUpdate()
    {
        transform.position = headConstraint.position + headBodyOffset;
        Vector3 targetForward = Vector3.ProjectOnPlane(head.vrTarget.forward, Vector3.up).normalized;
        if (Vector3.Dot(transform.forward, targetForward) <= 0)
        {
            targetForward *= -1f;
        }
        transform.forward = Vector3.Lerp(transform.forward, targetForward, turnSmoothness);
        head.Map();
        leftHand.Map();
        rightHand.Map();
    }
}
