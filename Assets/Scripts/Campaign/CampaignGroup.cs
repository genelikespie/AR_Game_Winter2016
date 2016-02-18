using UnityEngine;
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

    private CampaignStage currentStage;

    private bool start = true;
    private int numOfStages;
    private int currentStageIndex;
    void Awake()
    {
        childStages = new List<CampaignStage>();
        IEnumerable stages = transform.GetComponentsInChildren<CampaignStage>();
        foreach (CampaignStage s in stages)
        {
            childStages.Add(s);
            s.Initialize(this);
        }
        numOfStages = childStages.Count;
        currentStageIndex = 0;

    }

    public void StartCampaign()
    {
        currentStageIndex = 0;
        childStages[currentStageIndex].BeginCurrStage();
    }

    public void NotifyStageCompleted(CampaignStage stage)
    {

    }


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	}
}
