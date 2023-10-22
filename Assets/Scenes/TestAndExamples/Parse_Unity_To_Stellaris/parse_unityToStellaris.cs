using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class parse_unityToStellaris : MonoBehaviour
{
    //Holds all spawned in objects such as systemInits and Hyperlanes
    public GameObject placeHolder;
  //  private string outPutPath;

    [Header("Header Info")]
    public string nameOfGalaxy;
    public int priority;
    public int num_empires_min = 0, num_empires_max = 30, num_empires_default = 5;
    public int fallen_empire_min = 0, fallen_empire_max = 30, fallen_empirs_default = 5;
    public int marauder_empire_min = 0, marauder_empire_max = 30, marauder_empire_default = 5;
    public int advanced_empire_min = 0, advanced_empire_max = 30, advanced_empire_default = 1;
    public float colonizable_planet_odds_min = 0, colonizable_planet_odds_max = 30, colonizable_planet_odds_default = 1;
    public float primitive_odds_min = 0, primitive_odds_max = 30, primitive_odds_default = 0.75f;
    public float crisis_strength_min = 0, crisis_strength_max = 30, crisis_strength_default = 1;
    public List<int> extra_crisis_strength;
    public int num_wormhole_pairs_min = 0, num_wormhole_pairs_max = 30, num_wormhole_pairs_default = 1;
    public int num_gateways_min = 0, num_gateways_max = 30, num_gateways_default = 1;



    private string supports_shape = "elliptical";
    private bool random_hyperlanes = false;
    private int core_radius = 0;

    [Header("Grid Scaler")]
    public int gridScaler = 1;

    private void Awake()
    {
       // outPutPath = Application.dataPath + @"/Scenes/TestAndExamples/Parse_Unity_To_Stellaris/output/output.txt";


        nameOfGalaxy = "DefaultGalaxyName";
        priority = -1;
        //All Are Used
        num_empires_min = 0;
        num_empires_max = 30;
        num_empires_default = 5;
        //Only fallen empire default and max is used
        fallen_empire_min = 0;
        fallen_empire_max = 5;
        fallen_empirs_default = 1;
        //Only marauder default used and max is used
        marauder_empire_min = 0;
        marauder_empire_max = 5;
        marauder_empire_default = 1;
        //Only advanced default is used
        advanced_empire_min = 0;
        advanced_empire_max = 30;
        advanced_empire_default = 0;
        //Only default colonizable is used
        colonizable_planet_odds_min = 0;
        colonizable_planet_odds_max = 30;
        colonizable_planet_odds_default = 0.75f;
        //Only default primitive is used
        primitive_odds_min = 0;
        primitive_odds_max = 30;
        primitive_odds_default = 0.75f;
        //Only crisis_strength is used
        crisis_strength_min = 0;
        crisis_strength_max = 30;
        crisis_strength_default = 0.5f;
        //All are used
        extra_crisis_strength = new List<int>();
        extra_crisis_strength.Add(10);
        extra_crisis_strength.Add(25);
        extra_crisis_strength.Add(50);
        extra_crisis_strength.Add(100);
        //All are used
        num_wormhole_pairs_min = 0;
        num_wormhole_pairs_max = 10;
        num_wormhole_pairs_default = 1;
        //All are used
        num_gateways_min = 0;
        num_gateways_max = 10;
        num_gateways_default = 1;

        //Private vars that user can't change
        supports_shape = "elliptical";
        random_hyperlanes = false;
        core_radius = 0;
       // parseToStellaris();
    }
    public string parseToStellaris(string path, string nameOfOutputFile)
    {
        string outPutPath = path + "/" + nameOfOutputFile + ".txt";
        //if(Directory.Exists(outPutPath) == false)
        //{
        //    Directory.CreateDirectory(outPutPath);
        //}

        Debug.Log("Output in " + outPutPath);
        SaveableEntity[] tempToSort = new SaveableEntity[placeHolder.transform.childCount];

        for(int i = 0;i<tempToSort.Length;i++)
        {
            tempToSort[i] = placeHolder.transform.GetChild(i).GetComponent<SaveableEntity>();
        }
        //Sorts em higher priotry runs first
        sort(tempToSort, 0, tempToSort.Length - 1);


        StreamWriter writer = new StreamWriter(outPutPath);
        //Write Header Info
        string tempForWriting = "";
        #region Header Info
        writer.WriteLine("static_galaxy_scenario = {");
        writer.WriteLine("  name = " + "\"" + cleanRef._cleanRef._UI_galaxy_info.stellarisMapTitle.text.Replace(" ","_") + "\"");
        writer.WriteLine("  priority = " + priority);
        writer.WriteLine("  supports_shape = " + supports_shape);
        writer.WriteLine("  num_empires = { min = " + num_empires_min + " max = " + num_empires_max + " }");
        writer.WriteLine("  num_empire_default = " + num_empires_default);
        writer.WriteLine("  fallen_empire_default = " + fallen_empirs_default);
        writer.WriteLine("  fallen_empire_max = " + fallen_empire_max);
        writer.WriteLine("  marauder_empire_default = " + marauder_empire_default);
        writer.WriteLine("  marauder_empire_max = " + marauder_empire_max);
        writer.WriteLine("  advanced_empire_default = " + advanced_empire_default);
        writer.WriteLine("  colonizable_planet_odds = " + colonizable_planet_odds_default);
        writer.WriteLine("  primitive_odds = " + primitive_odds_default);
        writer.WriteLine("  crisis_strength = " + crisis_strength_default);

        for (int i = 0; i < extra_crisis_strength.Count; i++)
        {
            tempForWriting += extra_crisis_strength[i] + " ";
        }
        writer.WriteLine("  extra_crisis_strength = { " + tempForWriting + " }");
        writer.WriteLine("  num_wormhole_pairs = { min = " + num_wormhole_pairs_min + " max = " + num_wormhole_pairs_max + " }");
        writer.WriteLine("  num_wormhole_pairs_default = " + num_wormhole_pairs_default);
        writer.WriteLine("  num_gateways = { min = " + num_gateways_min + " max = " + num_gateways_max + " }");
        writer.WriteLine("  num_gateways_default = " + num_gateways_default);
        writer.WriteLine("  random_hyperlanes = " + boolToString(random_hyperlanes));
        writer.WriteLine("  core_radius = " + core_radius + "\n\n");
        #endregion

        #region Body Info such as SystemInits, Hyperlanes, Ex
        Stack<GameObject> initStack = new Stack<GameObject>();
        Stack<GameObject> hyperlaneStack = new Stack<GameObject>();

        for(int i = 0;i<place_manager._place_manager.grid_placedThings.Count;i++)
        {
            Debug.Log(place_manager._place_manager.grid_placedThings[i].tag);
            switch (place_manager._place_manager.grid_placedThings[i].tag)
            {
                case "gridNode":
                    initStack.Push(place_manager._place_manager.grid_placedThings[i]);
                    break;
                case "hyperlane":
                    hyperlaneStack.Push(place_manager._place_manager.grid_placedThings[i]);
                    break;
                default:
                    Debug.LogWarning("Unknown tag of " + place_manager._place_manager.grid_placedThings[i].tag);
                    break;
            }
        }

        string thingToWrite = "";
        gridData tempGridData;
        int currentID = 0;
        while(initStack.Count != 0)
        {
            Debug.Log(initStack.Count);

            string tempName = "";
            tempGridData = initStack.Pop().GetComponent<gridData>();

            Debug.Log(tempGridData);
           // Debug.Log(tempGridData.systemData);
           // Debug.Log(tempGridData.systemData.initializerName);

            //Do Random
            if (tempGridData.systemData.initializerName == "Random_Initializer")
            {
                thingToWrite = "   system = {"
                    + " id = \"" + tempGridData.ID + "\""
                    + " name = " + "\"\""
                    + " position = { x = " +  convertToStellarisScale(tempGridData.transform.position.x) + " y = " + convertToStellarisScale(tempGridData.transform.position.z) + " }"
                    + "  }";
            }
            else
            {
                if (tempGridData.systemData.name != "randDefault")
                    tempName = tempGridData.systemData.name;

                thingToWrite = "   system = {"
                   + " id = \"" + tempGridData.ID + "\""
                   + " name = \"" + tempName + "\""
                   + " position = { x = " + convertToStellarisScale(tempGridData.transform.position.x) + " y = " + convertToStellarisScale(tempGridData.transform.position.z) + " }"
                   + " initializer = " + tempGridData.systemData.initializerName
                   + "  }";
            }
            writer.WriteLine(thingToWrite);
        }
        #endregion


        #region Hyperlane writing

        while(hyperlaneStack.Count != 0)
        {
            gridHyperlane hyperlaneTempData = hyperlaneStack.Pop().GetComponent<gridHyperlane>();
            thingToWrite = "   add_hyperlane = { from = \"" + hyperlaneTempData.firstNode + "\" to = \"" + hyperlaneTempData.secondNode + "\" }";
            writer.WriteLine(thingToWrite);
        }

        #endregion















        //Write init Info
        //Write Hyerplane info
        writer.WriteLine("\n\n }");
        writer.Close();
        return outPutPath;
    }

    private int convertToStellarisScale(float input)
    {
        input = input / (9f / 5f);
        input = input * -1;
        Debug.Log(input);
        return Mathf.FloorToInt(input);
    }

    public void parseToUnity()
    {
        string nameOfModandProjectName = cleanRef._cleanRef._UI_galaxy_info.currentProjectName.text.Replace(" ", "_");

        if (Directory.Exists(Application.persistentDataPath + "/output") == false)
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/output");
        }

        //Getting the time
        string currentTime = System.DateTime.Now.ToString();
        currentTime = currentTime.Replace(' ', '_');
        currentTime = currentTime.Replace('/', '-');
        currentTime = currentTime.Replace(':', '.');
        //Adding time, project name and galaxy name together
        string nameOfFirstFile = Application.persistentDataPath + "/output/"
            + nameOfModandProjectName + "_"
            + cleanRef._cleanRef._UI_galaxy_info.galaxyName.text + "_"
            + currentTime;

        if (Directory.Exists(nameOfFirstFile))
            Directory.Delete(nameOfFirstFile);
        Directory.CreateDirectory(nameOfFirstFile);

        //File of Name Of Stellaris Map + Time and Date
        //Stellaris Stuff
        parseToSteam(nameOfFirstFile, false);
    }
    public void parseToSteamButton()
    {
        string steamPath =  idGenerator._idGenerator.getDocumentsPath();
        Debug.Log(steamPath);
        parseToSteam(steamPath + "/mod", false);
    }
    private void parseToSteam(string pathToWriteIt, bool startFresh)
    {
        string nameOfModandProjectName = cleanRef._cleanRef._UI_galaxy_info.currentProjectName.text.Replace(" ", "_");


        //Meaning remove all files in that Directory
        string firstLayerDirectory = pathToWriteIt + "/"+ nameOfModandProjectName;
        if (startFresh && Directory.Exists(firstLayerDirectory))
        {
            Directory.Delete(pathToWriteIt);
            File.Delete(pathToWriteIt + "/" + nameOfModandProjectName + ".mod");
        }
        Directory.CreateDirectory(firstLayerDirectory);

        //Make Descitpr
        //Opens a new stream writer and sets up the .mod file that stellaris would auto generate
        string headerName = pathToWriteIt + "/" + nameOfModandProjectName + ".mod";
        StreamWriter modCreator = new StreamWriter(headerName);
        modCreator.WriteLine("version=\"" + cleanRef._cleanRef._UI_galaxy_info.version.text + "\"");
        modCreator.WriteLine("tags={");
        modCreator.WriteLine("	\"" + "GalaxyGeneration" + "\"");
        modCreator.WriteLine("}");

        //TODO for tags support
        //for (int i = 0; i < tags.Count; i++)
        //{
        //    modCreator.WriteLine("	\"" + tags[i] + "\"");
        //}
        //modCreator.WriteLine("}");

        modCreator.WriteLine("name=\"" + cleanRef._cleanRef._UI_galaxy_info.currentProjectName.text.Replace(" ","_") + "\"");
        modCreator.WriteLine("supported_version=\"" + cleanRef._cleanRef._UI_galaxy_info.version.text + "\"");
        modCreator.WriteLine("path=\"" + pathToWriteIt.Replace(@"\",@"/") + @"/" + nameOfModandProjectName + "\"");
        modCreator.Close();

        Debug.Log(firstLayerDirectory + "/descriptor.mod");
        File.Copy(pathToWriteIt + "/" + nameOfModandProjectName + ".mod", firstLayerDirectory + "/descriptor.mod",true);

        //Making the common/solar_system_initializers/custom inits
        //TODO

        //Makeing map/setup_scenarios/stellarisName.txt
        if (Directory.Exists(firstLayerDirectory + "/map") == false)
            Directory.CreateDirectory(firstLayerDirectory + "/map");
        if (Directory.Exists(firstLayerDirectory + "/map/setup_scenarios") == false)
            Directory.CreateDirectory(firstLayerDirectory + "/map/setup_scenarios");
        string finalPath = firstLayerDirectory + "/map/setup_scenarios";
        parseToStellaris(finalPath, cleanRef._cleanRef._UI_galaxy_info.stellarisMapTitle.text.Replace(" ","_"));
    }



    public string boolToString(bool boolToConvert)
    {
        if (boolToConvert == true)
            return "yes";
        else
            return "no";
    }


    #region MERGE SORT
    ////////////////////////////MERGE SORT
    void merge(SaveableEntity[] arr, int l, int m, int r)
    {
        // Find sizes of two
        // subarrays to be merged
        int n1 = m - l + 1;
        int n2 = r - m;

        // Create temp arrays
        SaveableEntity[] L = new SaveableEntity[n1];
        SaveableEntity[] R = new SaveableEntity[n2];
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

        while (i < n1 && j < n2)
        {
            if (L[i].priority >= R[j].priority)
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
    void sort(SaveableEntity[] arr, int l, int r)
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
