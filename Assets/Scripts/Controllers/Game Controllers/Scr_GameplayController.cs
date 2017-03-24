using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_GameplayController : MonoBehaviour
{
    public Scr_Level CurrentLevel;

    public bool DebugMode;

    public Scr_SceneController SceneController = new Scr_SceneController();

	// Use this for initialization
	void Start ()
    {
        if(!DebugMode)
        {
            LoadLevelSelect();
        }
	}

    public void LoadLevel(string _LevelID)
    {
        //If there is currently a level, unload it
        if(CurrentLevel != null)
        {
            UnloadLevel(CurrentLevel.levelID);
        }

        //Loads the levelUI
        LoadLevelUI();

        //Load the level scene
        SceneController.AddScene(_LevelID);

        
    }

    public void UnloadLevel(string _LevelID)
    {
        //Unloads the level
        SceneController.RemoveScene(_LevelID);
    }

    public void LoadLevelSelect()
    {
        //If there is a level, Unload it
        if (CurrentLevel != null)
        {
            UnloadLevel(CurrentLevel.levelID);
        }

        //Load the level select Scene
        SceneController.AddScene("LevelSelect");
    }
    public void UnloadLevelSelect()
    {
        //Load the level select Scene
        SceneController.RemoveScene("LevelSelect");
    }

    public void LoadLevelUI()
    {
        SceneController.AddScene("LevelUI");
    }
    public void UnloadLevelUI()
    {
        Debug.Log("Unload Level UI");
        SceneController.RemoveScene("LevelUI");
    }

    public void LoadLevelComplete()
    {
        SceneController.AddScene("LevelComplete");
    }
    public void UnloadLevelComplete()
    {
        SceneController.RemoveScene("LevelComplete");
    }

    public void LoadNextLevel()
    {
        UnloadLevelComplete();
        LoadLevel(CurrentLevel.nextLevelID);
    }

    public void ReloadLevel()
    {
        UnloadLevelComplete();
        LoadLevel(CurrentLevel.levelID);
    }

    public void EndLevel()
    {
        LoadLevelComplete();
        UnloadLevelUI();
    }
}
