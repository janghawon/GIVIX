using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    [SerializeField] private RectTransform _myTrm;
    public RectTransform RectTrm => _myTrm;

    [SerializeField] private Animator _animator;
    private readonly int _toCenterHash = Animator.StringToHash("ToCenter"); 
    private readonly int _toHideHash = Animator.StringToHash("ToHide");

    private string _toGoSceneName;

    private void Update()
    {
        _myTrm.SetAsLastSibling();
    }

    public void ChangeSceneStart(string sceneName)
    {
        _toGoSceneName = sceneName;

        _animator.SetTrigger(_toCenterHash);
    }

    public void ChangeSceneEnd()
    {
        _animator.SetTrigger(_toHideHash);
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }

    public void ChangeScene()
    {
        SceneLoader.ChangeScene(_toGoSceneName);
    }
}
