using UnityEngine;
using System.Collections;

public class Missile : MonoBehaviour
{
  public GameObject explosion;
  public Transform target;
  float range = 50;
  float speed = 40;
  
  private float dist;
  // Use this for initialization
  void Start ()
  {
	
  }
	
  // Update is called once per frame
  void Update ()
  {
  
    if (target) {
      transform.Translate (Vector3.forward * Time.deltaTime * speed);
      dist += Time.deltaTime * speed;
      if (dist >= range) {
        Destroy (gameObject);
      }
    }
    
      
  
    if (target)
      transform.LookAt (target);
    else
      Destroy (gameObject);
      
   
	
  }
  
  void OnTriggerEnter (Collider collider)
  {
    if (collider.gameObject.tag == "Enemy") {
      Instantiate (explosion, target.transform.position, target.transform.rotation);
      target = null;
      Destroy (gameObject);
    }
    
  }
}
