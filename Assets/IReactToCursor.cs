using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Defines methods that the cursor calls on objects that it interacts with
 * Different classes can implement this in their own way so differnt object types can have their own sets of interactions
 */
public interface IReactToCursor {

    void CursorEnter(RaycastHit hitInfo);
    void CursorOn(RaycastHit hitInfo,bool active);
    void CursorHit(RaycastHit hitInfo);
    void CursorExit();

}
