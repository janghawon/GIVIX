using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MoreVector
{ 
    public static Vector3 RoundToIntVector(this Vector3 vector)
    {
        Vector3 roundingVec = new Vector3(Mathf.RoundToInt(vector.x),
                                          Mathf.RoundToInt(vector.y),
                                          Mathf.RoundToInt(vector.z));

        return roundingVec;
    }

    public static Vector3 Clamp(this Vector3 vector, Vector3 min, Vector3 max)
    {
        Vector3 clampingVec = new Vector3(Mathf.Clamp(vector.x, min.x, max.x),
                                          Mathf.Clamp(vector.y, min.y, max.y),
                                          Mathf.Clamp(vector.z, min.z, max.z));

        return clampingVec;
    }
}
