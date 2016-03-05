using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;

public enum SpaceShipState
{
    Alive, Inactive, Dead
}
public class SpaceShip : MonoBehaviour {
    public float maxHitPoints;
    public float baseDamage;
    public GameObject explosionAnimation;
    public Vector3 startSpawnOffset;

    protected float hitPoints;
    protected SpaceShipState shipState;
    protected StageWave parentWave;

    Collider myCollider;
    Vector3 spawnLocation;
    Vector3 startSpawnLocation;
    Vector3 startScale;
	// Use this for initialization
	protected void Awake () {
        myCollider = GetComponent<Collider>();

        Assert.IsTrue(myCollider);

        hitPoints = maxHitPoints;
        shipState = SpaceShipState.Inactive;
        startScale = transform.localScale;
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

    public void Spawn(Vector3 spawnLoc)
    {
        shipState = SpaceShipState.Alive;
        spawnLocation = spawnLoc;
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
        if (shipState == SpaceShipState.Dead || shipState == SpaceShipState.Inactive)
        {
            Debug.LogError("Spaceship was already dead!");
            return;
        }
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
