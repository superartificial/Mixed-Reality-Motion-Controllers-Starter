using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Just applies gravity to an object when touched with cursor 
 */
[RequireComponent(typeof(Rigidbody))]
public class PointAndDrop : MonoBehaviour, IReactToCursor
{

    public void CursorEnter(RaycastHit hitInfo) {
        GetComponent<Rigidbody>().useGravity = true;
    }

    public void CursorOn(RaycastHit hitInfo, bool active){}

    public void CursorExit() {}

    public void CursorHit(RaycastHit hitInfo) { }
}
