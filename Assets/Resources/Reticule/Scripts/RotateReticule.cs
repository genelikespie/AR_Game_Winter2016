using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class RotateReticule : MonoBehaviour {

    public Image reticule;
    public Color start;
    public Color end;
    Color current;

    float value;
    public float reticuleTime = 2;
    float currentTime;

	// Use this for initialization
	void Start () {
        reticule.type = Image.Type.Filled;
        reticule.fillMethod = Image.FillMethod.Radial360;
        reticule.fillOrigin = 0;
        value = 0;
        currentTime = reticuleTime;
    }
	
	// Update is called once per frame
	void Update () {
        currentTime -= Time.deltaTime;
        value = currentTime / reticuleTime;
        //Debug.Log(value);
        if (value < 0)
        {
            value = 0;
            currentTime = reticuleTime;
        }
       /* value = value + (float)0.1;
        if (value > 1.0)
        {
            value = 0;
        }*/
        reticule.fillAmount = Mathf.Max(value, 0.001f);
        reticule.color = Color.Lerp(start, end, value);
        current = Color.Lerp(start, end, value);
    }
}
