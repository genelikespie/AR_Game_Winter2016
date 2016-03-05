using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CampaignStage : MonoBehaviour {
    public string stageDescription;

    // List that stores all child campaign stages
    private List<StageWave> childWaves;
    private int numOfWaves;
    private int currentWaveIndex;
    // Private StageWave currentWave;
    private CampaignGroup parentGroup;

    //Timer waveTimer;
    bool beginNextWave;

    void Awake()
    {
        childWaves = new List<StageWave>();
        IEnumerable waves = transform.GetComponentsInChildren<StageWave>();
        foreach (StageWave w in waves)
        {
            childWaves.Add(w);
            w.Initialize(this);
        }
        numOfWaves = childWaves.Count;
        currentWaveIndex = 0;

        if (numOfWaves <= 0)
            Debug.LogError("num of waves is empty! CampaignStage" + name);
        beginNextWave = false;
    }

    void Update()
    {

    }

    /// <summary>
    /// Initialize this CampaignStage
    /// </summary>
    /// <param name="parent"></param>
    public void Initialize(CampaignGroup parent)
    {
        parentGroup = parent;
    }
    
    /// <summary>
    ///  Called by the parent CampaignGroup to Start the Stage
    /// </summary>
    public void BeginCurrStage()
    {
        if (!parentGroup)
            Debug.LogError("No Parent Group!");
        currentWaveIndex = 0;
        StartCoroutine(childWaves[currentWaveIndex].BeginCurrWave());
        //childWaves[currentWaveIndex].BeginCurrWave();
        Debug.Log("BEGINNING STAGE " + name);
    }
    /// <summary>
    ///  Called by the parent CampaignGroup to Start the Stage
    /// </summary>
    public void NotifyWaveCompleted(StageWave completedStage)
    {
        beginNextWave = true;

        currentWaveIndex++;
        if (currentWaveIndex >= numOfWaves)
        {
            parentGroup.NotifyStageCompleted(this);
            Debug.Log("PLAYER BEAT STAGE " + name);
            return;
        }
        //childWaves[currentWaveIndex].BeginCurrWave();
        StartCoroutine(childWaves[currentWaveIndex].BeginCurrWave());
    }
}
