using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoSingleton<GameManager>
{
    [SerializeField] private SceneChanger _sceneChangerPrefab;

    private void Awake()
    {
        SceneLoader.ActivationSceneLoader();
    }

    private void Start()
    {
        SceneLoader.afterSceneLoad += HandleSceneLoaded;
    }

    private void HandleSceneLoaded(Scene arg0)
    {
        var sceneChanger = CreateSceneChanger();
        sceneChanger.ChangeSceneEnd();
    }

    public void ChangeScene(string sceneName)
    {
        var sceneChanger = CreateSceneChanger();
        sceneChanger.ChangeSceneStart(sceneName);
    }

    private SceneChanger CreateSceneChanger()
    {
        var sceneChanger = Instantiate(_sceneChangerPrefab, UIManager.Instance.CanvasTrm);

        return sceneChanger;
    }
}
