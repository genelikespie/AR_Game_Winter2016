using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;

public class SSBigBomber : SpaceShip {

    // Crosshair that points to the ground for our player to see
    public GameObject crossHairObject;
    private GameObject myCrossHair;

    // Use this for initialization

    new void Awake()
    {
        base.Awake();
        Assert.IsTrue(crossHairObject);
    }
    new void Start()
    {
        base.Start();
        Assert.IsTrue(crossHairObject);
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
    }
}
