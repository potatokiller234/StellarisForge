using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class solarSystemSpawn : MonoBehaviour
{
    //We will be looking for things on the level of 1 or greater
    // public parseStellarisFiles parsedFilesTest;
    public test_parseStellarisFiles parsedFilesTest;
    public List<data_system> systemHolder;


    public GameObject sun;
    public GameObject planet;
    public GameObject moon;
    public GameObject asteroid_belt;

    public bool isDoneConvertingData = false;

    void Start()
    {
        systemHolder = new List<data_system>();
    }

    public void stellarisNode_to_UnityData()
    {
        systemHolder = new List<data_system>();
        Debug.LogWarning("Starting Unity PARSE");

       // Debug.Log(parsedFilesTest.allFilesOutput.Count + " HOW BIG");
        int currentIndex = -1;
        Debug.Log(parsedFilesTest.allFilesOutput.Count + " num of mods");
        foreach (stellarisDataHolder bob in parsedFilesTest.allFilesOutput)
        {
          //  Debug.LogError("Starting New Mod");
            foreach (stellarisNode initilaizer in bob.infoHolder.dataHolder)
            {
               // Debug.LogError("Starting new init");
                currentIndex = currentIndex + 1;
                Debug.Log("Name-" + initilaizer.name + " currentIndex-" + currentIndex);

                //First we add a new solarSystem to systemHolder
                systemHolder.Add(new data_system(initilaizer.name, bob.infoHolder.parent.name,initilaizer.fileID));

                //We are now inside the initilaizerData
                if (initilaizer.dataHolder != null)
                {
                    foreach (stellarisNode initilaizerData in initilaizer.dataHolder)
                    {
                      //  Debug.Log("Name-" + initilaizerData.name + " currentIndex-" + currentIndex + " lengthOfDataHolder-" + initilaizer.dataHolder.Count);

                        //We know this is the class of the system
                        if (initilaizerData.name == "class")
                        {
                            //If we get somthing like class = "sc_whitte_hole", it will add a new data_system with the system type of sc_whitte_hole, though the system will not have a declared name
                            systemHolder[currentIndex].systemClass = initilaizerData.data;
                            //Debug.LogError(initilaizerData.data + " name Of System");
                        }
                        if (initilaizerData.name == "name")
                        {
                           // Debug.LogWarning("Name is " + initilaizerData.data);
                            //This sets the name of the system such as name = "Gateway to Heaven"
                            systemHolder[currentIndex].name = initilaizerData.data;
                        }
                        if (initilaizerData.name == "planet")
                        {
                            systemHolder[currentIndex].addPlanet(addNewPlanet(initilaizerData));
                        }
                        if (initilaizerData.name == "asteroid_belt")
                        {
                            systemHolder[currentIndex].addAsteroid(addNewAsteroidBelt(initilaizerData));
                        }
                        if (initilaizerData.name == "usage_odds")
                        {
                            systemHolder[currentIndex].setDLC(setNewDLC(initilaizerData));
                        }
                    }
                }
            }
        }

        for (int i = 0; i < systemHolder.Count; i++)
        {
            //Debug.Log(systemHolder[i].printOutPlanet());
            //Debug.Log("Planet Has");
            foreach (data_planet fred in systemHolder[i].planets)
            {
                string memaw = systemHolder[i].printOutPlanet(fred.moons);
                if (string.IsNullOrEmpty(memaw) == false)
                {
                    //Debug.Log(memaw);
                }
            }
        }

        isDoneConvertingData = true;
    }


    //public void spawnOneSystem(int indexOfSystemToSpawn)
    //{
    //    List<GameObject> spawnedObj = new List<GameObject>();
    //    data_system currentSolar = systemHolder[indexOfSystemToSpawn];

    //    bool sunSpawnedYet = false;
    //    double orbit_distance = 0;
    //    foreach (data_planet planet in currentSolar.planets)
    //    {
    //        //We know this is an asteroid belt
    //        if(planet.asteroid_radius != -1)
    //        {
    //            GameObject temp = Instantiate(asteroid_belt, this.transform.position, Quaternion.Euler(90, 0, 0));
    //            temp.transform.localScale = new Vector3((int)planet.asteroid_radius, (int)planet.asteroid_radius, 1);
    //            temp.GetComponent<data_Holder>().planetData = planet;
    //        }
    //        //RULE : First planet that is not an asteroid belt is always the sun
    //        else if(sunSpawnedYet == false)
    //        {
    //            //At the current moment I am trying to deal with all the max min stuff
    //            //Orbit distance and its flaws
    //            //Don't forget about varible integration
    //            sunSpawnedYet = true;
    //            GameObject temp = Instantiate(sun, this.transform.position, Quaternion.Euler(0, 0,0));
    //            temp.transform.localScale = new Vector3((int)planet.asteroid_radius, (int)planet.asteroid_radius, 1);
    //            temp.GetComponent<data_Holder>().planetData = planet;
    //        }
    //        //This is now a normal planet
    //        else
    //        {
    //            //Do planet stuff then
    //            foreach(data_planet moons in planet.moons)
    //            {
    //                //Do moon stuff
    //            }
    //        }
    //    }
    //}


    //When this is called we know we are now dealing with a planet so we must recserivly go through it to find all the planet data
    public data_planet addNewPlanet(stellarisNode planetParent)
    {
        data_planet planet = new data_planet();
        //We are now looking at all the planet data such as count,name,class,orbit_angle,size,ect
        float[] tempForData;
        foreach(stellarisNode planetData in planetParent.dataHolder)
        {

            //These are just one value it can't be a range of values
            if (planetData.name == "moon")
            {
                //What this will do is add a new moon if moon is called
                planet.addMoon(addNewPlanet(planetData));
            }
            else if (planetData.name == "name")
            {
                planet.planet_name = planetData.data;
            }
            else if (planetData.name == "class")
            {
                planet.planet_class = planetData.data;
            }
            else if (planetData.name == "has_ring")
            {
                planet.hasRing = myStaticScripts.staticScripts.stringToBool(planetData.data);
            }
            else if (planetData.name == "home_planet")
            {
                planet.home_planet = myStaticScripts.staticScripts.stringToBool(planetData.data);
            }
            //These now can be a range of values
            else if (planetData.name == "count")
            {
                tempForData = getMinAndMax(planetData);
                planet.planet_countMax = tempForData[0];
                planet.planet_countMin = tempForData[1];
            }
            else if (planetData.name == "orbit_distance")
            {
                tempForData = getMinAndMax(planetData);
                planet.planet_orbit_distanceMax = tempForData[0];
                planet.planet_orbit_distanceMin = tempForData[1];
            }
            else if (planetData.name == "orbit_angle")
            {
                tempForData = getMinAndMax(planetData);
                planet.planet_orbit_angleMax = tempForData[0];
                planet.planet_orbit_angleMin = tempForData[1];
            }
            else if (planetData.name == "size")
            {
                tempForData = getMinAndMax(planetData);
                planet.planet_sizeMax = tempForData[0];
                planet.planet_sizeMin = tempForData[1];
            }
        }
        return planet;
    }
    public data_asteroid addNewAsteroidBelt(stellarisNode planetParent)
    {
        data_asteroid asteroid = new data_asteroid();
        foreach (stellarisNode asteroidData in planetParent.dataHolder)
        {
            //These are just one value it can't be a range of values
            if (asteroidData.name == "type")
            {
                asteroid.type = asteroidData.data;

            }
            else if (asteroidData.name == "radius")
            {
                asteroid.radius = getMinAndMax(asteroidData)[0];
               // asteroid.radius = int.Parse(asteroidData.data);
            }
        }
        return asteroid;

    }

    public DLC setNewDLC(stellarisNode DLCparent)
    {
        DLC thingToReturn = DLC.noDLC;
        getDLCInfo(DLCparent, ref thingToReturn);
        return thingToReturn;
    }

    private void getDLCInfo(stellarisNode nodeToSearch, ref DLC resultDLC)
    {
        if (nodeToSearch.dataHolder != null)
        {
            for (int i = 0; i < nodeToSearch.dataHolder.Count; i++)
            {
                getDLCInfo(nodeToSearch.dataHolder[i], ref resultDLC);
            }
        }

        if(resultDLC == DLC.noDLC && nodeToSearch.name == "host_has_dlc")
        {
            string temp = nodeToSearch.data.Replace("\"","");
            #region Parse Stellaris To Unity For DLC
            switch (temp)
            {
                case "Utopia":
                    resultDLC = DLC.utopia;
                    break;
                case "Megacorp":
                    resultDLC = DLC.megacorp;
                    break;
                case "Federations":
                    resultDLC = DLC.federations;
                    break;
                case "Apocalypse":
                    resultDLC = DLC.apocalypse;
                    break;
                default:
                    DEBUG_EntireDebug.DEBUG_entireDebug.printGenericErrorInformation(DEBUG_EntireDebug.DEBUG_entireDebug.isDLC_error, "Unknown DLC " + nodeToSearch.data + " for " + nodeToSearch.name);
                    break;
            }
            #endregion
        }
    }


    private float[] getMinAndMax(stellarisNode planetData)
    {

        float[] output = new float[2];
        //Has data
        if(planetData.dataHolder == null)
        { 
            float dataToSave = isVarSingle(planetData);
            //Debug.Log("Before " + planetData.data + " now it is " + dataToSave);

            output[0] = dataToSave;
            output[1] = output[0];

            //output[0] = int.Parse(planetData.data);
            //output[1] = output[0];
        }
        //Does not have data
        else
        {
            float[] dataToSave = isVarMany(planetData);

            output[0] = dataToSave[0];
            output[1] = dataToSave[1];

            //Debug.Log("Before Data -" + planetData.dataHolder[1].data + "|" + planetData.dataHolder[0].data + 
            //    "\n" + "After Data -" + output[0] + "|" + output[1]);

            //Debug.Log(planetData.dataHolder[1].data);
            //output[0] = int.Parse(planetData.dataHolder[1].data);
            //output[1] = int.Parse(planetData.dataHolder[0].data);
        }
        return output;
    }

    private float isVarSingle(stellarisNode planetData)
    {
        float result = -1;
        if (planetData.data.Contains("@"))
        {
            //First we check if the var is local
            string output = planetData.getVariableValue(planetData, planetData.data);
            //If we can't find it we search the global vars
            if(string.IsNullOrEmpty(output) == true)
            {
                for(int i = 0;i< parsedFilesTest.scripted_variables.Count;i++)
                {
                    if(parsedFilesTest.scripted_variables[i].name == planetData.data)
                    {
                        return float.Parse(parsedFilesTest.scripted_variables[i].data);
                    }
                }
            }
            else
            {
                return float.Parse(output);
            }
            Debug.LogError("Cant find var " + planetData.data + " with the fileID of " + planetData.fileID);
            return 0;
        }
        else
        {
            try
            {
                //If it is random then just do 20
                if (planetData.data == "random")
                    return 20;


                result = float.Parse(planetData.data);
                return result;
            }
            catch (System.FormatException e)
            {
                
                Debug.LogError("Invalid parse type: tried to parse " + planetData.data + " into a float " + ", it is in file " + planetData.fileID);
            }
            //Debug.Log(planetData.data);
            return result;
        }
    }
    private float[] isVarMany(stellarisNode planetData)
    {
        float[] outPut = new float[2];
        outPut[0] = isVarSingle(planetData.dataHolder[1]);
        outPut[1] = isVarSingle(planetData.dataHolder[0]);
        return outPut;
    }
}
