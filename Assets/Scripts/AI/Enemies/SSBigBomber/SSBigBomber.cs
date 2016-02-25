using UnityEngine;
using System.Collections;

public class SSBigBomber : SpaceShip {

    // Crosshair that points to the ground for our player to see
    public GameObject crossHairObject;
    private GameObject myTargetDetectionSphere;
    private GameObject myCrossHair;
    private BaseMissileLauncher myMissileLauncher;

    // Use this for initialization

    new void Awake()
    {
        base.Awake();
        crossHairObject = transform.Find("SSBigBomberRedCross").gameObject;
        if (!crossHairObject)
            Debug.LogError("Cannot find RedCross!");
        myTargetDetectionSphere = transform.Find("TargetDetectionSphere").gameObject;
        if (!myTargetDetectionSphere)
            Debug.LogError("Cannot find TargetDetectionSphere!");
        myTargetDetectionSphere.GetComponent<DetectionSphere>().Initialize(gameObject, "Headquarter");
        myMissileLauncher = GetComponent<BaseMissileLauncher>();
        if (!myMissileLauncher)
            Debug.LogError("Cannot find missilelauncher class!");
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

    public void ReceiveDetectionSphereEvent(GameObject collider)
    {
        Debug.Log("received detection event: " + collider.name);
        myMissileLauncher.LaunchMissile(collider.transform);
        //(collider).GetComponent<Headquarter>().Hit(10);
    }

}
