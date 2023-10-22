using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_mouseManager : MonoBehaviour
{
    public bool isInGridArea = false;

    public UI_mouseLocation[] mouseLocations;
    public bool[] isInMouseLocations;
    public static UI_mouseManager _UI_mouseManager;

    private void Awake()
    {
        _UI_mouseManager = this;
        isInMouseLocations = new bool[mouseLocations.Length];
        for(int i = 0;i<mouseLocations.Length;i++)
        {
            mouseLocations[i].id = i;
        }
    }
    public void inGridArea(int id)
    {
        isInMouseLocations[id] = true;
        checkIfInGrid();
    }
    public void outOfGridArea(int id)
    {
        isInMouseLocations[id] = false;
        checkIfInGrid();
    }


    private void checkIfInGrid()
    {
        for(int i = 0;i<isInMouseLocations.Length;i++)
        {
            if (isInMouseLocations[i] == true)
            {
                isInGridArea = true;
                return;
            }
        }
        isInGridArea = false;
    }

}
