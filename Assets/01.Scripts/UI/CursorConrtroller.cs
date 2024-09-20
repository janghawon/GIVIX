using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CursorConrtroller : MonoBehaviour
{
    private Image _cursor;

    private void OnEnable()
    {
        Cursor.visible = false;
    }

    public void VisableCursor()
    {
        _cursor.DOKill();
        _cursor.transform.DOKill();

        _cursor.transform.localScale = Vector3.zero;

        _cursor.DOFade(1, 0.15f);
        _cursor.transform.DOScaleX(1f, 0.2f).SetEase(Ease.OutBack);
        _cursor.transform.DOScaleY(1, 0.22f).SetEase(Ease.OutElastic);
    }

    public void UnVisibleCCursor()
    {
        _cursor.DOKill();
        _cursor.transform.DOKill();

        _cursor.DOFade(0, 0.15f);
    }
}
