using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Function;
using DG.Tweening;
using System;
using UnityEngine.EventSystems;

/*
* Class: UIManager
* Author: 장하원
* Created: 2024년 8월 15일 목요일
* Description: UI 생성 및 관리
*/

public class UIManager : MonoSingleton<UIManager>
{
    private RectTransform _canvasTrm;
    public RectTransform CanvasTrm
    {
        get
        {
            if(_canvasTrm == null )
            {
                // _canvasTrm이 null이라면 Canvas를 찾아온다.
                _canvasTrm = GameObject.FindAnyObjectByType<Canvas>().transform as RectTransform;
            }

            return _canvasTrm;
        }
    }

    // 씬에 나타날 UI들이 담겨있는 배열
    [SerializeField] private SceneUIContent[] _screenElementGroup;

    private Dictionary<SceneType, SceneUIContent> _sceneUIDic = new ();
    private SceneUIContent _currentSceneUIObject;
    public SceneUIContent CurrentSceneUiObject => _currentSceneUIObject;

    private EventSystem _eventSystem;

    private bool _canInteractivle = true;
    public bool CanInteractiveWithUI
    {
        get
        {
            return _canInteractivle;
        }
        set
        {
            // UI와 상호작용 할 수 없도록 eventSystem을 비활성화 한다.
            _eventSystem.enabled = value;
            _canInteractivle = value;
        }
    }

    private void Awake()
    {
        // 빠른 접근을 위해 Dictionary에 담아준다.
        foreach (SceneUIContent su in _screenElementGroup)
        {
            _sceneUIDic.Add(su.UIType, su);
        }

        // 씬이 로드 되기 전 UI를 닫기 위한 구독처리
        SceneLoader.beforeSceneLoad += HandleSceneUIClose;
        // 씬이 로드 된 후 UI를 교체하기 위한 구독처리
        SceneLoader.afterSceneLoad += HandleSceneUIChange;

        ChangeSceneUIOnChangedScene(SceneType.Game);
    }

    private void HandleSceneUIClose(Scene arg0)
    {
        if (_currentSceneUIObject != null)
        {
            // UI 닫기 전 종료 처리
            _currentSceneUIObject.SceneUIEnd();
            Destroy(_currentSceneUIObject.gameObject);
        }
    }

    private void HandleSceneUIChange(Scene scene)
    {
        _eventSystem = FindObjectOfType<EventSystem>();
        // 씬 이름을 통해 enum값을 얻어 반환
        ChangeSceneUIOnChangedScene((SceneType)Enum.Parse(typeof(SceneType), scene.name));
    }

    public void ChangeSceneUIOnChangedScene(SceneType toChangeUIType)
    {
        // 현재 씬에 나타낼 UI가 등록돼있다면
        if (_sceneUIDic.ContainsKey(toChangeUIType))
        {
            // UI 생성
            SceneUIContent suObject = Instantiate(_sceneUIDic[toChangeUIType], CanvasTrm);
            suObject.gameObject.name = _sceneUIDic[toChangeUIType].gameObject.name + "_[SceneUI]_";

            _currentSceneUIObject = suObject;

            // SceneUIContent 하위의 UIObject들 등록
            suObject.GenerateOnUIObject();
            // UI 시작 시 처리
            suObject.SceneUIStart();
        }
    }

    // 자주 사용하는 UIHover이벤트
    public void OnScalingProduction(UIObject uiObject, float scaleValue = 1.1f, float scaleTime = 0.2f)
    {
        uiObject.transform.DOScale(scaleValue, scaleTime);
    }

    // 자주 사용하는 Text이벤트
    public void ChangeTextProduction(UIObject label, string text)
    {
        LabelModule labelText = label as LabelModule;
        labelText.SetText(text);
    }
}
