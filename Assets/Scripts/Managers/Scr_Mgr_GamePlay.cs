using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_Mgr_GamePlay : MonoBehaviour
{
    //The current loaded level
    public Scr_Level currentLevel;

    //The gameResources object is used to store prefab references used throughout the game
    [HideInInspector]
    public Scr_GameResources GameResources;

    void Awake()
    {
        //Setting up a link between this and the main game manager
        Scr_Mgr_Game.inst.Manager_GamePlay = this;
        //Linking the gameResources object
        GameResources = GetComponent<Scr_GameResources>();
    }

    public void LoadLevel(Scr_Level _level)
    {
        if(currentLevel != null)
        {
            currentLevel.gameObject.SetActive(false);
        }
    }
}
