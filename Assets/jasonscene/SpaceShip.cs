using UnityEngine;
using System.Collections;

public class SpaceShip : MonoBehaviour {
    public float hitPoints;
    public float baseDamage;
    public GameObject explosionAnimation;
    public GameObject crossHair;

    SeekingAI seekingAI;
    Collider collider;

	// Use this for initialization
	void Start () {
        seekingAI = GetComponent<SeekingAI>();
        //if (!seekingAI)
          //  Debug.LogError("No AI Found!");
        collider = GetComponent<Collider>();
        if (!collider)
            Debug.LogError("No collider found!");

        crossHair = GameObject.Find("RedCross");
        GameObject myCrossHair = Instantiate(crossHair, new Vector3(transform.position.x, 1, transform.position.z), transform.rotation) as GameObject;
        myCrossHair.transform.Rotate(new Vector3(90, 0, 0));
        if (myCrossHair)
            myCrossHair.transform.SetParent(this.transform);
        else
            Debug.LogError("No crosshair found!");
	}
	
	// Update is called once per frame
	void Update () {
	}

    public void Hit(float damage)
    {
        hitPoints -= damage;
        if (hitPoints <= 0)
        {
            Explode();
        }
    }

    void Explode()
    {
        Debug.Log(this.name + " Blew up!");
        // Play animation
        if (explosionAnimation)
        {
            GameObject explosion = Instantiate(explosionAnimation, transform.position, transform.rotation) as GameObject;
            explosion.GetComponentInChildren<SpriteAnimator>().play = true;
            //explosion.GetComponentInChildren<Animator>().Play();
        }
        else
            Debug.LogError("There's no explosion prefab referenced!");
        // Kill object
        Destroy(this.gameObject);
    }
}
