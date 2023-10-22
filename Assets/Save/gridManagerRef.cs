using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gridManagerRef : MonoBehaviour
{
    public static gridManagerRef _gridManagerRef;
    public Transform placeManager;

    private void Awake()
    {
        _gridManagerRef = this;
    }
}
