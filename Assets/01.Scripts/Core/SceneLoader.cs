using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneLoader
{
    public static bool IsActive { get; private set; }

    public delegate void BeforeSceneLoad(Scene scene);
    public delegate void AfterSceneLoad(Scene scene);

    public static BeforeSceneLoad beforeSceneLoad;
    public static AfterSceneLoad afterSceneLoad;

    public static void ActivationSceneLoader()
    {
        if(IsActive)
        {
            Debug.LogError("SceneLoader has already Activation!");
            return;
        }

        IsActive = true;

        SceneManager.sceneLoaded += (scene, mode) => 
        afterSceneLoad?.Invoke(SceneManager.GetActiveScene());
    }

    public static void ChangeScene(string scnenName)
    {
        if(!IsActive)
        {
            Debug.LogError("SceneLoader has not Activation!");
            return;
        }

        beforeSceneLoad?.Invoke(SceneManager.GetActiveScene());
        SceneManager.LoadScene(scnenName);
    }
}
