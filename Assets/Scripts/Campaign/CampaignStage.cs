using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CampaignStage : MonoBehaviour {


    // List that stores all child campaign stages
    List<StageWave> childWaves;

    private int numOfWaves;
    private int currentWaveIndex;
    //private StageWave currentWave;
    private CampaignGroup parentGroup;

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
        BeginChildWaves(childWaves[currentWaveIndex]);
    }
    /// <summary>
    ///  Called by the parent CampaignGroup to Start the Stage
    /// </summary>
    public void NotifyWaveCompleted(StageWave completedStage)
    {
        currentWaveIndex++;
        if (currentWaveIndex >= numOfWaves)
        {
            parentGroup.NotifyStageCompleted(this);
            Debug.Log("PLAYER BEAT STAGE " + name);
        }
        BeginChildWaves(childWaves[currentWaveIndex]);
    }

    IEnumerator BeginChildWaves(StageWave runWave)
    {
        yield return new WaitForSeconds(runWave.delayBeforeCurrStage);
        runWave.BeginCurrWave();
        yield return new WaitForSeconds(runWave.delayBeforeNextStage);
    }

}
