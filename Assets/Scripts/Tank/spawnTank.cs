using UnityEngine;
using System.Collections;

public class spawnTank : MonoBehaviour {

    GameObject tankSpawnPad;
    GameObject Caller;
    GameObject Here;
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
        Caller = GameObject.Find("Spawner");
        Here = GameObject.Find("spawnTankZone");

    }

    // Use this for initialization
    void Start()
    {
        StartCoroutine(hold(1f));
    }

    // Update is called once per frame
    void Update () {
    }

    void spawnerTank()
    {

        if (Headquarter.Instance().transform == null)
            StartCoroutine(hold(3f));
        else
        {
            
            Vector3 vectorTankPad = locationtankSpawnPad.position;
            Instantiate(Tank, vectorTankPad, Tank.transform.rotation);
            
            //Caller.GetComponent<SpawningUnits>().DropLocation(Here.transform.position);
        }
    }
}
