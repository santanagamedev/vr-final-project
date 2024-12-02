using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugTransformLogger : MonoBehaviour {
    void Update() {
        // Log this object's (child) world position
        Debug.Log("Child Position (World): " + transform.position);

        // Log parent's world position, if it exists
        if (transform.parent != null) {
            Debug.Log("Parent Position (World): " + transform.parent.position);
        }
    }
}

