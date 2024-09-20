using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CursorBinder : MonoBehaviour
{
    [SerializeField] private RectTransform _cursorPrefab;
    private Transform _cursorTrm;

    private void Awake()
    {
        SceneLoader.afterSceneLoad += HandleLoadedScene;
    }

    private void HandleLoadedScene(Scene arg0)
    {
        if (_cursorTrm != null)
        {
            Destroy(_cursorTrm.gameObject);
        }

        _cursorTrm = Instantiate(_cursorPrefab, UIManager.Instance.CanvasTrm);
        _cursorTrm.transform.localScale = Vector3.one;
    }

    private void LateUpdate()
    {
        if (_cursorTrm == null) return;

        RectTransformUtility.ScreenPointToLocalPointInRectangle(UIManager.Instance.CanvasTrm, Input.mousePosition, Camera.main, out Vector2 point);
        _cursorTrm.localPosition = point;
        _cursorTrm.SetAsLastSibling();

        Cursor.visible = false;
    }
}
