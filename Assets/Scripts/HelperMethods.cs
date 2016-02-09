using UnityEngine;
using System.Collections;

public static class HelperMethods {

    public static T FindComponentInChildWithTag<T>(this GameObject parent, string tag) where T : Component
    {
        Transform t = parent.transform;
        foreach (Transform trans in t)
        {
            if (trans.tag == tag)
            {
                return trans.GetComponent<T>();
            }
        }
        return null;
    }

    public static GameObject FindChildWithTag(GameObject parent, string tag) 
    {
        Transform t = parent.transform;
        foreach (Transform trans in t)
        {
            if (trans.tag == tag)
            {
                return trans.gameObject;
            }
        }
        return null;
    }

    public static GameObject FindChildWithName(GameObject parent, string name)
    {
        Transform t = parent.transform;
        foreach (Transform trans in t)
        {
            if (trans.name == name)
            {
                return trans.gameObject;
            }
        }
        return null;
    }

}
