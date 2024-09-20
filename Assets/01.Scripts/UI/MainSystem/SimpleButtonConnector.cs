using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Function;
using Extension;

public abstract class SimpleButtonConnector : ExtensionMono
{
    protected void ConnectionBtn(string btnName)
    {
        ButtonModule btnModule = FindUIObject<ButtonModule>(btnName);

        if(btnModule == null)
        {
            Debug.LogError($"Error : Not exist BtnModule : {btnName}");
            return;
        }

        btnModule.OnClickEvent += BtnClickCallback;
        btnModule.OnHoverEvent += BtnHoverCallback;
        btnModule.OnDesecendEvent += BtnDesecendCallback;
    }

    protected abstract void BtnClickCallback(UIObject obj);
    protected abstract void BtnHoverCallback(UIObject obj);
    protected abstract void BtnDesecendCallback(UIObject obj);
}
