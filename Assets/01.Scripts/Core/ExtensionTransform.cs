using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ExtensionTransform 
{
    public static void Clear(this Transform transform)
    {
        foreach(Transform trm in transform)
        {
            GameObject.Destroy(trm.gameObject);
        }
    }
}
