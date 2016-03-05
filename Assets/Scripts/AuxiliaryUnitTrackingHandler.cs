using UnityEngine;
using UnityEngine.Assertions;
using Vuforia;
using System.Collections;

public class AuxiliaryUnitTrackingHandler : DefaultTrackableEventHandler {
    WayPointFlare wayPoint;

    void Awake()
    {
        wayPoint = GetComponentInChildren<WayPointFlare>();
        Assert.IsNotNull<WayPointFlare>(wayPoint);
    }
    protected override void OnTrackingFound()
    {
        Debug.Log("tracking");
        wayPoint.enabled = true;
        base.OnTrackingFound();
    }
    protected override void OnTrackingLost()
    {
        Debug.Log("lost");
        wayPoint.enabled = false;
        base.OnTrackingLost();
    }

}
