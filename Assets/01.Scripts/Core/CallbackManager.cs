using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallbackManager : MonoSingleton<CallbackManager>
{
    // 콜백을 담을 Dictionaary
    private Dictionary<string, Action<object[]>> _actionCallbackDic = new ();

    // 담겨있는 콜백 초기화
    public void ClearAllCallback()
    {
        _actionCallbackDic.Clear();
    }

    // 콜백 등록
    public void RegisterCallback(Action<object[]> callback, string eventkey)
    {
        if(_actionCallbackDic.ContainsKey(eventkey)) 
        {
            // eventKey가 이미 있다면 이미 존재하는 콜백에 구독 처리
            _actionCallbackDic[eventkey] += callback;
            return;
        }

        // eventKey가 없다면 새롭게 추가 
        _actionCallbackDic.Add(eventkey, callback);
    }

    public void UnRegisterCallback(Action<object[]> callback, string eventkey)
    {
        if (_actionCallbackDic.ContainsKey(eventkey))
        {
            // eventKey가 있다면 이미 존재하는 콜백에 구독 해제 처리
            _actionCallbackDic[eventkey] -= callback;
        }
        else
        {
            Debug.LogError($"Error : Not Exist Callback : {eventkey}");
        }
    }

    // eventKey에 해당하는 콜백 모두 제거
    public void RemoveCallback(string eventkey) 
    {
        _actionCallbackDic[eventkey] = null;
        _actionCallbackDic.Remove(eventkey);
    }

    // 콜백 실행
    // params object[]을 통해 편하게 메개 변수를 넘길 수 있다.
    public void Callback(string eventkey, params object[] arr) 
    {
        _actionCallbackDic[eventkey]?.Invoke(arr);
    }
}
