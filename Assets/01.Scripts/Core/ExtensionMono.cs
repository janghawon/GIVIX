using System;
using Function;
using UnityEngine;

/*
* Class: ExtensionMono
* Author: 장하원
* Created: 2024년 6월 13일 목요일
* Description: 추가적인 시스템에 편하게 접근하고 싶을 때 상속받는 모노
*/

namespace Extension
{
    // MonoBehaviour를 상속받아 편하게 기능에 접근할 수 있도록 도와주는 확장 MonoBehaviour
    public abstract class ExtensionMono : MonoBehaviour
    {
        public void AddSetupCallback(Action callBack)
        {
            SceneUIContent content = UIManager.Instance.CurrentSceneUiObject;
            content.SceneUIStartAction += callBack;
        }

        public T FindUIObject<T>(UIKeyword uiMask) where T : UIObject
        {
            SceneUIContent content = UIManager.Instance.CurrentSceneUiObject;
            return content.FindUIObject<T>(uiMask);
        }

        public T FindUIObject<T>(string objectName) where T : UIObject
        {
            SceneUIContent content = UIManager.Instance.CurrentSceneUiObject;
            return content.FindUIObject<T>(objectName);
        }

        public UIObject[] FindAllUIObject<T>(UIKeyword uiMask) where T : UIObject
        {
            SceneUIContent content = UIManager.Instance.CurrentSceneUiObject;
            return content.FindAllUIObject<T>(uiMask);
        }
    }
}

