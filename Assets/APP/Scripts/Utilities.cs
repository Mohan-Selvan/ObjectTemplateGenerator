using System.Collections;
using System.Collections.Generic;
using System.Security;
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

    public static List<float> ToFloatList(this Vector3 vector)
    {
        try
        {
            return new List<float>() { vector.x, vector.y, vector.z };

        }
        catch (System.Exception e)
        {
            Debug.LogError($"Exception occured during conversion : {e.Message}");
            return new List<float> { 0f, 0f, 0f };
        }
    }
}
