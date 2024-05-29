using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ExtensionMethods
{
    public static class ObjectExtension
    {
        public static T GetRandomInstance<T>(this T[] arr)
        {
            return arr[Random.Range(0, arr.Length)];
        }

        public static T GetRandomInstance<T>(this List<T> arr)
        {
            return arr[Random.Range(0, arr.Count)];
        }
    }
}

