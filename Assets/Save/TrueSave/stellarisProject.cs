using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class stellarisProject
{
    public List<galaxyData> GalaxyData;

    [Serializable]
    public struct galaxyData
    {
        public int starCount;
        public string lastEdited;

        public string nameOfGalaxy;
        public string stellarisMapTitle;
        public string version;
    }

    public string projectName;
    public string lastEdited;

    public void addGalaxy(int starCount, string lastEdited, string nameOfGalaxy, string stellarisMapTitle, string version)
    {
        if (GalaxyData == null)
            GalaxyData = new List<galaxyData>();
        this.lastEdited = lastEdited;

        GalaxyData.Add(new galaxyData()
        {
            starCount = starCount,
            lastEdited = lastEdited,
            nameOfGalaxy = nameOfGalaxy,
            stellarisMapTitle = stellarisMapTitle,
            version = version
        }) ;
    }

    public void updateGalaxyData(int starCount, string lastEdited, string nameOfGalaxy, string stellarisMapTitle, string version)
    {
        for(int i = 0;i<GalaxyData.Count;i++)
        {
            if(GalaxyData[i].nameOfGalaxy == nameOfGalaxy)
            {
                this.lastEdited = lastEdited;
                GalaxyData[i] = new galaxyData()
                {

                    starCount = starCount,
                    lastEdited = lastEdited,
                    nameOfGalaxy = nameOfGalaxy,
                    stellarisMapTitle = stellarisMapTitle,
                    version = version
                };
                return;
            }
        }
        Debug.LogWarning("Could not find galaxyToUpdate \n nameOfGalaxy-" + nameOfGalaxy + " so going to add a new one");
        addGalaxy(starCount, lastEdited, nameOfGalaxy, stellarisMapTitle, version);
    }

    public void removeGalaxy(galaxyData galData)
    {
        GalaxyData.Remove(galData);
    }
}
