using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/*
* Class: SliderObject
* Author: 장하원
* Created: 2024년 6월 18일 화요일
* Description: 간단하게 슬라이더 기능 제작
*/

namespace Function
{
    public class SliderObject : UIObject
    {
        private Slider _slider;
        public UnityAction<float> OnValueChange { get; set; }

        private void Awake()
        {
            _slider = GetComponent<Slider>();
            _slider.onValueChanged.AddListener(OnValueChange);
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

