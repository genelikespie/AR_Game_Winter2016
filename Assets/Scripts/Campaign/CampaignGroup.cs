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

    bool start = true;

    void Awake()
    {
        childStages = new List<CampaignStage>();
        IEnumerable stages = transform.GetComponentsInChildren<CampaignStage>();
        foreach (CampaignStage s in stages)
        {
            childStages.Add(s);
            s.Initialize(this);
        }

        // Check that stages are in correct order
        for (int i = 0; i < childStages.Count; i++)
        {
            Debug.Log(childStages[i].name);
        }
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    if(start)
        {
            start = false;
            childStages[0].BeginCurrStage();
        }
	}
}
