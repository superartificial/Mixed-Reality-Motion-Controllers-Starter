using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.WSA.Input;
using UnityEngine.Events;

/*
 * Singleton class that handles input from both motion controllers. 
 */
public class MotionControllerManager : MonoBehaviour
{
    private bool lastClickLeft = false;
    public UnityEvent padClicked; // Needs to be set up in Inspector, link to method to call when padClicked
    public bool debugMode = true;

    //Reference to singleton object
    private static MotionControllerManager ThisInstance = null;

    //Property for accessing single instance
    public static MotionControllerManager Instance
    {
        get
        {
            if (ThisInstance == null)
            {
                GameObject MotionControllerManagerObject = new GameObject("MotionControllerManager");
                ThisInstance = MotionControllerManagerObject.AddComponent<MotionControllerManager>();
            }

            return ThisInstance;
        }
    }

    void Awake()
    {
        //If single object already exists then destroy
        if (ThisInstance != null)
        {
            DestroyImmediate(gameObject);
            return;
        }
        //Make this single instance
        ThisInstance = this;
    }

    public static Vector3 GetHandPosition(bool left)
    {
        return InputTracking.GetLocalPosition((left) ? XRNode.LeftHand : XRNode.RightHand);
    }

    public static Quaternion GetHandRotation(bool left)
    {
        return InputTracking.GetLocalRotation((left) ? XRNode.LeftHand : XRNode.RightHand);
    }

    public static float GetTriggerValue(bool left)
    {
        return Input.GetAxis((left) ? "SelectTriggerLeft" : "SelectTriggerRight");
    }

    void Update()
    {
        // For walking action, keep track of what the last pad clicked was so that movement only occurs with alternate clicks
        // Shouldn't really be in this class which ideally would be restricted to managing the input only
        if (Input.GetButton("LeftPadDown") && !Input.GetButton("RightPadDown") && !lastClickLeft)
        {
            lastClickLeft = true;
            padClicked.Invoke();
        }
        if (Input.GetButton("RightPadDown") && !Input.GetButton("LeftPadDown") && lastClickLeft)
        {
            lastClickLeft = false;
            padClicked.Invoke();
        }

        if(debugMode)
        {
            Vector3 leftPosition = InputTracking.GetLocalPosition(XRNode.LeftHand);
            Quaternion leftRotation = InputTracking.GetLocalRotation(XRNode.LeftHand);
            Vector3 rightPosition = InputTracking.GetLocalPosition(XRNode.RightHand);
            Quaternion rightRotation = InputTracking.GetLocalRotation(XRNode.RightHand);
            HUDDebug.AddMessage("Left Position: " + leftPosition + " Left Rotation: " + leftRotation);
            HUDDebug.AddMessage("Right Position: " + rightPosition + " Right Rotation: " + rightRotation);

        }

        // Debug.Log(leftPosition + " " + leftRotation + " " + rightPosition + " " + leftHorz);
        //var interactionSourceStates = InteractionManager.GetCurrentReading();
        //Debug.Log(interactionSourceStates);

    }
}