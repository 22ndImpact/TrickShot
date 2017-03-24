using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scr_LevelUI : MonoBehaviour
{
    Scr_GameplayController GameplayController;

    public Text OrbIndicator;
    public Scr_MedalIndicator MedalIndicator;

    void Awake()
    {
        FindGameplayController();
    }

    void FindGameplayController()
    {
        GameplayController = GameObject.Find("Gameplay Controller").GetComponent<Scr_GameplayController>();
    }

    public void GoToLevelSelect()
    {
        GameplayController.LoadLevelSelect();
    }

    public void RestartLevel()
    {
        GameplayController.UnloadLevelUI();
        GameplayController.LoadLevel(GameplayController.CurrentLevel.levelID);
    }

    public void UpdateOrbDisplay(int _OrbsFired, int _SufficientScore, int _ExceptionalScore)
    {
        OrbIndicator.text = Mathf.Clamp((_SufficientScore - _OrbsFired), 0, 99).ToString();
        MedalIndicator.UpdateIndicator(_OrbsFired, _SufficientScore, _ExceptionalScore);
    }


}
