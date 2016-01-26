using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SphereCollider))]
public class MissileLauncher : MonoBehaviour {
    public EnumDefaultTarget DefaultTarget;

    SphereCollider DetectionCollider;
    GameObject MissileGameObject;
	// Use this for initialization
	void Start () {
        DetectionCollider = GetComponent<SphereCollider>();
        if (!DetectionCollider)
            Debug.LogError("Cannot find sphere collider!");
        MissileGameObject = GameObject.Find("MissileGameObject");
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
        missile.Initialize(st, 40.0f, 40.0f, true);
        Debug.Log("Launched missile at: " + st.name);
    }
}
