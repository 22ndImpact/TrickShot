using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_Button_LoadLevel : MonoBehaviour
{
    #region Variables
    //The target scene of the transition
    Scr_Level Level;
    #endregion

    #region Functions
    /// <summary>
    /// Activate the scene change button and activates the appropriate function changing function
    /// </summary>
    public void ActivateButton()
    {
       //Scr_Mgr_Game.inst.Manager_GamePlay.currentLevel = Level
    }
    #endregion
}
