using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System;

namespace Function
{
    // 씬의 존재하는 모든 UI를 자식으로 두는 부모가 갖고 있는 스크립트
    public abstract class SceneUIContent : MonoBehaviour
    {
        public Action SceneUIStartAction { get; set; }

        // 스스로의 씬 타입 할당
        [SerializeField] private SceneType _uiType;
        public SceneType UIType => _uiType;

        // 자식 중 존재하는 UI를 담은 리스트
        protected List<UIObject> _childUIList = new List<UIObject>();

        // 자식 중 존재하는 UI의 특징을 Enum Flag에 담아 매개변수로 넘겨주면
        // 해당하는 특징을 가진 UI 를 리턴해주는 메서드
        public T FindUIObject<T>(UIKeyword keywordMask) where T : UIObject
        {
            // linq문법을 통해 조건이 포함되는 UI를 탐색
            UIObject uiObject = 
            _childUIList.FirstOrDefault(x => (x.Keyword & keywordMask) == keywordMask);

            // 찾지 못한다면 Error 메세지와 함께 함수 실행 종료
            if(uiObject == null)
            {
                Debug.LogError($"Cant Found UiObject : {typeof(T)} \nPlease Check Your Keword : {keywordMask}");
                return null;
            }

            // 찾은 UI를 형식에 맞게 변환 후 리턴
            return uiObject as T;
        }

        // 자식 중 존재하는 UI를 이름을 통해 찾아 리턴해주는 메서드
        public T FindUIObject<T>(string objName) where T : UIObject
        {
            // linq 문법을 사용해 UI의 이름으로 특정 UI를 찾는다.
            UIObject uiObject =
            _childUIList.FirstOrDefault(x => x.gameObject.name == objName);

            // 찾지 못한다면 Error 메세지와 함께 함수 실행 종료
            if (uiObject == null)
            {
                Debug.LogError($"Cant Found UiObject : {typeof(T)} \nPlease Check Object Name : {objName}");
                return null;
            }

            // 찾은 UI를 형식에 맞게 변환 후 리턴
            return uiObject as T;
        }

        // 자식 중 존재하는 UI의 특징을 Enum Flag에 담아 매개변수로 넘겨주면
        // 해당하는 특징을 가진 모든 UI 를 리턴해주는 메서드
        public UIObject[] FindAllUIObject<T>(UIKeyword keywordMask) where T : UIObject
        {
            // linq 문법을 사용해 넘겨받은 특징에 해당하는 모든 UI를 찾는다.
            UIObject[] uiObjetArr =
            _childUIList.FindAll(x => (x.Keyword & keywordMask) == keywordMask).ToArray();

            // 찾은 UI들을 배열의 형태로 리턴
            return uiObjetArr;
        }

        // 간단한 DFS탐색 알고리즘을 통해 모든 자식을 순회하며 UI를 찾아 리스트에 넣어주는 메서드
        public void GenerateOnUIObject()
        {
            _childUIList.Clear();
            Stack<Transform> stack = new Stack<Transform>();

            // 가장 상위 transform을 스택에 넣어준다.
            stack.Push(transform);

            while (stack.Count > 0)
            {
                // 탐색할 trnasform을 stack에서 찾는다.
                Transform current = stack.Pop();

                // transform의 자식들을 순회하며 UI를 찾는다.
                foreach (Transform child in current)
                {
                    if(child.TryGetComponent<UIObject>(out UIObject ui))
                    {
                        _childUIList.Add(ui);
                    }
                    
                    // transform의 자식을 stack에 넣는다.
                    stack.Push(child);
                }
            }
        }

        // 해당 UI 그룹이 생성되었을 때 호출
        public abstract void SceneUIStart();

        // 해당 UI 그룹이 삭제되었을 때 호출
        public abstract void SceneUIEnd();
    }
}
