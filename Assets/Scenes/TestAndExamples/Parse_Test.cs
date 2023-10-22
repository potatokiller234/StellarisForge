using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parse_Test : MonoBehaviour
{
    public parseStellarisFiles stellarisParser;

    [Header("Settings")]
    public bool test_EditorPackaged = false;
    public bool test_stellarisFiles = false;
    public bool test_modFiles = false;
    public bool test_customFiles = false;


    [Header("Directory")]
    private string internalFilePath;
    private string internalOutputFilePath;

    //public string externalFilePath;

    [Header("MaxFileSize")]
    public int internalMaxSize = 100000;
    public int externalMaxSize = 10;

    [Header("Test")]
    public test_parseStellarisFiles bob;
    public LOAD_checkIfHasFiles LOAD_checkFiles;

    private void Awake()
    {
        internalFilePath = Application.dataPath + @"/Scenes/TestAndExamples/ParseToFileDocuments/Input";
        internalOutputFilePath = Application.dataPath + @"/Scenes/TestAndExamples/ParseToFileDocuments/Output/Output.txt";
    }

    private void Start()
    {
        //string bob = "cum";
        //int legnt = bob.Length;

        //bob = bob.Insert(legnt, "3");
        //bob = bob.Insert(legnt, "2");
        //bob = bob.Insert(legnt, "1");

        //legnt = bob.Length;

        //bob = bob.Insert(legnt, "6");
        //bob = bob.Insert(legnt, "5");
        //bob = bob.Insert(legnt, "4");

        //List<string> bob = new List<string>();

        //bob.Add("1");
        //bob.Add("2");
        //bob.Add("3");
        //bob.Add("4");
        //bob.Add("5");
        //bob.Add("6");
        //bob.Add("7");
        //bob.Add("8");

        //string sum = "";

        //for(int i =0;i<bob.Count;i++)
        //{
        //    sum += bob[i];
        //}






        ////  Debug.Log(sum);

        //List<string> bob = new List<string>();
        //List<string> bob2 = new List<string>();

        //bob = stellarisParser.makeSeperate(bob, "min=20");
        //bob2 = stellarisParser.makeSeperate(bob2, "min={}20");

        //string summed = "";
        //for(int i = 0;i<bob.Count;i++)
        //{
        //    summed += bob[i] + " ";
        //}
        //Debug.Log(summed);

        //summed = "";
        //for (int i = 0; i < bob2.Count; i++)
        //{
        //    summed += bob2[i] + " ";
        //}
        //Debug.Log(summed);




        //Debug.Log("For bob");
        //foreach(string teeth in parsed)
        //{
        //    Debug.Log(teeth);
        //}


        //Debug.Log("For bob2");
        //foreach (string teeth in parsed2)
        //{
        //    Debug.Log(teeth);
        //}

        //CURRENT

        //if (isTestingInInternal)
        //{
        //    //Method used to clear all other parse attempts and print it out to output
        //    stellarisParser.fileSizeLimit = internalMaxSize;
        //    stellarisParser.startNewParse(internalFilePath, internalOutputFilePath);
        //}
        //else if (isTestingInExternal)
        //{
        //    stellarisParser.fileSizeLimit = externalMaxSize;
        //    stellarisParser.testSteamMod(externalFilePath, internalOutputFilePath);
        //}
    }





    /// <summary>
    /// Starts the stellaris parsing process
    /// </summary>
    public void startParse()
    {
        //First we must set up the size limit for the input files
        bob.fileSizeLimit = externalMaxSize;

        //Init the dataStruct that will hold our filePathInfos
        List<parse_pathInformation> inputPaths = new List<parse_pathInformation>();

        //Now we add the editor inits
        if(test_EditorPackaged)
            inputPaths.Add(new parse_pathInformation(LOAD_checkFiles.LOAD_checkandWriteHardFiles(), ParseType.editor_packaged));

        //Now we add the given stellaris filess
        if(test_stellarisFiles)
            inputPaths.Add(new parse_pathInformation(idGenerator._idGenerator.getSteamPath(), ParseType.default_stellaris));

        //Now we add the custom files
        if (test_customFiles)
        {
            string[] modFileDirectorys = System.IO.Directory.GetDirectories(Application.persistentDataPath + "/Custom");
            for (int i = 0; i < modFileDirectorys.Length; i++)
            {
                inputPaths.Add(new parse_pathInformation(modFileDirectorys[i], ParseType.workshop));
            }
            Debug.Log("Count of custom files " + modFileDirectorys.Length);
        }

        //Now we must prepare the information for parsing the mods
        //Edit C:\SteamLibrary\steamapps\common\Stellaris into C:\SteamLibrary\steamapps\workshop\content\281990
        if (test_modFiles)
        {
            string newPath = idGenerator._idGenerator.getSteamPath();
            newPath = newPath.Substring(0, newPath.LastIndexOf(@"\"));
            newPath = newPath.Substring(0, newPath.LastIndexOf(@"\")) + @"\workshop\content\281990";
            //Then we get all the folders in C:\SteamLibrary\steamapps\workshop\content\281990 to get their filePath and convery it to a parse_pathInformation
            string[] modFileDirectorys = System.IO.Directory.GetDirectories(newPath);
            for (int i = 0; i < modFileDirectorys.Length; i++)
            {
                inputPaths.Add(new parse_pathInformation(modFileDirectorys[i], ParseType.workshop));
            }
        }
        Debug.Log(inputPaths.Count);
        Debug.Log(inputPaths[0].filePath);
        StartCoroutine(bob.startNewParse_Many(inputPaths, Application.persistentDataPath));
    }
}

public struct parse_pathInformation
{
    public ParseType parseType;
    public string filePath;

    public parse_pathInformation(string p_filePath, ParseType p_parseType)
    {
        filePath = p_filePath;
        parseType = p_parseType;
    }
}

public enum ParseType
{
    //Stellaris files given by game
    default_stellaris,
    //Given by the editor
    editor_packaged,
    //Make on workship
    workshop,
    //For static vars
    scripted_variables,
    //Null
    unknown
}

