using UnityEngine;
using System.Collections;

public class CampaignPlatform : MonoBehaviour {

    public CampaignGroup campaign;
    public bool canStartCampaign;
    private FloatingPlatform platform;
    public AudioSource buttonPress;
    void Awake()
    {
        platform = GetComponent<FloatingPlatform>();
        if (!platform)
            Debug.LogError("cannot find platform for campaignplatform!");
    }

    public void StartCampaign()
    {
        buttonPress.Play();
        campaign.StartCampaign();
        platform.Lift();
        platform.canMove = false;
        //gameObject.SetActive(false);
    }
}
