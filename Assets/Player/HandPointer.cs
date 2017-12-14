using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandPointer : MonoBehaviour {

    public float MaxLength = 100;
    private Transform thisTransform = null;
    private Transform startTransform = null;
    private LineRenderer line;
    private bool left = true;

    public float pickupForce = 10;
    public float holdDistance = 1.5f;
    public float maxGrabVelocity = 5f;

    private IReactToCursor cursorObject = null;

    // Use this for initialization
    void Start () {
        thisTransform = GetComponent<Transform>();
        line = GetComponentInChildren<LineRenderer>();
        startTransform = line.transform;
        line.enabled = false;
        left = GetComponent<HandMovement>().left;
    }
	

	void FixedUpdate () {
        // check if a coroutine should be used for this...
        // Trigger values are scalar (increase the more you push down), to use as on / off button taking it as on when 90% or more down
        if (MotionControllerManager.GetTriggerValue(left) > 0.9f)
        {

            // HUDDebug.AddMessage("TRIGGER DOWN: " + ((left) ? "LEFT" : "RIGHT") );
            // HUDDebug.AddMessage("RAY ORIGIN: " + ray.origin + " TRANSFORM START: " + (startTransform.position));
            Ray ray = new Ray(startTransform.position, startTransform.forward);
            RaycastHit hit;
            // whenever trigger is down we enable the line renderer on this hand
            line.enabled = true;
            line.SetPosition(0, ray.origin); // line start position, at hand location
            // If hit anything we carry out any options needed on that object, otherwise drop any object currently in focus
            if (Physics.Raycast(ray, out hit, MaxLength))
            {
                line.SetPosition(1, hit.point); // line end position, at hit location (MaxLength distance from hand if nothing hit)
                ReactToCursor(hit, ray.direction); 
            }
            else
            {
                Drop();
                line.SetPosition(1, ray.GetPoint(MaxLength));
            }

        } else
        {
            line.enabled = false;
            Drop();
        }
    }

    private void Drop()
    {
        if (cursorObject != null)
        {
            cursorObject.CursorExit();
            cursorObject = null;
            startHold = true;
        }
    }

    bool startHold = true;

    private void ReactToCursor(RaycastHit hit, Vector3 direction)
    {
        // If the object has any curser interaction supported it will implement the IReactToCursor interface
        IReactToCursor irc = hit.transform.GetComponent<IReactToCursor>();
        if (irc != null)
        {
            // If we have hit a new object this frame, if cursor was on a previous object notify that it has now left, notify new object that cursor has entered
            if (irc != cursorObject)
            {
                if (cursorObject != null) cursorObject.CursorExit();
                cursorObject = irc;
                cursorObject.CursorEnter(hit);
            }
            // Logic for objects that can be picked up, probably should be moved to Pickupable class
            Pickupable pickup = hit.transform.GetComponent<Pickupable>();
            if(pickup!=null)
            {
                Rigidbody hitRigidBody = hit.transform.GetComponent<Rigidbody>();
                // If the object is further away from the hold distance, draw towards user 
                // (hold distance allows objects to be held a little way in front of the user)
                if(hit.distance > holdDistance && hitRigidBody.velocity.magnitude < maxGrabVelocity)
                {
                    hitRigidBody.AddForce((direction).normalized * -pickupForce, ForceMode.Force);
                }
                else if(hit.distance <= holdDistance)
                {
                    // if(startHold) hitRigidBody.velocity = Vector3.zero;
                    // Allows user to hold object holdDistance away from hand, and allows throwing of object when let go
                    // value of 40 just from trial & error, needs to be refined and turned into a variable
                    hitRigidBody.velocity = ((startTransform.position + (direction).normalized * holdDistance) - hitRigidBody.position) * 40;

                    // hitRigidBody.MovePosition(startTransform.position + (direction).normalized * holdDistance);

                    startHold = false;
                }
            }
        }
    }
}
