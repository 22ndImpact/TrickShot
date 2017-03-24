using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_LevelSelect : MonoBehaviour
{
    Scr_GameplayController GameplayController;

    void Awake()
    {
        FindGameplayController();
    }

    void FindGameplayController()
    {
        GameplayController = GameObject.Find("Gameplay Controller").GetComponent<Scr_GameplayController>();
    }

    public void LoadLevel(string _LevelID)
    {
        //Unloads the level select
        GameplayController.SceneController.RemoveScene("LevelSelect");
        GameplayController.LoadLevel(_LevelID);
    }
}
