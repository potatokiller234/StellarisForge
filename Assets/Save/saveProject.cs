using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class saveProject
{
    public List<saveGalaxy> saved_galaxy;
    public int currentGalaxyID = -1;
    private int savedGalaxyCounter = 0;
    public int lastOpenedGalaxy = -1;
    public string fileName;

    public saveProject()
    {
        saved_galaxy = new List<saveGalaxy>();
    }



    public void saveOpenGalaxy()
    {
        //We are creating a new Galaxy
        if(currentGalaxyID == -1)
        {
            saved_galaxy.Add(new saveGalaxy(savedGalaxyCounter));
            saved_galaxy[savedGalaxyCounter].saveNodeData();
            savedGalaxyCounter++;
            currentGalaxyID = savedGalaxyCounter;
        }
        //Saving an already saved one
        else
        {
            saved_galaxy[currentGalaxyID].saveNodeData();
        }
    }
    public int loadGalaxy()
    {
        if (lastOpenedGalaxy == -1)
            return 0;
        else
            return lastOpenedGalaxy;
        //Will force the saveGalaxy to load their galaxy
    }

}
