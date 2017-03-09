using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_Button_ChangeScene : MonoBehaviour
{
    #region Variables
    //The target scene of the transition
    public Scr_Mgr_Scene.Scenes targetScene;

    public SceneButtonType sceneButtonType;
    public enum SceneButtonType
    {
        ChangeScene,
        AddScene,
        RemoveScene,
        SceneButtonType_Count
    }
    #endregion

    #region Functions
    /// <summary>
    /// Activate the scene change button and activates the appropriate function changing function
    /// </summary>
    public void ActivateButton()
    {
        switch(sceneButtonType)
        {
            case SceneButtonType.ChangeScene:
                ChangeScene();
                break;
            case SceneButtonType.AddScene:
                AddScene();
                break;
            case SceneButtonType.RemoveScene:
                RemoveScene();
                break;
        }
    }

    /// <summary>
    /// Uses the global GameDirector to load a full new scene while unloading all other scene
    /// </summary>
    ///
    public void ChangeScene()
    {
        //Unloads all other scenes before loading the new one
        Scr_Mgr_Game.inst.Manager_Scene.UnloadAllScenes();

        //Loads the new scene
        Scr_Mgr_Game.inst.Manager_Scene.LoadAdditiveScene(targetScene.ToString());
    }

    /// <summary>
    /// Adds a scene on top of other scenes currently loaded
    /// </summary>
    public void AddScene()
    {
        //Loads the new scene
        Scr_Mgr_Game.inst.Manager_Scene.LoadAdditiveScene(targetScene.ToString());
    }

    /// <summary>
    /// Removes a single targetd scene currently loaded
    /// </summary>
    public void RemoveScene()
    {
        //Unloads a scene
        Scr_Mgr_Game.inst.Manager_Scene.UnloadScene(targetScene.ToString());
    }
    #endregion
}
