using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new dataSave_Galaxy", menuName = "GalaxyBlacksmith/dataSave_Galaxy", order = 1)]
public class dataSave_Galaxy : ScriptableObject
{
    public GameObject example;
    public List<Vector3> nodePos;
    public List<GameObject> gameObjectRef;
}
