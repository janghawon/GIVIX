using UnityEngine;
using UnityEngine.EventSystems;
using System;
using UnityEngine.UI;
using Extension;

/*
* Class: UIObject
* Author: 장하원
* Created: 2024년 6월 13일 목요일
* Description: 모든 UI에게 넣어주는 클래스, 
*              클릭, 호버, 디센드 시 이벤트 관리
*/

[Serializable]
public struct UISFX
{
    public AudioClip clickSFX;
    public AudioClip hoverSFX;
    public AudioClip descendSFX;
}

namespace Function
{
    public class UIObject : ExtensionMono,
    IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        [SerializeField] private UIKeyword _myKeword;
        public UIKeyword Keyword => _myKeword;

        private Image _visual;
        public Image Visual
        {
            get
            {
                if(_visual == null)
                {
                    _visual = GetComponent<Image>();
                }
                return _visual;
            }
        }

        public UISFX uiSFX;

        public Action<UIObject> OnClickEvent { get; set; }
        public Action<UIObject> OnHoverEvent { get; set; }
        public Action<UIObject> OnDesecendEvent { get; set; }

        public void HandleClear()
        {
            OnClickEvent = null;
            OnHoverEvent = null;
            OnDesecendEvent = null;
        }

        public virtual void OnPointerClick(PointerEventData eventData)
        {
            if(uiSFX.clickSFX != null)
                SoundManager.Instance.PlaySFX(uiSFX.clickSFX);

            OnClickEvent?.Invoke(this);
        }

        public virtual void OnPointerEnter(PointerEventData eventData)
        {
            if (uiSFX.hoverSFX != null)
                SoundManager.Instance.PlaySFX(uiSFX.hoverSFX);

            OnHoverEvent?.Invoke(this);
        }

        public virtual void OnPointerExit(PointerEventData eventData)
        {
            if (uiSFX.descendSFX != null)
                SoundManager.Instance.PlaySFX(uiSFX.descendSFX);

            OnDesecendEvent?.Invoke(this);
        }
    }
}

