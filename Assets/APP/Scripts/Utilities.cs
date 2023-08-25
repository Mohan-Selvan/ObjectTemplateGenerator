using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utilities
{
    public static Vector3 ToVector3(this List<float> floatList)
    {
        try
        {
            return new Vector3(floatList[0], floatList[1], floatList[2]);
        }   
        catch(System.Exception e)
        {
            Debug.LogError($"Exception occured during conversion : {e.Message}");
            return Vector3.zero;
        }
    }
}
