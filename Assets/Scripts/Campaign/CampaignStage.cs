using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CampaignStage : MonoBehaviour {


    // List that stores all child campaign stages
    List<StageWave> childWaves;

    private StageWave currentWave;
    private CampaignGroup parentGroup;

    void Awake()
    {
        childWaves = new List<StageWave>();
        IEnumerable waves = transform.GetComponentsInChildren<StageWave>();
        foreach (StageWave w in waves)
        {
            childWaves.Add(w);
        }

        // Check that stages are in correct order
        for (int i = 0; i < childWaves.Count; i++)
        {
            Debug.Log(childWaves[i].name);
        }

    }

    /// <summary>
    /// Initialize this CampaignStage
    /// </summary>
    /// <param name="parent"></param>
    public void Initialize(CampaignGroup parent)
    {
        parentGroup = parent;
    }
    
    // Called by the parent CampaignGroup
    public void BeginCurrStage()
    {
        if (!parentGroup)
            Debug.LogError("No Parent Group!");
        BeginChildWaves();
    }

    IEnumerator BeginChildWaves()
    {
        for (int i = 0; i < childWaves.Count; i++)
        {
            yield return new WaitForSeconds(childWaves[i].delayBeforeCurrStage);
            childWaves[i].BeginCurrWave();
            yield return new WaitForSeconds(childWaves[i].delayBeforeNextStage);
        }
    }

}
