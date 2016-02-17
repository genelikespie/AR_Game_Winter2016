using UnityEngine;
using System.Collections;

public class GunnerTurret : BaseTurret
{
  public GameObject muzzleFire;
  public GameObject projectile;
  public float turnSpeed = 5f;
  public float firePauseTime = 0.01f;
  public float errorAmount = 0.001f;
  public Transform target;
  public Transform[] muzzlePositions;
  public Transform game_tilt;
  public Transform game_pan;
  public Transform aim_tilt;
  public Transform aim_pan;
  public bool fireOn = false;
  SpriteAnimator muzzleFireAnimation;


  protected float nextFireTime;
  protected float nextMoveTime;
  // private bool aimAhead = false;

  protected Quaternion aim_pan_start;
  protected Quaternion aim_tilt_start;

  // Use this for initialization
  protected virtual void Awake ()
  {
    aim_pan_start = aim_pan.rotation;
    aim_tilt_start = aim_tilt.localRotation;

  }


  new void Start()
  {
        base.Start();
      muzzleFireAnimation = (Instantiate(muzzleFire, transform.position, transform.rotation) as GameObject).GetComponentInChildren<SpriteAnimator>();
      if (!muzzleFireAnimation)
          Debug.LogError("No muzzle animation!");

  }
	
  // Update is called once per frame
  new void Update ()
  {
    if (target) {
      if (Time.time >= nextMoveTime) {

        aim_pan.LookAt (target);        
        Vector3 newRot = aim_pan.eulerAngles;
        newRot.x = newRot.z = 0;
        aim_pan.rotation = Quaternion.Euler (newRot);
        
        aim_tilt.LookAt (target);
        Vector3 rot = aim_tilt.localEulerAngles;
        rot.y = rot.z = 0;
        aim_tilt.localRotation = Quaternion.Euler (rot);

        game_pan.rotation = Quaternion.Lerp (game_pan.rotation, aim_pan.rotation, Time.deltaTime * turnSpeed);
        game_tilt.localRotation = Quaternion.Lerp (game_tilt.localRotation, aim_tilt.localRotation, Time.deltaTime * turnSpeed);

      }

      if (Time.time >= nextFireTime  && Input.GetMouseButtonDown(0) && FireYes == true) {
                FireBullet(dummyV, dummyQ);
      }

    } else {
      game_pan.rotation = Quaternion.Lerp (game_pan.rotation, aim_pan_start, Time.deltaTime * turnSpeed);
      game_tilt.localRotation = Quaternion.Lerp (game_tilt.localRotation, aim_tilt_start, Time.deltaTime * turnSpeed);
    }
  }

  new void FireBullet(Vector3 direction, Quaternion rotation)
    {
        if (Time.time >= nextFireTime)
        {

            nextFireTime = Time.time + reloadTime;
            nextMoveTime = Time.time + firePauseTime;

            foreach (Transform m in muzzlePositions)
            {
                muzzleFireAnimation.play = true;
                // Instantiate (projectile, m.position, m.rotation);
                base.FireBullet(m.position, m.rotation);
            }
        }

    }



}
