using UnityEngine;
using System.Collections;

public class RocketTurret : MonoBehaviour
{

  public GameObject projectile;
  public float reloadTime = 1f;
  public float turnSpeed = 5f;
  public float firePauseSpeed = .25f;
  public float errorAmount = .001f;
  public Transform target;
  public Transform[] muzzlePositions;
  public Transform game_tilt;
  public Transform game_pivot;
  public Transform aim_tilt;
  public Transform aim_pivot;
  
  private float nextFireTime;
  // Use this for initialization
  void Start ()
  {
	
  }
	
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
      
      if (Time.time >= nextFireTime) {
        FireProjectile ();
      }
    }
	
  }
  
  void OnTriggerEnter (Collider other)
  {
    if (other.gameObject.tag == "Enemy") {
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
  
  void FireProjectile ()
  {
    nextFireTime = Time.time + reloadTime;
    int m = Random.Range (0, muzzlePositions.Length);
    GameObject missile = (GameObject)Instantiate (projectile, muzzlePositions [m].position, muzzlePositions [m].localRotation);
    (missile.GetComponent ("Missile") as Missile).target = target;
  }
}
