using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Moves player forward in current look direction with alternate (left / right) pad clicks
 */
public class PlayerMovement : MonoBehaviour {

    private CharacterController controller;
    public Transform cameraTransform;
    // Each step (pad click) moves player forward at a speed of stepSpeed for a duraction of stepTime - so length of each step is these multiplied
    public float stepSpeed = 10f; 
    public float stepTime = 0.1f;

    public AudioClip[] footstepClips;

    private float endStepTime = 0;
    private const float gravity = -9.81f;
    private AudioSource audioSource;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        audioSource = GetComponentInChildren<AudioSource>();
    }

    private void FixedUpdate()
    {        
        Vector3 projectedLookDirection = Vector3.zero;
        // If the end step time is greater than the current time, then we should be part way through a step
        if (endStepTime > Time.time)
        {
            // Use project on plane to get step direction so that we only aim to walk in the horizontal plane 
            // (CharacterController will take care of moving us approiately on slopes and steps)
            projectedLookDirection = Vector3.ProjectOnPlane(cameraTransform.forward, Vector3.up).normalized * stepSpeed;
            // Debug.DrawRay(transform.position, projectedLookDirection * 20, Color.green);
        }
        controller.Move(new Vector3(projectedLookDirection.x * Time.deltaTime, gravity * Time.deltaTime, projectedLookDirection.z * Time.deltaTime));
    }

    // Called from padClicked event MotionContollerManager script on InputManager Unity object. 
    public void StepForward()
    {
        endStepTime = Time.time + stepTime;
        audioSource.PlayOneShot(footstepClips[Random.Range(0, footstepClips.Length - 1)]);
    }
}