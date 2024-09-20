using DG.Tweening;
using Extension;
using Febucci.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

/*
* Class: LabelModule
* Author: 장하원
* Created: 2024년 6월 19일 수요일
* Description: 간단한 라벨 텍스트 모듈
*/

namespace Function
{
    public class LabelModule : UIObject
    {
        [SerializeField] private float _fadingTime;

        [SerializeField] private TextMeshProUGUI _text;
        public TextMeshProUGUI Text => _text;

        public string TypedText => _text.text;

        [SerializeField] private TextAnimator_TMP _animator;
        public TextAnimator_TMP Animator => _animator;

        public void SetText(string text)
        {
            Text.text = text;
        }

        public override void OnPointerClick(PointerEventData eventData)
        {
            base.OnPointerClick(eventData);
        }
        public override void OnPointerEnter(PointerEventData eventData)
        {
            base.OnPointerEnter(eventData);
        }
        public override void OnPointerExit(PointerEventData eventData)
        {
            base.OnPointerExit(eventData);
        }
    }
}

