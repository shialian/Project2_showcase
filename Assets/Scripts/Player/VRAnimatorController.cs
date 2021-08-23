using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRAnimatorController : MonoBehaviour
{
    public float speedThreshold = 0.1f;
    [Range(0, 1)]
    public float smoothing = 1f;
    public CharacterController character;

    private Animator animator;
    private Vector3 previousPos;
    private VRRig vrRig;

    private void Start()
    {
        animator = GetComponent<Animator>();
        vrRig = GetComponent<VRRig>();
        previousPos = vrRig.head.vrTarget.position;
    }

    private void FixedUpdate()
    {
        // Get current velocity
        Vector3 velocity = character.velocity;

        animator.SetBool("isMoving", velocity.magnitude > speedThreshold);
        animator.SetFloat("DirectionX", velocity.x);
        animator.SetFloat("DirectionY", velocity.z);
    }
}
