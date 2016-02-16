using UnityEngine;
using System.Collections;

// Attach to a child object to send events from the child's trigger collider to its parent
// Parent must have a ReceiveDetectionSphereEvent method
public class DetectionSphere : MonoBehaviour {
    // parent object to receive events
    GameObject parent;
    // tag of the collider to check
    string colliderTag;
    bool initialized = false;
    public void Initialize(GameObject p, string tag)
    {
        parent = p;
        colliderTag = tag;
        initialized = true;
    }

    void OnTriggerEnter (Collider c) {
        Debug.Log("detected: " + c.name + " init: " + initialized + " tag: " + c.tag);
        if (initialized && c.tag == colliderTag && parent)
        {
            parent.SendMessage("ReceiveDetectionSphereEvent", c.gameObject, SendMessageOptions.RequireReceiver);
        }
    }
}
