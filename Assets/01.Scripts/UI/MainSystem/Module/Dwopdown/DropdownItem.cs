using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Function
{
    public class DropdownItem : MonoBehaviour, IPointerClickHandler
    {
        private DropdownObject _ownerDropdown;
        [SerializeField] private TextMeshProUGUI _selectionName;
        [SerializeField] private Image _checkVisual;

        public string SelectionName { get; private set; }
        public Action OnSelectAction { get; private set; }
        public Action OnCancleAction { get; private set; }

        public void SetInfo(DropdownSelection info , DropdownObject owner, Sprite checkMark)
        {
            _ownerDropdown = owner;
            _selectionName.text = info.selectionName;

            SelectionName = info.selectionName;
            OnSelectAction += info.onSelectThisSelection;
            OnCancleAction += info.onCancleThisSelection;

            _checkVisual.sprite = checkMark;
        }

        public void Marking()
        {
            _checkVisual.enabled = true;
        }   
        
        public void Earasing()
        {
            _checkVisual.enabled = false;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            _ownerDropdown.SelectionCallback(this);
        }
    }
}

