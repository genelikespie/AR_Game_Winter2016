using UnityEngine;
using System.Collections;


/// <summary>
/// Attach this script to an object that launches missiles
/// </summary>
public class BaseMissileLauncher : MonoBehaviour
{
    public float missileBaseSpeed;
    public float missileTurnSpeed;
    public float missileExplosionRadius;
    public float missileMaxTimeToLive;
    public float missileDamage;
    public bool IncludeYAxis;

    public GameObject MissileGameObject;
    // Use this for initialization
    void Start()
    {
        //MissileGameObject = Resources.Load("Prefabs/MissileGameObject") as GameObject;
        if (!MissileGameObject)
            Debug.LogError("Cannot find missile game object");
    }

    public void LaunchMissile(Transform missileTarget)
    {
        MissileAI missile = (Instantiate(MissileGameObject, transform.position, transform.rotation)
            as GameObject).GetComponent<MissileAI>();
        missile.Initialize(missileTarget, missileBaseSpeed, missileTurnSpeed, IncludeYAxis, missileMaxTimeToLive, missileExplosionRadius, missileDamage);
        Debug.Log("Launched missile at: " + missileTarget.name);
    }
}
