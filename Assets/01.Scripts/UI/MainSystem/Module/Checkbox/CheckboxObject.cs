using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/*
* Class: CheckBoxObject
* Author: 장하원
* Created: 2024년 6월 18일 화요일
* Description: 체크박스 기능 제작
*/

namespace Function
{
    public class CheckboxObject : UIObject
    {
        public Action<bool> OnValueChanged { get; set; }

        [SerializeField] private Image _boxImg;
        public Image BoxImg => _boxImg;

        [SerializeField] private Image _markingImg;
        public Image MarkingImg => _markingImg;

        private bool _value;
        public bool Value
        {
            get
            {
                return _value;
            }
            set
            {
                Marking(value);
                _value = value;
                OnValueChanged?.Invoke(value);
            }
        }

        private void Marking(bool value)
        {
            _markingImg.enabled = value;
        }

        public override void OnPointerClick(PointerEventData eventData)
        {
            Value = !Value;
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
