using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coolGuy : MonoBehaviour
{
    private List<GameObject> smallFellas;
    private List<System.Guid> smallFellasID;
   // private System.Guid currentFellaID;

    public coolGuy()
    {
        smallFellas = new List<GameObject>();
        smallFellasID = new List<System.Guid>();
       // currentFellaID = idGenerator._idGenerator.getCurrentID();
    }
    #region Adding a fella
    public System.Guid addFella(GameObject fella)
    {
        smallFellas.Add(fella);
        System.Guid ID = idGenerator._idGenerator.generateNewID();
        smallFellasID.Add(ID);
        return ID;
    }
    //Idea add IDS to each thing like nodes,hyperlnas, and UI buttons
    //Add a new method that will addAFella but without changing the currentFellaID
    public void addFella(GameObject fella, System.Guid fellaID)
    {
        smallFellas.Add(fella);
        smallFellasID.Add(fellaID);
    }
    #endregion
    #region Remove a fella
    public void removeFella(GameObject fella)
    {
        smallFellasID.Remove(smallFellasID[smallFellas.IndexOf(fella)]);
        smallFellas.Remove(fella);
    }
    public void removeFella(System.Guid id)
    {
        smallFellas.Remove(smallFellas[smallFellasID.IndexOf(id)]);
        smallFellasID.Remove(id);
    }
    #endregion


    #region Utilities
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
        for (int i = 0; i < smallFellasID.Count; i++)
        {
            if (smallFellasID[i] == id)
            {
                return smallFellas[i];
            }
        }
        if (debug == true)
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
    #endregion






}
