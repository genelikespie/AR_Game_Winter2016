using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class RotateReticule : MonoBehaviour {

    public Image reticule;
    public Color start;
    public Color end;
    Color current;

    float value;
    public float maxReticuleValue = 2;
    public float currReticuleValue;
    public bool updateWithTime;
    float reducedValue;
    public bool isActive;

    //Reticule Health
    public float targetValue;
    public float previousValue;
    public bool updateWithValue;
    public bool showStart;

	// Use this for initialization
	void Start () {
        reticule.type = Image.Type.Filled;
        reticule.fillMethod = Image.FillMethod.Radial360;
        reticule.fillOrigin = 0;
        value = 0;
        currReticuleValue = maxReticuleValue;
        if (showStart == true)
        reticule.color = end;
    }
	
	// Update is called once per frame
	void Update () {
        if (updateWithTime && isActive)
        {
            currReticuleValue -= Time.deltaTime;
            value = currReticuleValue / maxReticuleValue;
            //Debug.Log(value);
            if (value < 0)
            {
                value = 0;
            }
            reticule.fillAmount = Mathf.Max(value, 0.001f);
            reticule.color = Color.Lerp(start, end, value);
            current = Color.Lerp(start, end, value);
        }
        else if(updateWithValue == true)
        {
            float ratio = Mathf.Max(currReticuleValue / targetValue, targetValue / currReticuleValue);
            currReticuleValue = Mathf.Lerp(previousValue, targetValue, ratio);
            value = currReticuleValue / maxReticuleValue;
            if (value < 0.1f)
            {
                value = 0;
            }
            //Debug.Log(value);
            reticule.fillAmount = Mathf.Max(value, 0.001f);
            reticule.color = Color.Lerp(start, end, value);
            current = Color.Lerp(start, end, value);
            if (ratio > .9)
            {
                updateWithValue = false;
                currReticuleValue = targetValue;
            }
        }
    }

    public void ChangeValue (float value)
    {
        previousValue = currReticuleValue;
        targetValue = value;
        updateWithValue = true;
    }

    public void SetTimer(float maxTime)
    {
        maxReticuleValue = maxTime;
        currReticuleValue = maxReticuleValue;
    }
    public void RestartTimer()
    {
        currReticuleValue = maxReticuleValue;
        isActive = true;
    }
    public void ResetTimer()
    {
        currReticuleValue = maxReticuleValue;
        isActive = false;
    }
    public void BeginTimer()
    {
        isActive = true;
    }
    public void StopTimer()
    {
        isActive = false;
    }
    public void HideImage()
    {
        Color curColor = GetComponent<Image>().color;
        GetComponent<Image>().color = new Color(curColor.r, curColor.g, curColor.b, 0);
    }
    public void ShowImage()
    {
        Color curColor = GetComponent<Image>().color;
        GetComponent<Image>().color = new Color(curColor.r, curColor.g, curColor.b, 1);
    }
}
