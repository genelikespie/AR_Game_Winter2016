using UnityEngine;
using System.Collections;

public class GattlingTurret : GunTurret
{
  public Renderer[] muzzlesFires;
  void Start ()
  {
	
  }
  
  override protected void Awake ()
  {
    base.Awake ();
    foreach (Renderer r in muzzlesFires) {
      r.enabled = false;
    }
  }
  /*
  override protected void OnTriggerExit (Collider t)
  {
    base.OnTriggerExit (t);
    if (target == null) {
      foreach (Renderer r in muzzlesFires)
        r.enabled = false;
    }
  }
  */
  override protected void Update ()
  {
    base.Update ();
    if (target == null) {
      foreach (Renderer r in muzzlesFires) {
        r.enabled = false;
      }
    }
  }
  // Update is called once per frame
  
  override protected void FireProjectile ()
  {
    foreach (Renderer r in muzzlesFires) {
      r.enabled = true;
    }
    nextFireTime = Time.time + reloadTime;
    nextMoveTime = Time.time + firePauseTime;
    
    foreach (Transform m in muzzlePositions) {
      Instantiate (projectile, m.position, m.rotation);
    } 
 
  }
}
