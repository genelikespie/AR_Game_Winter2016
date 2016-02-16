using UnityEngine;
using System.Collections;

/// <summary>
/// This script detects a target and launches missiles at said target
/// </summary>
[RequireComponent(typeof(SphereCollider))]
public class MissileLauncher : BaseMissileLauncher {

    public EnumDefaultTarget DefaultTarget;

    SphereCollider DetectionCollider;
	// Use this for initialization
	void Start () {
        DetectionCollider = GetComponent<SphereCollider>();
        if (!DetectionCollider)
            Debug.LogError("Cannot find sphere collider!");
	}
	
    void OnTriggerEnter(Collider collider)
    {
        Transform setTarget;
        if (DefaultTarget == EnumDefaultTarget.testship)
        {
            if (collider.GetComponent<SpaceShipAI>())
            {
                setTarget = collider.transform;
                LaunchMissile(setTarget);
            }
            
        }

    }
}
