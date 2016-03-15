using UnityEngine;
using System.Collections;

public class AU_Missile : MonoBehaviour {

    public MissileTurret missileTurret;
    public bool barrageOnEnter = false;
    void Awake()
    {
        if (barrageOnEnter)
            UnityEngine.Assertions.Assert.IsTrue(missileTurret);
    }
    void OnTriggerEnter(Collider c)
    {
        if (c.tag == "Player" && barrageOnEnter)
        {
            missileTurret.Barrage();
        }
    }
}
