using UnityEngine;
using System.Collections;

public enum SpaceShipState
{
    Alive, Inactive, Dead
}
public class SpaceShip : MonoBehaviour {
    public float maxHitPoints;
    public float baseDamage;
    public GameObject explosionAnimation;

    protected float hitPoints;
    protected SpaceShipState shipState; 
    Collider collider;

	// Use this for initialization
	protected void Awake () {
        collider = GetComponent<Collider>();
        if (!collider)
            Debug.LogError("No collider found!");

        hitPoints = maxHitPoints;
        shipState = SpaceShipState.Inactive;
	}

    protected void Start()
    {
    }
	// Update is called once per frame
	protected void Update () {
	}

    public virtual void Hit(float damage)
    {
        Debug.Log("Space ship " + this.name + " was hit for: " + damage + " dmg");
        hitPoints -= damage;
        if (hitPoints <= 0)
        {
            Explode();
        }
    }

    public void SetAlive()
    {
        shipState = SpaceShipState.Alive;
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
        shipState = SpaceShipState.Dead;
        //Destroy(this.gameObject);
    }
}
