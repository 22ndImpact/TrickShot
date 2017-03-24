using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_Level : MonoBehaviour
{
    public Scr_LevelUI LevelUI;

    //The unique ID of the level used to load and unload levels
    public string levelID;
    //The uniwue ID of the level this one will continue on to
    public string nextLevelID;
    //A refernce to the gameplay controller
    public Scr_GameplayController GameplayController;
    //The prefab reference used to spawn orbs
    public GameObject Orb_Prefab;
    //The level's anchor from which the balls are slung
    public Transform Anchor;
    //The time after shooting an orb before the next one spawns
    public float reloadTime;
    //The time it takes the orb to respawn after being fired
    float reloadTimer = 0;
    //A state tracker to see if the ball is reloadign or not
    bool reloading = false;
    //The totla number of entities in the level
    public int NumberOfEntities;


    //The orb requirement to progress to the next level
    public int SufficientScore;
    //The orb requirement to achieve full completion of the level
    public int ExceptionalScore;
    //Current number of orbs fired in the level
    public int OrbsFired;

    void Awake()
    {
        //Scans the objects within the level so they can be referenced later
        FindObjects();

        //Tracks the current level in the gameplay controller;
        GameplayController.CurrentLevel = this;
    }

    void Start()
    {
        //Creates an initial orb to start the level
        SpawnOrb();
        //Forces the UI to refresh
        UpdateOrbsFired(0);
    }

    void Update()
    {
        #region DebugInput
        if(Input.GetKeyDown(KeyCode.Backspace))
        {
            GameplayController.EndLevel();
        }
        #endregion

        #region Reloading
        if(reloading)
        {
            UpdateReloadTimer();
        }
        #endregion
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
        GameObject newOrb = Instantiate<GameObject>(Orb_Prefab);
        //Initialize the orb, giving it information about the level geometry
        newOrb.GetComponent<Scr_Orb>().Initialize(Anchor, transform);
    }

    void FindObjects()
    {
        FindAnchor();
        FindGameplayController();
        FindLevelEntities();

        LevelUI = FindObjectOfType<Scr_LevelUI>();
    }

    void FindAnchor()
    {
        Anchor = GameObject.Find("Anchor").transform;
    }

    void FindGameplayController()
    {
        GameplayController = GameObject.Find("Gameplay Controller").GetComponent<Scr_GameplayController>();
    }

    void FindLevelEntities()
    {
        NumberOfEntities = FindObjectsOfType<Scr_LevelEntity>().Length;
    }

    public void UpdateEntities(int _Adjustment)
    {
        NumberOfEntities += _Adjustment;
        if (NumberOfEntities <= 0)
        {
            CompleteLevel();
        }
        
    }

    void CompleteLevel()
    {
        GameplayController.EndLevel();
    }

    public void UpdateOrbsFired(int _Adjustment)
    {
        OrbsFired += _Adjustment;

        if (LevelUI != null)
        {
            LevelUI.UpdateOrbDisplay(OrbsFired, SufficientScore, ExceptionalScore);
        }

    }
}
