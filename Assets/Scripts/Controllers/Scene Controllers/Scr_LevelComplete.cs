using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_LevelComplete : MonoBehaviour
{
    Scr_GameplayController GameplayController;
    public Scr_MedalIndicator MedalIndicator;

    void Awake()
    {
        FindGameplayController();
    }

    void Start()
    {
        MedalIndicator.UpdateEndOfLevelIndicator(GameplayController.CurrentLevel.OrbsFired, GameplayController.CurrentLevel.SufficientScore, GameplayController.CurrentLevel.ExceptionalScore);
    }

    void FindGameplayController()
    {
        GameplayController = GameObject.Find("Gameplay Controller").GetComponent<Scr_GameplayController>();
    }

    public void LoadNextLevel()
    {
        GameplayController.LoadNextLevel();
    }

    public void RetryLevel()
    {
        GameplayController.ReloadLevel();
    }

    
}


