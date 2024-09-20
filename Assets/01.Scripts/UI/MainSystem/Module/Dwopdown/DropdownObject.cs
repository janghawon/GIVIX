using DG.Tweening;
using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/*
* Class: DropdownObject
* Author: 장하원
* Created: 2024년 6월 18일 화요일
* Description: 드롭다운 기능 제작
*/

namespace Function
{
    [Serializable]
    public class DropdownSelection
    {
        public string selectionName;
        public Action onSelectThisSelection;
        public Action onCancleThisSelection;
    }

    public class DropdownObject : UIObject
    {
        [Header("DropdownSetting")]
        private bool _isLookList;
        private bool _isOnScrolling;

        [SerializeField] private TextMeshProUGUI _selectionItemText;
        [SerializeField] private Sprite _checkMarkVisual;
        [SerializeField] private float _scrollingTime = 0.2f;
        [SerializeField] private Transform _listImage;

        private Vector2 _scrollStartValue = Vector2.zero;
        private Vector2 _scrollEndValue = new Vector2(0, 250);

        [Header("DropdownItem")]
        [SerializeField] private RectTransform _contentScroll;
        [SerializeField] private Vector2 _itemSize;
        [SerializeField] private float _padding;
        [SerializeField] private RectTransform _itemContent;
        private GridLayoutGroup _layOutGroup;

        [Header("DropdownList")]
        [SerializeField] private DropdownItem _dropdownItemPrefab;
        [SerializeField] private List<DropdownSelection> _dropdownSelectionList = new();
        private List<DropdownItem> _dropdownItemList = new();
        private DropdownItem _currentDropdownItem;

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
            _layOutGroup = _itemContent.GetComponent<GridLayoutGroup>();
            _layOutGroup.cellSize = _itemSize;
            _layOutGroup.spacing = new Vector2(0, _padding);

            OnClickEvent += HandleEnableContentList;
        }
        private void Start()
        {
            GenerateItemList();
            if (_currentDropdownItem != null)
            {
                _currentDropdownItem.OnCancleAction?.Invoke();
                _currentDropdownItem.Earasing();
            }

            _currentDropdownItem = _dropdownItemList[0];
            _currentDropdownItem?.OnSelectAction?.Invoke();
            _currentDropdownItem.Marking();

            _selectionItemText.text = _dropdownItemList[0].SelectionName;
        }
        public void ReloadScroll(int firstSelectedOrder)
        {
            GenerateItemList();
            SelectionCallback(_dropdownItemList[firstSelectedOrder]);
        }
        private void GenerateItemList()
        {
            _itemContent.sizeDelta = new Vector2(0, _padding * 2);
            foreach (var selection in _dropdownSelectionList)
            {
                if(_dropdownItemList.FirstOrDefault(x => x.SelectionName == selection.selectionName) == null)
                {
                    DropdownItem item = Instantiate(_dropdownItemPrefab, _itemContent);
                    item.SetInfo(selection, this, _checkMarkVisual);
                    _dropdownItemList.Add(item);
                }

                _itemContent.sizeDelta += new Vector2(0, _itemSize.y + _padding);
            }
        }
        private void HandleEnableContentList(UIObject obj)
        {
            if (_isLookList || _isOnScrolling) return;

            _isOnScrolling = true;
            _isLookList = true;

            OnClickEvent -= HandleEnableContentList;
            OnClickEvent += HandleDisableContentList;

            _contentScroll.transform.DOKill();
            DOTween.To(() => _scrollStartValue, y => _contentScroll.sizeDelta = y, _scrollEndValue, _scrollingTime).OnComplete(() => { _isOnScrolling = false; });

            HandleSwitchingListImage();
        }
        private void HandleDisableContentList(UIObject obj)
        {
            if(!_isLookList || _isOnScrolling) return;

            _isLookList = false;

            OnClickEvent += HandleEnableContentList;
            OnClickEvent -= HandleDisableContentList;

            _contentScroll.transform.DOKill();
            DOTween.To(() => _scrollEndValue, y => _contentScroll.sizeDelta = y, _scrollStartValue, _scrollingTime).
            OnComplete(() => 
            {
                _itemContent.localPosition = Vector3.zero;
                _isOnScrolling = false; 
            });

            HandleSwitchingListImage();
        }
        private void HandleSwitchingListImage()
        {
            int temp = _isLookList ? 180 : 0;

            _listImage.transform.DOKill();
            _listImage.transform.DOLocalRotate(new Vector3(0, 0, temp), _scrollingTime);
        }
        public void AddDropdownSelection(string selectionName, Action selectionCallback = null, Action cancleCallback = null)
        {
            DropdownSelection selection = new DropdownSelection();

            selection.selectionName = selectionName;
            selection.onSelectThisSelection += selectionCallback;
            selection.onCancleThisSelection += cancleCallback;

            _dropdownSelectionList.Add(selection);

            _itemContent.sizeDelta = new Vector2(_itemContent.sizeDelta.x + 100, 0);
        }
        public void RemoveDropdownSelection(string selectionName)
        {
            DropdownSelection target = _dropdownSelectionList.FirstOrDefault(x => x.selectionName == selectionName);

            if(target != null)
            {
                _dropdownSelectionList.Remove(target);
            }
            else
            {
                Debug.LogError($"ERROR : \"{selectionName}\" Selection has not exist");
            }

            _itemContent.sizeDelta = new Vector2(_itemContent.sizeDelta.x - 100, 0);
        }
        public void ClearDropdownSelection()
        {
            _dropdownSelectionList.Clear();
        }
        public void SelectionCallback(DropdownItem dropdownItem)
        {
            if(_currentDropdownItem != null)
            {
                _currentDropdownItem.OnCancleAction?.Invoke();
                _currentDropdownItem.Earasing();
            }

            _currentDropdownItem = dropdownItem;
            _currentDropdownItem?.OnSelectAction?.Invoke();
            _currentDropdownItem.Marking();

            _selectionItemText.text = dropdownItem.SelectionName;

            OnClickEvent?.Invoke(null);
        }
    }
}

