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
    //public bool onDeathNotify = true;


    protected float hitPoints;
    protected SpaceShipState shipState;
    protected StageWave parentWave;

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
        gameObject.SetActive(true);
    }

    /// <summary>
    /// Set this object as inactive for future use
    /// </summary>
    public void SetInactive()
    {
        shipState = SpaceShipState.Inactive;
        gameObject.SetActive(false);
    }
    // Notify the parent wave when this spaceship dies
    public void SetOnDeathNotify(StageWave parent)
    {
        parentWave = parent;
    }

    protected virtual void Explode()
    {
        if (shipState == SpaceShipState.Dead)
            Debug.LogError("Spaceship was already dead!");
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
        if (parentWave)
            parentWave.NotifySpaceShipDied(this);
        SetInactive();
        //Destroy(this.gameObject);
    }
}
