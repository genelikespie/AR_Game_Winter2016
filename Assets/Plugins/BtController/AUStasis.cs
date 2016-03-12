using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;


public class AUStasis : MonoBehaviour {

    float journeyLength;
    bool journeyDir;

    float startTime;
    float gametime;
    public bool needToCharge = false;
    public float prevValue;
    public GameObject scaleMe;
    public bool notDoneChanging = false;
    public bool sensorExists = false;

    public float currBallPower;
    public float goalValue;
    public float minRadius = 3f;
    float defaultRadius = 5.5f;
    public float maxRadius = 9f;
    public bool doOnce = false;

    IEnumerator CoolDown(float wait)
    {
        yield return new WaitForSeconds(wait);
        notDoneChanging = false;
        FireCharge();
    }



    // Use this for initialization
    void Start () {
        goalValue = defaultRadius;
        currBallPower = minRadius;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        gametime += Time.deltaTime;

        if (sensorExists == true)
        {
            if (Input.GetMouseButtonDown(0) == true)
                PowerUp(100f);
            if (Input.GetMouseButtonDown(1) == true)
                FireCharge();
        }



        if (needToCharge == true)
        {

            float currentDuration = (Time.time - startTime); //* speed;
            float journeyFraction = currentDuration / journeyLength;
            currBallPower = Mathf.Lerp(prevValue, goalValue, journeyFraction);
            float maxsafely = goalValue * .95f;
            float minsafely = goalValue + (goalValue * .04f)+ .001f;
            if (currBallPower > maxsafely && journeyDir == true)
            {
                needToCharge = false;
                currBallPower = goalValue;
              //  notDoneChanging = false;
                goalValue = minRadius;
                StartCoroutine(CoolDown(2f));      

            }

            if (currBallPower < minsafely && journeyDir == false)
            {
                needToCharge = false;
                currBallPower = goalValue;
                
                if (sensorExists == false)
                {
                    goalValue = defaultRadius;
                    StartCoroutine(CoolDown(3f));
                }
                else
                    notDoneChanging = false;

            }
        }

        this.GetComponent<SphereCollider>().radius = currBallPower;
        scaleMe.GetComponent<Transform>().localScale = new Vector3(currBallPower *2f,currBallPower * 2f,currBallPower *2f);

        if (sensorExists == false && doOnce == false && gametime > .5)
        {
            doOnce = true;
            FireCharge();
        }
    }

    public void FireCharge()
    {
        if (notDoneChanging == false)
        {
            notDoneChanging = true;
            prevValue = currBallPower;
            journeyLength = Mathf.Abs(goalValue - currBallPower);
            if (goalValue < currBallPower)
                journeyDir = false; //go negative
            else
                journeyDir = true;
            startTime = Time.time;
            needToCharge = true;
        }

    }

    public void PowerUp(float value)
    {
        if (notDoneChanging == false && goalValue < maxRadius)
        {
            goalValue += (50*Time.deltaTime) / value;

        }

    }

    public void ActivateSensor(bool value)
    {
        sensorExists = value;
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
