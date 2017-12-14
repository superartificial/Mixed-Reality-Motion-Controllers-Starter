using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Add to both hand objects to set location and position 
 */
public class HandMovement : MonoBehaviour {

    public bool left = true;
	
    private Transform thisTransform = null;

    private void Start()
    {
        thisTransform = GetComponent<Transform>();
    }

    void Update () {
        thisTransform.localPosition = MotionControllerManager.GetHandPosition(left);
        thisTransform.localRotation = MotionControllerManager.GetHandRotation(left);
    }
}
