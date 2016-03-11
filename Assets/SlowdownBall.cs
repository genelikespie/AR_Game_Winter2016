using UnityEngine;
using System.Collections;

public class SlowdownBall : MonoBehaviour {

    public float inputCharge;
    public bool charging = false;
    public float currBallPower;
    public float goalValue;
    float startTime;
    public bool notBeingModified;
    public bool regularState = true;
    public bool needToCharge = false;
    float maxValue;
    public float prevValue;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        /*
        //if accepting input
         if   (charging)
        {
            if (inputCharge < 4)
            {
                currBallPower = 4f;
                maxValue = 4;

            }
            else if (inputCharge > 9)
            {
                currBallPower = 9f;
                maxValue = 9f;
            }
            else
            {
                currBallPower = inputCharge;
                maxValue = 9f;
            }

            this.GetComponent<SphereCollider>().radius = currBallPower;
            
        }
        else if (!charging)
        {
            if (currBallPower < 5.5f)
            {
                needToCharge = true;
                goalValue = 5.5f;
                maxValue = 5.5f;
            }
            else
                needToCharge = false;
            this.GetComponent<SphereCollider>().radius = 5.5f;
        }
        */
        if (needToCharge == false)
            prevValue = currBallPower;

        if (needToCharge == true)
        {
            float ratio = Mathf.Max(currBallPower / goalValue, goalValue / currBallPower);
            currBallPower = Mathf.Lerp(prevValue, goalValue, ratio);
            if (ratio > .9)
            {
                needToCharge = false;
                currBallPower = goalValue;
            }
        }

        this.GetComponent<SphereCollider>().radius = currBallPower;
    }

    void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Enemy")
        {
         //   other.GetComponent<SLOWDOWNSHTUFF>().value = input;
            print("SLOW DOWN NOW!!!!!!");
        }
    }

    void OnTriggerExit(Collider other)
    {

        if (other.tag == "Enemy")
        {
            //   other.GetComponent<SLOWDOWNSHTUFF>().value = 0;
            print("REMOVE SLOW FIELD!!!!!!");
        }
    }
}
