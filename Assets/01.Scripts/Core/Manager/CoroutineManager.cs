using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class CoroutineInfoBox 
{
    private int _instanceID;
    private string _coName;
    public Coroutine Coroutine { get; private set; }

    public CoroutineInfoBox(int instanceID, string coName, Coroutine coroutine)
    {
        _instanceID = instanceID;
        _coName = coName;
        Coroutine = coroutine;
    }

    public bool Checked(int instaceID, string coroutineName)
    {
        return (instaceID == _instanceID) && (coroutineName == _coName);
    }
}


public class CoroutineManager : MonoSingleton<CoroutineManager>
{
    private Dictionary<int, CoroutineInfoBox> _coroutineSaveDic = new();

    public void CoroutineStart(MonoBehaviour mono, IEnumerator coroutine)
    {
        int instanceid = mono.GetInstanceID();
        string coName = nameof(coroutine);
        
        if(_coroutineSaveDic.TryGetValue(instanceid, out var value))
        {
            if(value.Checked(instanceid, coName))
            {
                mono.StopCoroutine(value.Coroutine);
            }
        }

        Coroutine co = mono.StartCoroutine(coroutine);
        CoroutineInfoBox box = new CoroutineInfoBox(instanceid, coName, co);
        
        _coroutineSaveDic.Add(mono.GetInstanceID(), box);
    }

    public void CoroutineStop(MonoBehaviour mono, IEnumerator coroutine)
    {
        int instanceID = mono.GetInstanceID();
        string coName = nameof(coroutine);

        if(_coroutineSaveDic.TryGetValue(instanceID, out var value))
        {
            if(value.Checked(instanceID, coName))
            {
                mono.StopCoroutine(value.Coroutine);
            }
            else
            {
                Debug.LogError($"Error : {nameof(coroutine)} is not running!");
                return;
            }
        }
        else
        {
            Debug.LogError($"Error : {nameof(coroutine)} is not running!");
            return;
        }
    }
}
