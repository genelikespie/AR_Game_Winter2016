using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;

public class AISpawnerLocation : MonoBehaviour {

    Bounds spawnBounds;
    Vector3 boundSize;
    Vector3 boundExtent;

    // Use this for initialization
    void OnEnable()
    {
        spawnBounds = GetComponent<Collider>().bounds;
        boundSize = spawnBounds.size;
        boundExtent = spawnBounds.extents;
        if (!GetComponent<Collider>())
            Debug.LogError("NOPE)");
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(boundSize);
    }

    public GameObject Spawn(GameObject AIShip)
    {
        Vector3 spawnLocation = spawnBounds.center;
        Quaternion spawnRotation = Quaternion.LookRotation(spawnLocation.normalized);
        spawnLocation.x = spawnLocation.x + (Random.value * boundSize.x - boundExtent.x);
        spawnLocation.y = spawnLocation.y + (Random.value * boundSize.y - boundExtent.y);
        spawnLocation.z = spawnLocation.z + (Random.value * boundSize.z - boundExtent.z);
        //Debug.Log(spawnLocation + " center: " + spawnBounds.center);
        Assert.IsNotNull(AIShip);
        return (Instantiate(AIShip, spawnLocation, spawnRotation) as GameObject);

    }

    public Transform Relocate(Transform AIShipTransform)
    {
        Assert.IsNotNull(AIShipTransform);
        Vector3 spawnLocation = spawnBounds.center;
        Quaternion spawnRotation = Quaternion.LookRotation(spawnLocation.normalized);
        spawnLocation.x = spawnLocation.x + (Random.value * boundSize.x - boundExtent.x);
        spawnLocation.y = spawnLocation.y + (Random.value * boundSize.y - boundExtent.y);
        spawnLocation.z = spawnLocation.z + (Random.value * boundSize.z - boundExtent.z);
        //Debug.Log(spawnLocation + " center: " + spawnBounds.center);
        AIShipTransform.position = spawnLocation;
        AIShipTransform.rotation = spawnRotation;
        return AIShipTransform;

    }
}
