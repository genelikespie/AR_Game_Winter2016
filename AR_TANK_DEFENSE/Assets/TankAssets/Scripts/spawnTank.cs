using UnityEngine;
using System.Collections;

public class spawnTank : MonoBehaviour {

    GameObject tankSpawnPad;
    public GameObject Tank;
    Transform locationtankSpawnPad;
    public bool spawnOnce;

    IEnumerator hold(float timetohold)
    {
        yield return new WaitForSeconds(timetohold);
        spawnerTank();
    }


	void Awake () {
        tankSpawnPad = GameObject.Find("spawnTankZone");
        locationtankSpawnPad = tankSpawnPad.transform;

    }

    // Use this for initialization
    void Start()
    {
        StartCoroutine(hold(2f));
    }
	
	// Update is called once per frame
	void Update () {
    }

    void spawnerTank()
    {
        Vector3 vectorTankPad = locationtankSpawnPad.position;
        Instantiate(Tank,vectorTankPad,Tank.transform.rotation);
        Debug.Log("TANK CREATED");
    }
}
