using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Extension;
using Function;

public class CursorConrtroller : ExtensionMono
{
    private Image _cursor;
    private RectTransform _cursorTrm;
    private Vector3 _eulerDir = new Vector3(0, 0, -1);

    public bool IsCursorActive { get; private set; }

    private void OnEnable()
    {
        Cursor.visible = false;
        _cursor = FindUIObject<PanelObject>("TargetMarkCursor").Visual;
        _cursorTrm = _cursor.transform as RectTransform;
    }

    public void VisableCursor()
    {
        IsCursorActive = true;

        _cursor.DOKill();
        _cursor.transform.DOKill();

        _cursor.transform.localScale = Vector3.zero;

        _cursor.DOFade(1, 0.15f);
        _cursor.transform.DOScaleX(1f, 0.2f).SetEase(Ease.OutBack);
        _cursor.transform.DOScaleY(1, 0.22f).SetEase(Ease.OutElastic);
    }

    public void UnVisibleCursor()
    {
        IsCursorActive = false;

        _cursor.DOKill();
        _cursor.transform.DOKill();

        _cursor.DOFade(0, 0.15f);
    }

    private void Update()
    {
        _cursorTrm.localPosition = Camera.main.WorldToScreenPoint(Input.mousePosition);

        _cursorTrm.transform.Rotate(_eulerDir * Time.deltaTime);
    }
}
