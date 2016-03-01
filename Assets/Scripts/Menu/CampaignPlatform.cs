using UnityEngine;
using System.Collections;

public class CampaignPlatform : MonoBehaviour {

    public CampaignGroup campaign;
    private FloatingPlatform platform;

    void Awake()
    {
        platform = GetComponent<FloatingPlatform>();
        if (!platform)
            Debug.LogError("cannot find platform for campaignplatform!");
    }

    public void StartCampaign()
    {
        campaign.StartCampaign();
        platform.Lift();
        platform.canMove = false;
        //gameObject.SetActive(false);
    }
}
