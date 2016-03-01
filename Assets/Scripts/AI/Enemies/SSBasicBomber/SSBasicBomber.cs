using UnityEngine;
using System.Collections;

public class SSBasicBomber : SpaceShip {

    // Crosshair that points to the ground for our player to see
    public GameObject crossHairObject;
    private GameObject myCrossHair;

    // Use this for initialization

    new void Awake()
    {
        base.Awake();
        crossHairObject = transform.Find("RedCross").gameObject;
        if (!crossHairObject)
            Debug.LogError("Cannot find RedCross!");
    }
    new void Start()
    {
        base.Start();
        if (crossHairObject)
        {
            myCrossHair = crossHairObject;
            myCrossHair.transform.localPosition = new Vector3(0, myCrossHair.transform.localPosition.y, 0);
        }
        else
            Debug.LogError("No crosshair found!");
        
    }

    void LateUpdate()
    {
        // Update crosshair position after normal update in case it gets offset
        //myCrossHair.transform.localPosition = new Vector3(0, myCrossHair.transform.localPosition.y, 0);
    }
}
