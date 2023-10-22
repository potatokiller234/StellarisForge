using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Linq;

public class SaveManager : MonoBehaviour
{
    public List<System.Guid> Ids;

    private saveList iHateThis;

    [Header("Prefabs")]
    public GameObject deafult_cube;
    public GameObject deafult_sphere;
    public GameObject deafult_hyperlane;

    public static SaveManager _SaveManager;
    [Header("Ref to other files")]
    public solarSystemSpawn SolarSystemSpawn;
    public UI_parsedToButtons ref_UI_parsedToButtons;

    private void Awake()
    {
        _SaveManager = this;
    }
    private void Start()
    {
        DEBUG_EntireDebug.DEBUG_entireDebug.printGenericInformation(DEBUG_EntireDebug.DEBUG_entireDebug.isGeneralInfo, "DataPath-" + Application.persistentDataPath);
    }


    [ContextMenu("Save")]
    public void save()
    {
        //This line is not used due to the fact that saveManager does not support removing key values
        //If left it would fill the Dictionary with dead values that would take up time and do nothing
        //Dictionary<int, object> state = loadFile();
        Dictionary<System.Guid, object> state = new Dictionary<System.Guid, object>();
        saveState(state);
        saveFile(state);
       
    }
    [ContextMenu("Load")]
    public void load(string filePathFileName)
    {
        Dictionary<System.Guid, object> state = loadFile(filePathFileName);
        //loadState(state);
        stellaris_loadState(state);
        cleanRef._cleanRef._UI_buttonsToStatic.moveToStatic();
    }
    //Saves file of data given by state
    public void saveFile(object state)
    {
        //Directory
        string path_ProjectDirectory = Application.persistentDataPath + "/Saves/" + cleanRef._cleanRef._UI_galaxy_info.currentProjectName.text;
        //Project filePath
        string path_ProjectFilePath = path_ProjectDirectory + "/" + cleanRef._cleanRef._UI_galaxy_info.currentProjectName.text + ".lemons";
        //Galaxy filePath
        string path_GalaxyFilePath = path_ProjectDirectory + "/" + cleanRef._cleanRef._UI_galaxy_info.galaxyName.text + ".lemon";

        #region Saving/Loading the Project File (Both new and old project files)
        //If project file does not exisit then make a new saveDirectory and create a new stellarisProject Info
        stellarisProject temp_projectRef;
        if (File.Exists(path_ProjectFilePath) == false)
        {
            Directory.CreateDirectory(path_ProjectDirectory);
            temp_projectRef = new stellarisProject();

            temp_projectRef.projectName = cleanRef._cleanRef._UI_galaxy_info.currentProjectName.text;
            temp_projectRef.lastEdited = System.DateTime.Now.ToString("MMMM dddd d HH:mm yyyy");
            temp_projectRef.addGalaxy(-1, temp_projectRef.lastEdited,
                cleanRef._cleanRef._UI_galaxy_info.galaxyName.text,
                cleanRef._cleanRef._UI_galaxy_info.stellarisMapTitle.text,
                cleanRef._cleanRef._UI_galaxy_info.version.text);
        }
        //The ProjectFile Exisits
        else
        {
            //Load Project
            using (FileStream stream = File.Open(path_ProjectFilePath, FileMode.Open))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                temp_projectRef = (stellarisProject)formatter.Deserialize(stream);
                stream.Close();
            }
            //Update project info
            temp_projectRef.updateGalaxyData(-1,
                System.DateTime.Now.ToString("MMMM dddd d HH:mm yyyy"),
                cleanRef._cleanRef._UI_galaxy_info.galaxyName.text,
                cleanRef._cleanRef._UI_galaxy_info.stellarisMapTitle.text,
                cleanRef._cleanRef._UI_galaxy_info.version.text);
        }
        //Save project info
        using (FileStream stream = File.Open(path_ProjectFilePath, FileMode.Create))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(stream, temp_projectRef);
            stream.Close();
        }
        //Done with project stuff
        #endregion

        #region Saving Galaxy Project File (Both new and old Galaxy Files)
        using (FileStream stream = File.Open(path_GalaxyFilePath, FileMode.Create))
        {
            BinaryFormatter formatter = new BinaryFormatter();

            iHateThis = new saveList();
            iHateThis.Id = new List<System.Guid>(place_manager._place_manager.supreamDude.getSmallFellaID());
            Ids = iHateThis.Id;
            Debug.Log(Ids.Count + " Count OF FELLAS\n" 
                + place_manager._place_manager.supreamDude.getSmallFellaID().Count + " Fellas in placeManager\n" 
                + iHateThis.Id.Count + " Fellas in IhateThis");
            iHateThis.saveData = state;
            iHateThis.save_systemHolder = SolarSystemSpawn.systemHolder;
            formatter.Serialize(stream, iHateThis);
            //Fix editor error
            stream.Close();
        }
        #endregion
    }

    //Returns data that was in file
    Dictionary<System.Guid, object> loadFile(string savePath)
    {
        if(File.Exists(savePath) == false)
        {
            Debug.LogError("Save file not found in path " + savePath);
            return new Dictionary<System.Guid, object>();
        }

        using(FileStream stream = File.Open(savePath, FileMode.Open))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            iHateThis = (saveList)formatter.Deserialize(stream);
            Ids =  iHateThis.Id;
            SolarSystemSpawn.systemHolder = iHateThis.save_systemHolder;
            Debug.Log(iHateThis.Id.Count + " loaded this many Ids");
            Debug.Log(place_manager._place_manager.supreamDude.getCount() + " currenty have this many loaded Ids");
            ref_UI_parsedToButtons.spawnInButtons();
            stream.Close();
            return (Dictionary<System.Guid, object>)iHateThis.saveData;
        }
    }


    //Saves the dictonary from saveableEntity in a new dictonary
    void saveState(Dictionary<System.Guid, object> state)
    {
        SaveableEntity[] dataToSave = FindObjectsOfType<SaveableEntity>();
        Debug.Log("Trying to save-" + dataToSave.Length + " amount");
        for(int i = 0;i<dataToSave.Length;i++)
        {
            state[dataToSave[i].Id] = dataToSave[i].saveState();
        }
    }

    //Loads the dictonary from state into saveableEntitys dictonary
    void loadState(Dictionary<System.Guid, object> state)
    {
        SaveableEntity[] dataToLoad = FindObjectsOfType<SaveableEntity>();

        for (int i = 0; i < dataToLoad.Length; i++)
        {
            if(state.TryGetValue(dataToLoad[i].Id,out object saveState))
            {
                dataToLoad[i].loadState(saveState);
            }
        }

    }



    //Stellaris Cool
    void stellaris_loadState(Dictionary<System.Guid, object> state)
    {
        //SaveableEntity[] dataToLoad = FindObjectsOfType<SaveableEntity>();

        //for(int i = 0;i<dataToLoad.Length;i++)
        //{
        //    if(dataToLoad[i].ignore == false)
        //        Ids.Add(dataToLoad[i].Id);
        //}



        List<stringObject> tempHolder = new List<stringObject>();

        //Add IDS to list so we can sort it later
        stringObject temp;
        Debug.Log("Amount Of IDS-"+Ids.Count);
        for(int i = 0;i<Ids.Count;i++)
        {
            if(state.TryGetValue(Ids[i], out object saveState))
            {
                temp = new stringObject();
                temp.String = Ids[i];
                temp.Object = saveState;
                tempHolder.Add(temp);
            }
        }

        Debug.Log(tempHolder.Count);
        //Sorts it so small numbers (higher priority) priority is at the start while larger (lower priority) is at the back  
        sort(tempHolder, 0, tempHolder.Count - 1);

        SaveableEntity.SaveableEntityData tempStringObject;
        for(int i = 0;i<tempHolder.Count;i++)
        {
            tempStringObject = (SaveableEntity.SaveableEntityData)tempHolder[i].Object;


            GameObject tempObject = null;
            switch (tempStringObject.spawnType)
            {
                case spawnTypes.cube:
                    #region Cube
                    tempObject = Instantiate(deafult_cube);
                    #endregion
                    break;
                case spawnTypes.sphere:
                    SaveableEntity.SaveableEntityData newEntityData = (SaveableEntity.SaveableEntityData)tempStringObject;
                    //Debug.Log("Trying to find ID-" + newEntityData.id);
                    #region Sphere
                    tempObject = Instantiate(deafult_sphere);
                    place_manager._place_manager.grid_placedThings.Add(tempObject);
                    #endregion
                    break;
                case spawnTypes.hyperlane:
                    #region Hyperlane
                    tempObject = Instantiate(deafult_hyperlane);
                    #endregion
                    place_manager._place_manager.grid_placedThings.Add(tempObject);
                    break;
                case spawnTypes.UI_init_button:
                    #region Hyperlane
                    tempObject = ref_UI_parsedToButtons.findObjectData(tempStringObject);
                    if (tempObject != null)
                    {
                        place_manager._place_manager.supreamDude.addOldFella(tempObject, tempHolder[i].String);
                        tempObject.GetComponent<SaveableEntity>().setID(tempHolder[i].String);
                    }
                    #endregion
                    break;
                default:
                    Debug.LogError("Unknown dataType " + tempStringObject.spawnType + "\n Add a new case here");
                    break;
            }
            //If sphere, hyperlane, or cube
            if(tempObject != null &&(
                tempStringObject.spawnType == spawnTypes.cube || 
                tempStringObject.spawnType == spawnTypes.sphere || 
                tempStringObject.spawnType == spawnTypes.hyperlane))
            {
                tempObject.transform.SetParent(place_manager._place_manager.parentHolder);
                SaveableEntity saveTemp = tempObject.GetComponent<SaveableEntity>();
                saveTemp.ignore = false;
                saveTemp.loadState(tempHolder[i].Object);
                place_manager._place_manager.supreamDude.addOldFella(tempObject, tempHolder[i].String);
            }
            //else if(tempObject != null &&(tempStringObject.spawnType == spawnTypes.UI_init_button))
            //{

            //}


            #region OLD
            //switch (tempStringObject.spawnType)
            //{
            //    case spawnTypes.cube:
            //        #region Cube
            //        GameObject tempCube = Instantiate(deafult_cube);
            //        SaveableEntity saveTemp_cube = tempCube.GetComponent<SaveableEntity>();
            //        saveTemp_cube.ignore = false;
            //        saveTemp_cube.loadState(tempHolder[i].Object);
            //        place_manager._place_manager.supreamDude.addOldFella(tempCube, tempHolder[i].String);
            //        #endregion
            //        break;
            //    case spawnTypes.sphere:
            //        #region Sphere
            //        GameObject tempSphere = Instantiate(deafult_sphere);
            //        SaveableEntity saveTemp_sphere = tempSphere.GetComponent<SaveableEntity>();
            //        saveTemp_sphere.ignore = false;
            //        saveTemp_sphere.loadState(tempHolder[i].Object);
            //        place_manager._place_manager.supreamDude.addOldFella(tempSphere, tempHolder[i].String);
            //        #endregion
            //        break;
            //    case spawnTypes.hyperlane:
            //        GameObject tempHyperlane = Instantiate(deafult_hyperlane);
            //        SaveableEntity saveTemp_hyperlane = tempHyperlane.GetComponent<SaveableEntity>();



            //        break;
            //    default:
            //        Debug.LogError("Unknown dataType " + tempStringObject.spawnType + "\n Add a new case here");
            //        break;
            //}
            #endregion
        }
       // Ids = place_manager._place_manager.supreamDude.getSmallFellaID();


        Debug.Log("Done loading");

    }
    public struct stringObject
    {
        public System.Guid String;
        public object Object;
    }

    [System.Serializable]
    private struct saveList
    {
        public List<System.Guid> Id;
        public object saveData;
        public List<data_system> save_systemHolder;
    }
    #region MERGE SORT
    ////////////////////////////MERGE SORT
    void merge(List<stringObject> arr, int l, int m, int r)
    {
        // Find sizes of two
        // subarrays to be merged
        int n1 = m - l + 1;
        int n2 = r - m;

        // Create temp arrays
        stringObject[] L = new stringObject[n1];
        stringObject[] R = new stringObject[n2];
        int i, j;

        // Copy data to temp arrays
        for (i = 0; i < n1; ++i)
            L[i] = arr[l + i];
        for (j = 0; j < n2; ++j)
            R[j] = arr[m + 1 + j];

        // Merge the temp arrays

        // Initial indexes of first
        // and second subarrays
        i = 0;
        j = 0;

        // Initial index of merged
        // subarray array
        int k = l;

        SaveableEntity.SaveableEntityData temp1;
        SaveableEntity.SaveableEntityData temp2;

        while (i < n1 && j < n2)
        {
            temp1 = (SaveableEntity.SaveableEntityData)L[i].Object;
            temp2 = (SaveableEntity.SaveableEntityData)R[j].Object;

            if (temp1.priority <= temp2.priority)
            {
                arr[k] = L[i];
                i++;
            }
            else
            {
                arr[k] = R[j];
                j++;
            }
            k++;
        }

        // Copy remaining elements
        // of L[] if any
        while (i < n1)
        {
            arr[k] = L[i];
            i++;
            k++;
        }

        // Copy remaining elements
        // of R[] if any
        while (j < n2)
        {
            arr[k] = R[j];
            j++;
            k++;
        }
    }

    // Main function that
    // sorts arr[l..r] using
    // merge()
    void sort(List<stringObject> arr, int l, int r)
    {
        if (l < r)
        {
            // Find the middle
            // point
            int m = l + (r - l) / 2;

            // Sort first and
            // second halves
            sort(arr, l, m);
            sort(arr, m + 1, r);

            // Merge the sorted halves
            merge(arr, l, m, r);
        }
    }
    #endregion


}

public enum spawnTypes
{
    cube,
    sphere,
    hyperlane,
    UI_init_button
}

