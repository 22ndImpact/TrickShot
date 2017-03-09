using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scr_Mgr_Scene : MonoBehaviour
{
    #region Variables
    //Use to keep track of the core scene of the game
    public string CoreScene;
    //Enums used to allow scene switching easier
    public enum Scenes
    {
        Sce_Full_MainMenu,
        Sce_Full_Gameplay,
        Sce_Add_Pause,
        Scenes_Count
    }
    #endregion

    #region Functions

    void Awake()
    {
        //Sets the core scene to the only scene currently active
        CoreScene = SceneManager.GetActiveScene().name;
    }

    /// <summary>
    /// Loads a scene replacing all other scenes current loaded.
    /// </summary>
    /// <param name="_SceneName">The name of the scene to load.</param>
    public void LoadFullScene(string _SceneName)
    {
        SceneManager.LoadScene(_SceneName, LoadSceneMode.Single);
    }

    /// <summary>
    /// Loads a scene on top of all currently loaded scenes.
    /// </summary>
    /// <param name="_SceneName">The name of the scene to load.</param>
    public void LoadAdditiveScene(string _SceneName)
    {
        SceneManager.LoadScene(_SceneName, LoadSceneMode.Additive);
    }

    /// <summary>
    /// Unloads a single scene
    /// </summary>
    /// <param name="_SceneName">The scene to unload</param>
    public void UnloadScene(string _SceneName)
    {
        SceneManager.UnloadSceneAsync(_SceneName);
    }

    /// <summary>
    /// Unloads every scene except the scene name given
    /// </summary>
    /// <param name="_SceneName">The only scene left loaded, usually should be "Sce_Core"</param>
    public void UnloadAllScenes()
    {
        //Unloads every scene except the core scene
        for (int i = 0; i < SceneManager.sceneCount; i++)
        {
            if(SceneManager.GetSceneAt(i).name != CoreScene)
            {
                SceneManager.UnloadSceneAsync(SceneManager.GetSceneAt(i));
            }
        }
        
    }

    #endregion
}
