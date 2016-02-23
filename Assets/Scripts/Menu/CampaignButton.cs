using UnityEngine;
using System.Collections;

public class CampaignButton : MonoBehaviour {

    public CampaignGroup campaign;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void StartCampaign()
    {
        campaign.StartCampaign();
        gameObject.SetActive(false);
    }
}
