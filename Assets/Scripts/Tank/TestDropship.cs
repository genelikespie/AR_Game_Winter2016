using UnityEngine;
using System.Collections;

public class TestDropship : MonoBehaviour {

    public bool SpawnNow = false;
    public GameObject Tank;
    GameObject Caller;
    GameObject Here;
    Transform locationtankSpawnPad;
    // Use this for initialization
    void Start () {
	
	}
    void Awake()
    {
        Caller = GameObject.Find("Spawner");
        Here = GameObject.Find("spawnTankZone");
    }
	
	// Update is called once per frame
	void Update () {
	if (SpawnNow == true)
        {
            SpawnNow = false;
            Caller.GetComponent<SpawningUnits>().DropLocation(Here.transform.position, Tank);
        }
	}
}
