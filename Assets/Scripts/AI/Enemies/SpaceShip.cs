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
    public GameObject myCrosshair;
    public Vector3 startSpawnOffset;

    protected float hitPoints;
    protected SpaceShipState shipState;
    protected StageWave parentWave;
    protected SeekingAI ai;
    protected ScaleUnitWrapper scaleUnitWrapper;

    Collider myCollider;
    Vector3 spawnLocation; // location where we end up after spawning
    Vector3 startSpawnLocation; // location where we will start to spawn
    bool drop = false;
    float currTime = 0;
    public float timeToDrop = 2;
	// Use this for initialization
	protected void Awake () {
        myCollider = GetComponent<Collider>();
        ai = GetComponent<SeekingAI>();
        scaleUnitWrapper = GetComponent<ScaleUnitWrapper>();
        Assert.IsTrue(myCollider && myCrosshair && scaleUnitWrapper);
        myCrosshair.transform.localPosition = new Vector3(0, myCrosshair.transform.localPosition.y, 0);
        hitPoints = maxHitPoints;
        shipState = SpaceShipState.Inactive;
	}

    protected void Start()
    {
    }
	// Update is called once per frame
	protected void Update () {
        if (drop)
        {
            currTime += Time.deltaTime;
            float ratio = currTime / timeToDrop;
            if (ratio >= 1)
            {
                transform.position = spawnLocation;
                drop = false;
                return;
            }
            transform.position = Vector3.Lerp(startSpawnLocation, spawnLocation, ratio);
        }
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
        startSpawnLocation = spawnLocation + startSpawnOffset;
        transform.position = startSpawnLocation;
        scaleUnitWrapper.Scale(false);
        gameObject.SetActive(true);
        if (ai)
        {
            ai.enabled = false;
        }
    }
    // When the ScaleUnitWrapper finishes scaling it will call this function
    public void FinishedScalingUp()
    {
        if (ai)
        {
            ai.enabled = true;
        }
        currTime = 0;
        drop = true;
        
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
