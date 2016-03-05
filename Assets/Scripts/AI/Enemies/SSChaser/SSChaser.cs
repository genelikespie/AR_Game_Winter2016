using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;

public class SSChaser : SpaceShip {

    // Crosshair that points to the ground for our player to see
    public GameObject myCrossHair;

    // Use this for initialization

    new void Awake()
    {
        base.Awake();
        Assert.IsTrue(myCrossHair);
        myCrossHair.transform.localPosition = new Vector3(0, myCrossHair.transform.localPosition.y, 0);
    }
    new void Start()
    {
        base.Start();
    }
}
