using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DebuggingExtensions
{
    
    public static void Log(this Vector2 vector)
    {
        Debug.Log($"x : {vector.x}; y : {vector.y}");
    }
    
    public static void Log(this Vector3 vector)
    {
        Debug.Log($"x : {vector.x}; y : {vector.y}; z : {vector.z}");
    }

}
