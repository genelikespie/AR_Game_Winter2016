using UnityEngine;
using System.Collections;
using System.Collections;

public class Timer : MonoBehaviour {

    public bool timerDone;
    bool isActive = false;
    float currTime;
    float maxTime;


    public void Initialize(float time)
    {
        SetTime(time);
        Restart();
    }
    public void SetTime(float time)
    {
        maxTime = time;
    }
    public void Play()
    {
        isActive = true;
    }
    public void Pause()
    {
        isActive = false;
    }
    public void Restart()
    {
        isActive = false;
        currTime = maxTime;
        timerDone = false;
        Play();
    }
	// Update is called once per frame
	void Update () {
        if (isActive && !timerDone)
        {
            currTime = currTime - Time.deltaTime;
            if (currTime <= 0)
                timerDone = true;
        }
	}

}
