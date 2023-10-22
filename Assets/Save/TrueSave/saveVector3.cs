using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class saveVector3
{
    float x, y, z;

    public saveVector3(Vector3 Vector3ToSet)
    {
        x = Vector3ToSet.x;
        y = Vector3ToSet.y;
        z = Vector3ToSet.z;
    }

    public void setVector3(Vector3 Vector3ToSet)
    {
        x = Vector3ToSet.x;
        y = Vector3ToSet.y;
        z = Vector3ToSet.z;
    }

    public Vector3 getVector3()
    {
        return new Vector3(x, y, z);
    }
}
