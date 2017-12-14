using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
* Defines an object that can be drawn to the user and picked up 
* Most of the logic for this is in the HandPointer class (possibly should be moved to this one)
*/
[RequireComponent(typeof(Rigidbody))]
public class Pickupable : MonoBehaviour, IReactToCursor
{
    
    public void CursorEnter(RaycastHit hitInfo) {
        GetComponent<Rigidbody>().useGravity = false;
    }
    public void CursorOn(RaycastHit hitInfo, bool active) { }
    public void CursorExit() {
        GetComponent<Rigidbody>().useGravity = true;
    }
    public void CursorHit(RaycastHit hitInfo) { }
}
