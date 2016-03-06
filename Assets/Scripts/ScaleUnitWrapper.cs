using UnityEngine;
using System.Collections;

// Use this to animate drop down units from the sky
public class ScaleUnitWrapper : MonoBehaviour {

    Vector3 originalScale;
    Vector3 startScale;
    Vector3 endScale;
    public float scaleTime = 1;

    bool scaleDown;
    bool ready;
    float currTime;
    
    void Awake()
    {
        originalScale = transform.localScale;
    }
    void Update()
    {
        if (!ready)
        {
            currTime += Time.deltaTime;
            float ratio = currTime/scaleTime;
            if (ratio >= 1)
            {
                transform.localScale = endScale;
                ready = true;
                if (scaleDown)
                {
                    gameObject.SendMessage("FinishedScalingDown", SendMessageOptions.DontRequireReceiver);
                }
                else
                    gameObject.SendMessage("FinishedScalingUp", SendMessageOptions.DontRequireReceiver);
                return;
            }
            else
                transform.localScale = Vector3.Lerp(startScale, endScale, ratio);
        }
    }
    public void Scale( bool down)
    {
        ready = false;
        scaleDown = down;
        currTime = 0;
        if (scaleDown)
        {
            startScale = originalScale;
            transform.localScale = startScale;
            endScale = Vector3.zero;
        }
        else
        {
            startScale = Vector3.zero;
            transform.localScale = startScale;
            endScale = originalScale;
        }
    }
}
