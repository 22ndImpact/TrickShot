using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_Mgr_Game : MonoBehaviour
{
    #region Variables
    //The default scene that the core scene loads at the start of the game
    public Scr_Mgr_Scene.Scenes DefaultScene;
    #endregion

    #region Objects
    //Initializing a globally accessable game director
    public static Scr_Mgr_Game inst;

    #region Manager Initializations
    [HideInInspector]
    public Scr_Mgr_Scene Manager_Scene;

    [HideInInspector]
    public Scr_Mgr_GamePlay Manager_GamePlay;
    #endregion

    #endregion

    #region Functions
    void Awake()
    {
        //Setting up the bad singleton pattern
        inst = this;

        #region Manager Initializations
        //Adds a scene manager to the GameDirectors components and sets up a public link to it
        Manager_Scene = gameObject.AddComponent<Scr_Mgr_Scene>();
        #endregion
    }
    void Start()
    {
        //Loads the default scene
        Manager_Scene.LoadAdditiveScene(DefaultScene.ToString());
    }
    #endregion
}
