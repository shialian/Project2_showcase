using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomHandPose : MonoBehaviour
{
    public OVRInput.Controller controller;
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        float flex = OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger, controller);
        animator.SetFloat("Flex", flex);
        float pinch = OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger, controller);
        animator.SetFloat("Pinch", pinch);

    }
}
