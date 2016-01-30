using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SphereCollider))]
public class MissileLauncher : MonoBehaviour {
    public EnumDefaultTarget DefaultTarget;

    public bool OverrideMissileSettings;
    public float BaseSpeed;
    public float TurnSpeed;
    public bool IncludeYAxis;

    SphereCollider DetectionCollider;
    GameObject MissileGameObject;
	// Use this for initialization
	void Start () {
        DetectionCollider = GetComponent<SphereCollider>();
        if (!DetectionCollider)
            Debug.LogError("Cannot find sphere collider!");
        MissileGameObject = Resources.Load("Prefabs/MissileGameObject") as GameObject;
        if (!MissileGameObject)
            Debug.LogError("Cannot find missile game object");
	}
	
	// Update is called once per frame
	void Update () {
	
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

    void LaunchMissile(Transform st) {
        MissileAI missile = (Instantiate(MissileGameObject,transform.position,transform.rotation) 
            as GameObject).GetComponent<MissileAI>();
        if (OverrideMissileSettings)
            missile.Initialize(st, BaseSpeed, TurnSpeed, IncludeYAxis);
        Debug.Log("Launched missile at: " + st.name);
    }
}
