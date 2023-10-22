using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI_search : MonoBehaviour
{
    List<UI_nodeDataMod> modsFailSearch;
    List<UI_nodeDataFile> filesFailSearch;
    List<UI_nodeDataSystem> systemFailSearch;

  //  public bool canGo = false;

    private float waitTime = 1f;
    private float currentTime = 0f;

    [SerializeField]
    private TMP_InputField searchBar;

    [Header("Get from _UI_parsedToButtons")]
    public bool getFromMain = true;
    public List<UI_nodeDataMod> thingToDoCool;

    public void Search()
    {
        if (getFromMain == true)
            thingToDoCool = cleanRef._cleanRef._UI_parsedToButtons.UI_nodeDataModeHolder;

        //Get input string
        string inputString = searchBar.text.ToLower();
        //If empty then just resetallButtons to being enabled with an enabled count of 0
        if(inputString.Length == 0)
        {
            resetAllChildren(thingToDoCool);
            return;
        }
        else
        {
            //Else do the search stuff
            resetAllChildren(thingToDoCool);
            //Returns all mods without the input string
            modsFailSearch = searchMods(inputString);
            //Returns all files without the input string
            filesFailSearch = searchFiles(inputString, modsFailSearch);
            //Returns all solarSystems without the input string
            systemFailSearch = searchSolarSystems(inputString, filesFailSearch);
            //Disableds the failed SolarSystems that chain reacts using the UI_nodeDataFile and UI_nodeDataMod OnEnable and Disable to be disabled if all their children
            //Are disabled


            disableBadSolarSystems(systemFailSearch);
            //Done with searhcing
        }
    }
    private void resetAllChildren(List<UI_nodeDataMod> childrenToMakeEnabled)
    {        
        for(int i = 0;i<childrenToMakeEnabled.Count;i++)
        {
            //Now talking for each mod
            childrenToMakeEnabled[i].gameObject.SetActive(true);
            childrenToMakeEnabled[i].resetDisabled();
            foreach (UI_nodeDataFile file in childrenToMakeEnabled[i].UI_nodeDataFileHolder)
            {
                //Now we talking about file
                file.gameObject.SetActive(true);
                file.resetDisabled();
                foreach(UI_nodeDataSystem sys in file.UI_nodeDatasystemHolder)
                {
                   sys.gameObject.SetActive(true);
                }
            }
        }
    }
    private List<UI_nodeDataMod> searchMods(string inputText)
    {
        List<UI_nodeDataMod> badListOfMods = new List<UI_nodeDataMod>();

        for(int i = 0;i< thingToDoCool.Count;i++)
        {           
            if(thingToDoCool[i].ModName.ToLower().IndexOf(inputText) == -1)
            {
                badListOfMods.Add(thingToDoCool[i]);
            }
            else
            {
                //Debug.Log("Found A REALLY GOOD ONE MOD " + Instance_UI_parsedToButtons.UI_nodeDataModeHolder[i].ModName.ToLower());
            }
        }
        return badListOfMods;
    }
    private List<UI_nodeDataFile> searchFiles(string inputText, List<UI_nodeDataMod> thingToSearch)
    {
        List<UI_nodeDataFile> badListOfFiles = new List<UI_nodeDataFile>();

        foreach(UI_nodeDataMod singleMod in thingToSearch)
        {
            for(int i = 0;i<singleMod.UI_nodeDataFileHolder.Count;i++)
            {
                //We are now looking at the files
                if(singleMod.UI_nodeDataFileHolder[i].fileName.ToLower().IndexOf(inputText) == -1)
                {
                    badListOfFiles.Add(singleMod.UI_nodeDataFileHolder[i]);
                }
                else
                {
                    //Debug.Log("Found A REALLY GOOD ONE " + singleMod.UI_nodeDataFileHolder[i].fileName.ToLower());
                }

            }
        }
        return badListOfFiles;
    }

    private List<UI_nodeDataSystem> searchSolarSystems(string inputText, List<UI_nodeDataFile> thingToSearch)
    {
        List<UI_nodeDataSystem> badListOfSolarSystems = new List<UI_nodeDataSystem>();

        foreach(UI_nodeDataFile badFiles in thingToSearch)
        {
            for(int i = 0;i<badFiles.UI_nodeDatasystemHolder.Count;i++)
            {
                bool isBad = false;
                //Now we are looking at systems
                if(badFiles.UI_nodeDatasystemHolder[i].initName.ToLower().IndexOf(inputText) == -1)
                {
                    isBad = true;
                }
                //ADD MORE STUFF HERE TO SEARCH




                //At the end
                if (isBad == true)
                    badListOfSolarSystems.Add(badFiles.UI_nodeDatasystemHolder[i]);
            }
        }
        return badListOfSolarSystems;
    }

    private void disableBadSolarSystems(List<UI_nodeDataSystem> badSystems)
    {
        for(int i = 0;i<badSystems.Count;i++)
        {
            badSystems[i].cool_parent.disableChild(badSystems[i].gameObject);
        }
    }





}
