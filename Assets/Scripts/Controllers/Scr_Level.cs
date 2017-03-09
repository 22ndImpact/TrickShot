using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_Level : MonoBehaviour
{
    //The level's anchor from which the balls are slung
    public Transform Anchor;
    //The time after shooting an orb before the next one spawns
    public float reloadTime;
    float reloadTimer = 0;
    bool reloading = false;

    void Awake()
    {
        //Scans the objects within the level so they can be referenced later
        FindObjects();
    }

    void Start()
    {
        Scr_Mgr_Game.inst.Manager_GamePlay.currentLevel = this;

        SpawnOrb();
    }

    void Update()
    {
        if(reloading)
        {
            UpdateReloadTimer();
        }
        
    }

    void UpdateReloadTimer()
    {
        if(reloadTimer < reloadTime)
        {
            reloadTimer += Time.deltaTime;
        }
        else
        {
            SpawnOrb();
            reloading = false;
        }
    }

    public void Reload()
    {
        reloadTimer = 0;
        reloading = true;
    }

    public void SpawnOrb()
    {
        //Creating a new orb from the GameResources object
        GameObject newOrb = Instantiate<GameObject>(Scr_Mgr_Game.inst.Manager_GamePlay.GameResources.Orb);
    }

    void FindObjects()
    {
        FindAnchor();
    }

    void FindAnchor()
    {
        Anchor = GameObject.Find("Anchor").transform;
    }
}
