using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/*
* Class: ToggleObject
* Author: 장하원
* Created: 2024년 6월 18일 화요일
* Description: 토글 기능 제작
*/

namespace Function
{
    public class ToggleObject : UIObject
    {
        [Header("ToggleSystem")]
        #region ToggleSystem
        private bool _isOnActivation;
        public bool Value => _isOnActivation;

        [SerializeField] private UIObject _toggleHandleObject;
        private UIObject ToggleHandle => _toggleHandleObject;

        private bool _isInBehaviour = false;
        #endregion

        [Header("ToggleSettingValue")]
        #region ToggleSettingValue
        [SerializeField] private float _toggleTime;
        public float ToggleTime
        {
            get => _toggleTime;
            set
            {
                _toggleTime = value;
            }
        }

        [SerializeField] private RectTransform _handleSideArea;

        [SerializeField] private Color _unActiveHandleColor;
        public Color UnActiveHandleColor
        {
            get => _unActiveHandleColor;
            set
            {
                _unActiveHandleColor = value;
            }
        }

        [SerializeField] private Color _unActiveToggleBaseColor;
        public Color UnActiveToggleBaseColor
        {
            get => _unActiveToggleBaseColor;
            set
            {
                _unActiveToggleBaseColor = value;
            }
        }

        [SerializeField] private Color _activeHandleColor;
        public Color ActiveHandleColor
        {
            get => _activeHandleColor;
            set
            {
                _activeHandleColor = value;
            }
        }

        [SerializeField] private Color _activeToggleBaseColor;
        public Color ActiveToggleBaseColor
        {
            get => _activeToggleBaseColor;
            set
            {
                _activeToggleBaseColor = value;
            }
        }
        #endregion

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

        private void Awake()
        {
            _toggleHandleObject.OnClickEvent += HandleToggleHandleClick;

            ToggleActivation(Value);
        }
        private void HandleToggleHandleClick(UIObject obj)
        {
            if (_isInBehaviour) return;

            _isOnActivation = !_isOnActivation;
            ToggleActivationBehaviour(_isOnActivation);
        }
        private void ToggleActivation(bool value)
        {
            (int, Color, Color) toggleValueGroup = GetTogglingValue(value);

            _toggleHandleObject.transform.localPosition = 
            new Vector2((_handleSideArea.sizeDelta.x * 0.5f) * toggleValueGroup.Item1, _toggleHandleObject.transform.localPosition.y);

            _toggleHandleObject.Visual.color = toggleValueGroup.Item2;
            Visual.color = toggleValueGroup.Item3;
        }
        private void ToggleActivationBehaviour(bool value)
        {
            (int, Color, Color) toggleValueGroup = GetTogglingValue(value);

            Sequence togglingSeq = DOTween.Sequence();
            togglingSeq.SetEase(Ease.InCubic);
            togglingSeq.Append(_toggleHandleObject.transform.DOLocalMoveX((_handleSideArea.sizeDelta.x * 0.5f) * toggleValueGroup.Item1, _toggleTime));
            togglingSeq.Join(_toggleHandleObject.Visual.DOColor(toggleValueGroup.Item2, _toggleTime));
            togglingSeq.Join(Visual.DOColor(toggleValueGroup.Item3, _toggleTime));
            togglingSeq.AppendCallback(() => _isInBehaviour = false);
        }
        private (int, Color, Color) GetTogglingValue(bool value)
        {
            int temp = value ? 1 : -1;
            Color handleColor = value ? ActiveHandleColor : UnActiveHandleColor;
            Color toggleBaseColor = value ? ActiveToggleBaseColor : UnActiveToggleBaseColor;

            return (temp, handleColor, toggleBaseColor);
        }
    }
}
