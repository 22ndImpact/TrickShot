using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scr_SceneController
{

	// Update is called once per frame
	public void AddScene (string _SceneName)
    {
        SceneManager.LoadScene(_SceneName, LoadSceneMode.Additive);
	}

    public void RemoveScene(string _SceneName)
    {
        SceneManager.UnloadSceneAsync(_SceneName);
    }
}
