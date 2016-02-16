using UnityEngine;
using System.Collections;

public class SpaceShip : MonoBehaviour {
    public float maxHitPoints;
    public float baseDamage;
    public GameObject explosionAnimation;

    private float hitPoints;
    Collider collider;

	// Use this for initialization
	protected void Start () {
        collider = GetComponent<Collider>();
        if (!collider)
            Debug.LogError("No collider found!");

        hitPoints = maxHitPoints;
	}
	
	// Update is called once per frame
	protected void Update () {
	}

    /**
     * Method to apply damage to our SpaceShip
     */
    public virtual void Hit(float damage)
    {
        Debug.Log("Space ship " + this.name + " was hit for: " + damage + " dmg");
        hitPoints -= damage;
        if (hitPoints <= 0)
        {
            Explode();
        }
    }


    protected virtual void Explode()
    {
        Debug.Log(this.name + " Blew up!");
        // Play animation
        if (explosionAnimation)
        {
            GameObject explosion = Instantiate(explosionAnimation, transform.position, transform.rotation) as GameObject;
            explosion.GetComponentInChildren<SpriteAnimator>().play = true;
        }
        else
            Debug.LogError("There's no explosion prefab referenced!");
        // Kill object
        Destroy(this.gameObject);
    }
}
