﻿using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

public class Headquarter : MonoBehaviour {

    public float HitPoints;
    public float Armor;
    public RotateReticule healthReticule;
    public RotateReticule armorReticule;


    private GameManagerScript gameManager;
    private MenuManager menuManager;

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
        Assert.IsTrue(menuManager && gameManager);
        Assert.IsTrue(healthReticule && armorReticule);

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
        //Debug.Log("WAAAAAAAAAAAAGH" + b);
        menuManager.hCollider = this.GetComponent<Collider>();
        menuManager.boundSize = b.size;
        menuManager.boundExtent = b.extents;
        menuManager.center = b.center;
    }
}
