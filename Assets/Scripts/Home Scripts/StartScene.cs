using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScene : MonoBehaviour
{


    private void Awake()
    {
        //LevelSystem.SaveProgressData(LevelSystem.CreateProgressData());
        LoadStartScene();
    }

    private void LoadStartScene()
    {
        ProgressData progress = LevelSystem.LoadProgressData();
        if (progress.curWorld == 0)
            SceneManager.LoadSceneAsync("Tutorial");
        else
            SceneManager.LoadSceneAsync("Home");
    }

    // Start is called before the first frame update
}
