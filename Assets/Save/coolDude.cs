using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coolDude
{
    private List<GameObject> smallFellas;
    private List<System.Guid> smallFellasID;
    public coolDude()
    {
        smallFellas = new List<GameObject>();
        smallFellasID = new List<System.Guid>();
    }

    public List<System.Guid> getSmallFellaID()
    {
        return smallFellasID;
    }

    public int getCount()
    {
        if (smallFellas.Count != smallFellasID.Count)
            Debug.LogError("ERROR both fellas are not the same size smallFella:" + smallFellas.Count + " smallFellaID:" + smallFellasID);
        return smallFellas.Count;
    }

    public GameObject getID(System.Guid id, bool debug)
    {
        for(int i = 0;i<smallFellasID.Count;i++)
        {
            if(smallFellasID[i] == id)
            {
                return smallFellas[i];
            }
        }
        if(debug == true)
            Debug.LogError("Invalid id " + id + " was Null");
        return null;
    }
    public System.Guid getID(GameObject thingToLookFor)
    {
        for (int i = 0; i < smallFellas.Count; i++)
        {
            if (smallFellas[i] == thingToLookFor)
            {
                return smallFellasID[i];
            }
        }
        return System.Guid.Empty;
    }
    public bool doesHaveId(System.Guid id)
    {
        for (int i = 0; i < smallFellasID.Count; i++)
        {
            if (smallFellasID[i] == id)
            {
                return true;
            }
        }
        return false;
    }
    public System.Guid addFella(GameObject fella)
    {
        smallFellas.Add(fella);
        System.Guid tempID = idGenerator._idGenerator.generateNewID();
        smallFellasID.Add(tempID);
        return tempID;
    }
    //Idea add IDS to each thing like nodes,hyperlnas, and UI buttons
    //Add a new method that will addAFella but without changing the currentFellaID
    public void addOldFella(GameObject fella, System.Guid fellaID)
    {
        smallFellas.Add(fella);
        smallFellasID.Add(fellaID);
    }
    public void removeFella(GameObject fella)
    {
        //Debug.Log("Index of " + fella.name + " is " + smallFellas.IndexOf(fella));

        smallFellasID.Remove(smallFellasID[smallFellas.IndexOf(fella)]);
        smallFellas.Remove(fella);
    }
    public void removeFella(System.Guid id)
    {
        smallFellas.Remove(smallFellas[smallFellasID.IndexOf(id)]);
        smallFellasID.Remove(id);
    }



}
