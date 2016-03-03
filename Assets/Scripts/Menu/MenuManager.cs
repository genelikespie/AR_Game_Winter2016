using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

public class MenuManager : MonoBehaviour {

    private List<FloatingPlatform> platformsList;

    // Message Board
    public Transform messageBoard;

    // references to any pause buttons we might have
    private PauseButton pauseButton;
    private GameManagerScript gameManager;
    public float acceleration = 5f;

    // For finding if the crosshair is over the headquarters
    private RotateReticule loadingMenuReticule;
    private GameObject crosshair;
    private GameObject headquarters;

    // The time it takes for the crosshair to rest on the HQ
    public float timeToLoadMenu;
    private float currTime;
    private bool crosshairOnHeadquarters = true; // initialize to true, and our update will set to false
    public Collider hCollider;
    public Vector3 boundSize;
    public Vector3 boundExtent;
    public Vector3 center;

    private static MenuManager instance;
    private static Object instance_lock = new Object();

    public static MenuManager Instance()
    {
        if (instance != null)
            return instance;
        lock (instance_lock)
        {
            instance = (MenuManager)FindObjectOfType(typeof(MenuManager));
            if (FindObjectsOfType(typeof(MenuManager)).Length > 1)
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
        platformsList = new List<FloatingPlatform>();
        IEnumerable childPlatforms = GetComponentsInChildren<FloatingPlatform>();

        foreach (FloatingPlatform platform in childPlatforms) {
            platformsList.Add(platform);
            platform.acceleration = acceleration;
        }

        pauseButton = GameObject.FindObjectOfType<PauseButton>();
        gameManager = GameManagerScript.Instance();
        crosshair = moveCrosshair.Instance().gameObject;
        headquarters = Headquarter.Instance().gameObject;
        loadingMenuReticule = GetComponentInChildren<RotateReticule>();
        Assert.IsTrue(gameManager && crosshair && headquarters && loadingMenuReticule);

        loadingMenuReticule.SetTimer(timeToLoadMenu);
        currTime = timeToLoadMenu;
    }

    void Update()
    {
        if (headquarters.activeSelf &&
        !gameManager.paused &&
        (crosshair.transform.position.x < (center.x + boundExtent.x)) &&
        (crosshair.transform.position.x > (center.x - boundExtent.x)) &&
        (crosshair.transform.position.z < (center.z + boundExtent.z)) &&
        (crosshair.transform.position.z > (center.z - boundExtent.z)))
        {
            if (!crosshairOnHeadquarters) {
                loadingMenuReticule.isActive = true;
                loadingMenuReticule.ShowImage();
            }
            if (loadingMenuReticule.currReticuleValue <= 0)
            {
                gameManager.pauseGame();
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

    public void DropPlatforms()
    {
        foreach (FloatingPlatform p in platformsList)
        {
            if (p.canMove)
                p.Drop();
        }
        if (pauseButton)
            pauseButton.GetComponent<Text>().text = "Unpause";
    }
    public void LiftPlatforms()
    {
        foreach (FloatingPlatform p in platformsList)
        {
            if (p.canMove)
                p.Lift();
        }
    }

}
