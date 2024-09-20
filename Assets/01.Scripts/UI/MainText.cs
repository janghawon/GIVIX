using DG.Tweening;
using Febucci.UI;
using Febucci.UI.Core.Parsing;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MainText : PoolableMono
{
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private TypewriterByWord _typeWriter;
    [SerializeField] private AudioClip _textingSound;

    private Action _textingEndCallback;

    public void Texting(string sentence, Action callback = default)
    {
        _typeWriter.onMessage.AddListener(HandleEndOfMessage);
        _typeWriter.onCharacterVisible.AddListener((s) => SoundManager.Instance.PlaySFX(_textingSound));
        _text.text = sentence + " <?EndOfWriting>";
        _textingEndCallback = callback;
    }

    private void HandleEndOfMessage(EventMarker maker)
    {
        switch (maker.name)
        {
            case "EndOfWriting":
                _text.DOFade(0, 0.3f).OnComplete(() =>
                {
                    _textingEndCallback?.Invoke();
                    _typeWriter.onTextDisappeared.AddListener(() => PoolManager.Instance.Push(this));
                });
                break;
        }
    }

    public override void OnPop()
    {
    }

    public override void OnPush()
    {
        Color color = _text.color;
        color.a = 1;
        _text.color = color;
    }
}
