using UnityEngine;
using System.Collections;

public class SSBasicBomber : SpaceShip {

    // Crosshair that points to the ground for our player to see
    public GameObject crossHairObject;
    private GameObject myCrossHair;

    // Use this for initialization
    void Start()
    {
        base.Start();

        crossHairObject = GameObject.Find("RedCross");
        if (crossHairObject)
        {
            myCrossHair = Instantiate(crossHairObject, new Vector3(transform.position.x, 1, transform.position.z), transform.rotation) as GameObject;
            myCrossHair.transform.Rotate(new Vector3(90, 0, 0));
            if (myCrossHair)
                myCrossHair.transform.SetParent(this.transform);
            else
                Debug.LogError("No crosshair found!");
            myCrossHair.transform.localPosition = new Vector3(0, myCrossHair.transform.localPosition.y, 0);
        }
        else
            Debug.LogError("No crosshair found!");
        
    }

    void LateUpdate()
    {
        // Update crosshair position after normal update in case it gets offset
        myCrossHair.transform.localPosition = new Vector3(0, myCrossHair.transform.localPosition.y, 0);
    }
}
