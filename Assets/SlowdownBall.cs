using UnityEngine;
using System.Collections;

public class SlowdownBall : MonoBehaviour {

    public float inputCharge;
    float journeyLength;
    bool journeyDir;
    public bool charging = false;
    public float currBallPower;
    public float goalValue;
    float startTime;
    public bool notBeingModified;
    public bool regularState = true;
    public bool needToCharge = false;
    float maxValue;
    public float prevValue;
    public GameObject scaleMe;

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
        {
            prevValue = currBallPower;
            journeyLength = Mathf.Abs(goalValue - currBallPower);
            if (goalValue < currBallPower)
                journeyDir = false; //go negative
            else
                journeyDir = true;
            startTime = Time.time;
        }

        if (needToCharge == true)
        {

            float currentDuration = (Time.time - startTime);
            float journeyFraction = currentDuration / journeyLength;
           // float ratio = Mathf.Max(currBallPower / goalValue, goalValue / currBallPower);
            currBallPower = Mathf.Lerp(prevValue, goalValue, journeyFraction);
            float maxsafely = goalValue * .95f;
            float minsafely = goalValue + (goalValue * .04f)+ .001f;
            if (currBallPower > maxsafely && journeyDir == true)
            {
                needToCharge = false;
                currBallPower = goalValue;
            }

            if (currBallPower < minsafely && journeyDir == false)
            {
                needToCharge = false;
                currBallPower = goalValue;
            }
        }

        this.GetComponent<SphereCollider>().radius = currBallPower;
        scaleMe.GetComponent<Transform>().localScale = new Vector3(currBallPower *2f,currBallPower * 2f,currBallPower *2f);
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
