 using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

public class Headquarter : MonoBehaviour {

    public float maxHitPoints;
    public float maxArmor;

    private float HitPoints;
    private float Armor;

    private RotateReticule healthReticule;
    private RotateReticule armorReticule;
    private RotateReticule loadingMenuReticule;

    private GameManagerScript gameManager;
    private MenuManager menuManager;
    private moveCrosshair crosshair;

    // The time it takes for the crosshair to rest on the HQ
    public float timeToLoadMenu;
    private float currTime;
    private bool crosshairOnHeadquarters = true; // initialize to true, and our update will set to false
    private Collider hCollider;
    private Vector3 boundSize;
    private Vector3 boundExtent;
    private Vector3 center;

    private static Headquarter instance;
    private static Object instance_lock = new Object();
    private string name;
    public static Headquarter Instance()
    {
        if (instance != null)
            return instance;
        lock (instance_lock)
        {
            instance = (Headquarter)FindObjectOfType(typeof(Headquarter));
            if (FindObjectsOfType(typeof(Headquarter)).Length > 1)
            {
                Debug.LogError("There can only be one instance!");
                return instance;
            }
            if (instance != null)
                return instance;
            Debug.LogError("Could not find a instance!");
            return null;
        }
    }
    void Awake()
    {
        menuManager = MenuManager.Instance();
        gameManager = GameManagerScript.Instance();
        crosshair = moveCrosshair.Instance();
        Canvas childCanvas = GetComponentInChildren<Canvas>();
        loadingMenuReticule = HelperMethods.FindChildWithName(childCanvas.gameObject, "RotatingReticule").GetComponent<RotateReticule>();
        healthReticule = HelperMethods.FindChildWithName(childCanvas.gameObject, "HealthReticule").GetComponent<RotateReticule>();
        armorReticule = HelperMethods.FindChildWithName(childCanvas.gameObject, "ArmorReticule").GetComponent<RotateReticule>();

        Assert.IsTrue(menuManager && gameManager && crosshair);
        Assert.IsTrue(healthReticule && armorReticule && loadingMenuReticule);

        loadingMenuReticule.SetTimer(timeToLoadMenu);
        currTime = timeToLoadMenu;
        Armor = maxArmor;
        HitPoints = maxHitPoints;

    }
    void Update()
    {
        if (gameObject.activeSelf &&
        !gameManager.paused &&
        (crosshair.transform.position.x < (center.x + boundExtent.x)) &&
        (crosshair.transform.position.x > (center.x - boundExtent.x)) &&
        (crosshair.transform.position.z < (center.z + boundExtent.z)) &&
        (crosshair.transform.position.z > (center.z - boundExtent.z)))
        {
            if (!crosshairOnHeadquarters)
            {
                loadingMenuReticule.isActive = true;
                loadingMenuReticule.ShowImage();
            }
            if (loadingMenuReticule.currReticuleValue <= 0)
            {
                menuManager.PauseGame();
                loadingMenuReticule.ResetTimer();
            }
        }
        else
        {
            crosshairOnHeadquarters = false;
            loadingMenuReticule.ResetTimer();
            loadingMenuReticule.HideImage();
        }
    }
    public void Hit(float damage)
    {
        // apply damage to armor
        if (Armor > 0)
        {
            float remainder = Armor - damage;
            Armor -= damage;
            // if armor is depleted, apply remaining damage to HP
            if (Armor <= 0) {
                Armor = 0;
                HitPoints += remainder;
            }
        }
        else
            HitPoints -= damage;

        // if HP is gone, player loses
        if (HitPoints <= 0)
        {
            gameManager.PlayerLost();
        }

        healthReticule.ChangeValue(HitPoints);
        armorReticule.ChangeValue(Armor);
    }

    void OnEnable()
    {
        Bounds b = this.GetComponent<Collider>().bounds;
        hCollider = this.GetComponent<Collider>();
        boundSize = b.size;
        boundExtent = b.extents;
        center = b.center;
    }
}
