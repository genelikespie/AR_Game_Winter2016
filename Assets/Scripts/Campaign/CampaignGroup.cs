using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// This object holds a collection of campaign stages
/// The GameManager chooses which campaign to load and the CampaignGroup
/// loads to proper stage
/// </summary>
public class CampaignGroup : MonoBehaviour {

    // List that stores all child campaign stages
    List<CampaignStage> childStages;
    public Transform tankSpawnLocation;
    public GameObject playerTankPrefab;
    private CampaignStage currentStage;
    private SpawningUnits unitSpawner;
    private bool canStartCampaign;

    private bool start = true;
    private int numOfStages;
    private int currentStageIndex;
    private GameObject unitSpawnerPrefab;
    void Awake()
    {
        childStages = new List<CampaignStage>();
        unitSpawnerPrefab = Resources.Load("Prefabs/UnitSpawner") as GameObject;
        //playerTankPrefab = Resources.Load("Prefabs/Pla") as GameObject;
        IEnumerable stages = transform.GetComponentsInChildren<CampaignStage>();
        foreach (CampaignStage s in stages)
        {
            childStages.Add(s);
            s.Initialize(this);
        }
        numOfStages = childStages.Count;
        currentStageIndex = 0;
        canStartCampaign = true;
        Assert.IsTrue(tankSpawnLocation && unitSpawnerPrefab && playerTankPrefab);
    }
    void Start()
    {
        // create campaign manager's unit spawner
        unitSpawner = (Instantiate(unitSpawnerPrefab, new Vector3(0, -100, 0), Quaternion.identity) as GameObject).GetComponent<SpawningUnits>();
        unitSpawner.gameObject.SetActive(false);

    }
    public void StartCampaign()
    {
        if (!canStartCampaign)
            return;
        currentStageIndex = 0;
        childStages[currentStageIndex].BeginCurrStage();
        Debug.Log("BEGINNING CAMPAIGN " + name);
        unitSpawner.gameObject.SetActive(true);
        unitSpawner.DropLocation(tankSpawnLocation.position, playerTankPrefab);
        canStartCampaign =  false;

    }

    public void NotifyStageCompleted(CampaignStage stage)
    {
        currentStageIndex++;
        if (currentStageIndex >= numOfStages)
        {
            Debug.Log("PLAYER BEAT CAMPAIGN " + name);
            GameManagerScript.Instance().PlayerWon();
            canStartCampaign = true;
            return;
        }
        childStages[currentStageIndex].BeginCurrStage();
    }
}
