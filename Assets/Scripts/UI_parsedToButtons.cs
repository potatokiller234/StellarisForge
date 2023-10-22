using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UI_parsedToButtons : MonoBehaviour
{
    public solarSystemSpawn SolarSystemSpawn;
    public GameObject example_button;
    public Transform button_Holder;
    public List<UI_nodeDataMod> UI_nodeDataModeHolder;


    [Header("For Searching")]
    List<UI_nodeDataSystem> dataSystems;


    //Stuff To Customize it
    [Header("Customize Mod")]
    public float yModSize = 100;
    public Color32 colorMod;

    [Header("Customize File")]
    public float yFileSize = 75;
    public Color32 colorFile;
    
    [Header("Customize System")]
    public float ySystemSize = 50;
    public Color32 colorSystem;


    private void Start()
    {
        UI_nodeDataModeHolder = new List<UI_nodeDataMod>();
        dataSystems = new List<UI_nodeDataSystem>();
    }



    public void spawnInButtons()
    {
        Debug.LogError(SolarSystemSpawn.systemHolder.Count);
        for(int i = 0;i< SolarSystemSpawn.systemHolder.Count; i++)
        {
            checkIfItemIsThere(SolarSystemSpawn.systemHolder[i]);
        }
    }


    //private void checkIfItemIsTher(data_system system)
    //{
    //    //If it is empty then we must add a new Mod header
    //    //After that we add the new file header
    //    if (UI_nodeDataModeHolder.Count == 0)
    //    {
    //        addNewButton_MOD(system);
    //    }
    //    else
    //    {
    //        int badIIndex = -1;
    //        for (int i = 0;i< UI_nodeDataModeHolder.Count;i++)
    //        {
    //            if(UI_nodeDataModeHolder[i].ModName == system.modName)
    //            {
    //                badIIndex = i;
    //                break;
    //            }
    //        }
    //        //Meaning it found a match
    //        if(badIIndex > -1)
    //        {
    //            int badOIndex = -1;
    //            for (int o = 0; o < UI_nodeDataModeHolder[badIIndex].UI_nodeDataFileHolder.Count; o++)
    //            {
    //                if (UI_nodeDataModeHolder[badIIndex].UI_nodeDataFileHolder[o].fileName == system.fileName)
    //                {
    //                    badOIndex = o;
    //                    break;
    //                }
    //            }
    //            //Mod the mod and file header are already there
    //            if (badOIndex > -1)
    //            {
    //                //Debug.Log("ADDED NEW SYSYEM");


    //                addNewButton_SYSTEM(system, UI_nodeDataModeHolder[badIIndex].UI_nodeDataFileHolder[badOIndex]);
    //            }
    //            //The mod header is there just not the file
    //            else
    //            {
    //                //Debug.Log("ADDED NEW File");
    //                addNewButton_FILE(system, UI_nodeDataModeHolder[badIIndex]);
    //            }
    //        }
    //        //It is not there
    //        else
    //        {
    //            addNewButton_MOD(system);
    //        }


    //    }

    //}


    private void checkIfItemIsThere(data_system system)
    {
        //If it is empty then it must be a mod
        if(UI_nodeDataModeHolder.Count == 0)
        {
            addNewButton_MOD(system);
            return;
        }

        //If it is not empty now we check
        int badIIndex = -1;
        for (int i = 0; i < UI_nodeDataModeHolder.Count; i++)
        {
            if (UI_nodeDataModeHolder[i].ModName == system.modName)
            {
                badIIndex = i;
                break;
            }
        }
        //If it can't find a mod match then it will add a new mod
        if(badIIndex <= -1)
        {
            addNewButton_MOD(system);
            return;
        }


        //Now we check to see if it has the same file
        int badOIndex = -1;
        for (int o = 0; o < UI_nodeDataModeHolder[badIIndex].UI_nodeDataFileHolder.Count; o++)
        {
            if (UI_nodeDataModeHolder[badIIndex].UI_nodeDataFileHolder[o].fileName == system.fileName)
            {
                badOIndex = o;
                break;
            }
        }
        //Since it could find it, it will add a new fileHeader
        if(badOIndex <= -1)
        {
            addNewButton_FILE(system, UI_nodeDataModeHolder[badIIndex]);
            return;
        }


        //Now we check to see if it has the same solar system
        int badSystemIndex = -1;
        for (int i = 0; i < UI_nodeDataModeHolder[badIIndex].UI_nodeDataFileHolder[badOIndex].UI_nodeDatasystemHolder.Count; i++)
        {
            if (UI_nodeDataModeHolder[badIIndex].UI_nodeDataFileHolder[badOIndex].UI_nodeDatasystemHolder[i].initName == system.initializerName)
            {
                badSystemIndex = i;
                break;
            }
        }

        if (badSystemIndex <= -1)
        {
            addNewButton_SYSTEM(system, UI_nodeDataModeHolder[badIIndex].UI_nodeDataFileHolder[badOIndex]);
            return;
        }








        ////Mod the mod and file header are already there
        //if (badOIndex > -1)
        //    {
        //        //Debug.Log("ADDED NEW SYSYEM");


        //        addNewButton_SYSTEM(system, UI_nodeDataModeHolder[badIIndex].UI_nodeDataFileHolder[badOIndex]);
        //    }












    }



    public void updateLooks(GameObject coolButton, float ySize, Color32 colorOfButton)
    {
        //Now changing its looks to make it looks better
        RectTransform sad = coolButton.GetComponent<RectTransform>();
        sad.sizeDelta = new Vector3(sad.sizeDelta.x, ySize);
        //Now we change the color to make it CLEAR that it is a mod
        coolButton.GetComponent<Image>().color = colorOfButton;
    }


    private void addNewButton_MOD(data_system system)
    {
        //Added new Mod header
        GameObject newModButton = Instantiate(example_button);
        newModButton.transform.SetParent(button_Holder,false);
        newModButton.SetActive(true);
        UI_nodeDataMod Modtemper = newModButton.AddComponent<UI_nodeDataMod>();
        Modtemper.UI_setNewNodeDataMod(system.modName, newModButton.GetComponentInChildren<TextMeshProUGUI>());
        //Update the button to look more like a header
        updateLooks(newModButton, yModSize, colorMod);
        UI_nodeDataModeHolder.Add(Modtemper);

        addNewButton_FILE(system, Modtemper);
    }
    private void addNewButton_FILE(data_system system, UI_nodeDataMod parentMod)
    {
        //Added new File Header
        GameObject newFileButton = Instantiate(example_button);
        newFileButton.transform.SetParent(button_Holder,false);
        newFileButton.SetActive(true);
        UI_nodeDataFile Filetemper = newFileButton.AddComponent<UI_nodeDataFile>();
        Filetemper.UI_setNewNodeDataFile(system.fileName, newFileButton.GetComponentInChildren<TextMeshProUGUI>());
        //Update the button to look more like a File header
        updateLooks(newFileButton, yFileSize, colorFile);
        //Add the new button to the mod's Children
        parentMod.UI_addNewFileNode(Filetemper);
        Filetemper.cool_parent = parentMod;

         addNewButton_SYSTEM(system, Filetemper);
    }
    private void addNewButton_SYSTEM(data_system system, UI_nodeDataFile parentFile)
    {
        //Add new SolarSystem
        GameObject newSolarSystemButton = Instantiate(example_button);
        newSolarSystemButton.transform.SetParent(button_Holder,false);
        newSolarSystemButton.SetActive(true);
        UI_nodeDataSystem SolarSystemTemper = newSolarSystemButton.AddComponent<UI_nodeDataSystem>();
        SolarSystemTemper.UI_setNewNodeDataSystem(system, newSolarSystemButton.GetComponentInChildren<TextMeshProUGUI>());
        updateLooks(newSolarSystemButton, ySystemSize, colorSystem);
        //Add the new button to the mod's Children
        parentFile.UI_addNewSystemNode(SolarSystemTemper);
        SolarSystemTemper.cool_parent = parentFile;

        //Adds to ID system
        System.Guid ID = place_manager._place_manager.supreamDude.addFella(newSolarSystemButton);
        SolarSystemTemper.ID = ID;


        //This line forces the button toOnClick give its solarSystem to place_manager to place down the system
        UI_parsedButtonClicks megaTemp = newSolarSystemButton.AddComponent<UI_parsedButtonClicks>();
        megaTemp.setData(SolarSystemTemper.getSystemData());


        //Adding ISaveable to it

        SaveableEntity tempRef = newSolarSystemButton.AddComponent<SaveableEntity>();
        tempRef.priority = -10;
        tempRef.spawnType = spawnTypes.UI_init_button;
        tempRef.setID(ID);

        dataSystems.Add(SolarSystemTemper);
    }


    public GameObject findObjectData(object data)
    {
        SaveableEntity.SaveableEntityData newEntityData = (SaveableEntity.SaveableEntityData)data;

        for(int i =0;i<dataSystems.Count;i++)
        {
            ISaveable[] temp = dataSystems[i].gameObject.GetComponents<ISaveable>();
            for (int o = 0; o < temp.Length; o++)
            {
                //Gets the Dictionary key and stores it inside of typeName
                string typeName = temp[o].GetType().ToString();
                //If it is there then store savedState (savedData) in the scirpt with ISaveable
                if (newEntityData.state.TryGetValue(typeName, out object savedState))
                {
                    //Here I gotta do it!
                    place_manager._place_manager.supreamDude.removeFella(dataSystems[i].gameObject);
                    temp[o].loadState(savedState);
                    //Debug.Log("Loaded Data Good");
                    GameObject tempGame = dataSystems[i].gameObject;
                    dataSystems.RemoveAt(i);
                    return tempGame;
                }
            }
        }
        Debug.LogError("Could not find data to connect to " + newEntityData.spawnType + " ID-"+newEntityData.id);
        return null;

      ////  ISaveable[] temp = GetComponents<ISaveable>();
      //  for (int i = 0; i < temp.Length; i++)
      //  {
      //      //Gets the Dictionary key and stores it inside of typeName
      //      string typeName = temp[i].GetType().ToString();
      //      //If it is there then store savedState (savedData) in the scirpt with ISaveable
      //      if (newEntityData.state.TryGetValue(typeName, out object savedState))
      //      {
      //          temp[i].loadState(savedState);
      //      }
      //  }
    }


}




