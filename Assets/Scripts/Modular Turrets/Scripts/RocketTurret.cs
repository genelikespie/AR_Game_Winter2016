using UnityEngine;
using System.Collections;

public class RocketTurret : BaseTurret
{
  public Transform target;
  public Transform[] muzzlePositions;
  public Transform game_tilt;
  public Transform game_pivot;
  public Transform aim_tilt;
  public Transform aim_pivot;
  public string targetTag = "Enemy";
  
    // Update is called once per frame
    void Update ()
  {
  
    if (target) {
      aim_pivot.LookAt (target);
      Vector3 newRot = aim_pivot.eulerAngles;
      newRot.x = newRot.z = 0;
      aim_pivot.rotation = Quaternion.Euler (newRot);
      
      aim_tilt.LookAt (target);
      Vector3 rot = aim_tilt.localEulerAngles;
      rot.y = rot.z = 0;
      aim_tilt.localRotation = Quaternion.Euler (rot);
      
      game_pivot.rotation = Quaternion.Lerp (game_pivot.rotation, aim_pivot.rotation, Time.deltaTime * turnSpeed);
      game_tilt.localRotation = Quaternion.Lerp (game_tilt.localRotation, aim_tilt.localRotation, Time.deltaTime * turnSpeed);
    }
	
  }
  
  void OnTriggerEnter (Collider other)
  {
    if (other.gameObject.tag == targetTag) {
      nextFireTime = Time.time + (reloadTime * 0.5f);
      target = other.gameObject.transform;
    }
  }
  
  void OnTriggerExit (Collider other)
  {
    if (other.gameObject.transform == target) {
      target = null;
    }
  }

    public override void FireBullet(Vector3 direction, Quaternion rotation)
    {
        if (Time.time >= nextFireTime)
        {
            nextFireTime = Time.time + reloadTime;
            int m = Random.Range(0, muzzlePositions.Length);
            base.FireBullet(muzzlePositions[m].position, muzzlePositions[m].localRotation);
           // GameObject missile = (GameObject)Instantiate(projectile, muzzlePositions[m].position, muzzlePositions[m].localRotation);
            //(missile.GetComponent("Missile") as Missile).target = target;
        }
    }
}
