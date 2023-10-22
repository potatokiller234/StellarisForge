using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;
using System.Threading.Tasks;


public class test_parseStellarisFiles : MonoBehaviour
{
    //Holdes all the parsedMods
    public List<stellarisDataHolder> allFilesOutput;
    //Holds all static vars
    public List<varableNode> scripted_variables;
    //Holds persisant dataPath
    public string my_persistentDataPath = "";




    //Size limit on files trying to parse
    public int fileSizeLimit = 100000;
    // int maxTimes = 10000;
    //Check if there is # then just remove lin
    // 
    public bool isDoneParsing = false;
    public bool isGoingToDebug = false;
    public bool parseVars = false;
    int currentFilePaths = 0;

    //private void Start()
    //{
    //    checkandWriteHardFiles();
    //}

    private void Awake()
    {
        my_persistentDataPath = Application.persistentDataPath;
    }
    public IEnumerator startNewParse_Many(List<parse_pathInformation> inputPaths, string outputFilePath)
    {
        allFilesOutput = new List<stellarisDataHolder>();
        scripted_variables = new List<varableNode>();
        isDoneParsing = false;
        isGoingToDebug = false;




        for (int i = 0;i<inputPaths.Count;i++)
        {
            currentFilePaths++;
            StartCoroutine(ie_startNewParse_Folder(inputPaths[i]));
        }
        yield return new WaitUntil(() => currentFilePaths <= 0);

        #region Debug.Log the result and write result to Output file
        string output = "";
        Debug.Log("Amount in all files" + allFilesOutput.Count);

        
        StreamWriter modCreator = new StreamWriter(outputFilePath + "/debugOutput.txt", false);

        if(isGoingToDebug == true)
        {
            foreach (stellarisDataHolder tempSteam in allFilesOutput)
            {
                //  tempSteam.printOutTree(tempSteam.infoHolder.parent);
                // modCreator.Write(tempSteam.printOutTree(tempSteam.infoHolder.parent));
                output += tempSteam.printOutTree(tempSteam.infoHolder.parent);
            }
            output += "scripted_varibles known as static varibles\n";
            foreach(varableNode varNode in scripted_variables)
            {
                output += varNode.ToString() + "\n";
            }
        }

        modCreator.Write(output);
        modCreator.Close();
        #endregion

        //Done parsing
        isDoneParsing = true;
    }

    IEnumerator ie_startNewParse_Folder(parse_pathInformation singleFolderPath)
    {
        yield return new WaitForEndOfFrame();
        stellarisDataHolder dataHolder;
        int currentCounter = 0;

        //Gets a single mod directory
        //If it exists do somthing
        if (Directory.Exists(singleFolderPath.filePath))
        {
            string nameOfMod = "";
            //If parsed using workshop then we need to parse descriptor to get real name of mod
            if(singleFolderPath.parseType == ParseType.workshop)
            {
                //Gets the name of the Mod
                nameOfMod = parseDescriptor(singleFolderPath.filePath);
                Debug.Log(nameOfMod);
                //int wee = singleFolderPath.filePath.LastIndexOf(@"\");
                //nameOfMod = singleFolderPath.filePath.Substring(wee + 1, singleFolderPath.filePath.Length - wee - 1);
            }
            else if(singleFolderPath.parseType == ParseType.default_stellaris || singleFolderPath.parseType == ParseType.editor_packaged)
            {
                //Gets the name of the Mod
                int wee = singleFolderPath.filePath.LastIndexOf(@"\");
                nameOfMod = singleFolderPath.filePath.Substring(wee + 1, singleFolderPath.filePath.Length - wee - 1);
            }
            else if(singleFolderPath.parseType == ParseType.unknown)
            {
                Debug.LogError("PARSE TYPE IS NULL THIS IS BAD");
            }

            //Setting up the headNode and getting the entire usable filePath
            string fullFilePath = singleFolderPath.filePath + @"\common\solar_system_initializers";
            //Setting up the for parsing scripted_variables (static stellaris vars)
            string staticVarPath = singleFolderPath.filePath + @"\common\scripted_variables";


            string headNodeName = nameOfMod;

            //First check if it even has \common\solar_system_initializers
            if(Directory.Exists(fullFilePath) == false)
            {
                currentFilePaths--;
                yield break;
            }



            #region added Section
            //  List<string> starClasses = new List<string>();
            DirectoryInfo d = new DirectoryInfo(fullFilePath);
            string tempPath = fullFilePath;
            FileInfo[] Files = d.GetFiles("*.txt"); //Getting Text files

            dataHolder = new stellarisDataHolder(headNodeName);
            //Gets all the static vars
            if(parseVars)
                parseScriptedVariables(staticVarPath);

            foreach (FileInfo file in Files)
            {
               // Debug.LogWarning("Files " + file.Name);
                //In so the stack size would not cause a buffer overflow with files larger then 100kb (should make smaller but not now
                if (file.Length > fileSizeLimit)
                {
                    Debug.Log("File " + file.Name + " is Too Long");
                }
                else
                {
                    currentCounter++;
                    Task.Run(() =>
                    {
                        //For each .txt file it will parse it (Don't ask how it does parse it)
                        string fileName = tempPath + @"\" + file.Name;
                        //Debug.Log(fileName);
                        //Debug.Log(fileName);

                        stellarisDataHolder tempDataHolder = new stellarisDataHolder("Penis");
                        getData_oneTxt_Version_Three(tempDataHolder, file.Name, file);
                       // string outPutData = tempDataHolder.printOutTree(tempDataHolder.infoHolder.parent);
                        //Debug.LogWarning(outPutData);


                        if (tempDataHolder.infoHolder.dataHolder != null)
                        {
                            for (int i = 0; i < tempDataHolder.infoHolder.dataHolder.Count; i++)
                            {
                                dataHolder.addChild(dataHolder.infoHolder.parent, tempDataHolder.infoHolder.dataHolder[i]);
                            }
                            //Dont forget to add back the vars
                            for (int i = 0; i < tempDataHolder.variableHolder.Count; i++)
                            {
                                dataHolder.variableHolder.Add(tempDataHolder.variableHolder[i]);
                            }
                        }
                        currentCounter--;
                    });
                    
                }
            }
            #endregion
            yield return new WaitUntil(() => currentCounter == 0);
            allFilesOutput.Add(dataHolder);
        }
        else
        {
            Debug.LogError("Directory " + singleFolderPath.filePath + " does not exist");
        }

        yield return new WaitForEndOfFrame();
        currentFilePaths--;
    }

   

   
    //Parses Descriptor Files to find the name of the mod
    public string parseDescriptor(string filePath)
    {
        //C:\SteamLibrary\steamapps\workshop\content\281990\1150521384\descriptor.mod
        //C:\SteamLibrary\steamapps\workshop\content\281990\683230077
        string nameOfMod = "";
        StreamReader find_nameOfMod;
        try
        {
            find_nameOfMod = new StreamReader(filePath + @"\descriptor.mod");
            Debug.Log(find_nameOfMod);
        }
        catch(System.Exception e)
        {
            Debug.Log(e);
            DEBUG_EntireDebug.DEBUG_entireDebug.printInformation(DEBUG_EntireDebug.DEBUG_entireDebug.isDefault, DEBUG_mode.ID_GEN, this.name);
            int wee = filePath.LastIndexOf(@"\");
            return filePath.Substring(wee + 1, filePath.Length - wee - 1);
        }

        bool done = false;
        while (done == false)
        {
            nameOfMod = find_nameOfMod.ReadLine();
            if (nameOfMod != null && nameOfMod.Contains("name"))
            {
                int startLocation = nameOfMod.IndexOf(@"=");
                nameOfMod = nameOfMod.Substring(startLocation + 2);
                nameOfMod = nameOfMod.Substring(0, nameOfMod.Length - 1);
                done = true;
            }

        }

        return nameOfMod;
    }

    /// <summary>
    /// Parses all vars in a given static_varible path
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    private void parseScriptedVariables(string path)
    {
        //Path example C:\Users\Potato2\AppData\LocalLow\DefaultCompany\StellarisFinalGen\Default Initializers\common\scripted_variables
        //First check if it even has \common\scripted_variables
        if (Directory.Exists(path) == false)
        {
            return;
        }

        DirectoryInfo d = new DirectoryInfo(path);
        FileInfo[] Files = d.GetFiles("*.txt"); //Getting Text files

        for(int i = 0;i<Files.Length;i++)
        {
            //Reads and removes comment of var file
            //string input = parseCommands.readAndRemoveComments(Files[i]);
            //Removes Gunk
            //List<string> trueSplit = parseCommands.removeInputGunk(input,my_persistentDataPath,isGoingToDebug);

            List<string> trueSplit = parseCommands.parseFile(Files[i], my_persistentDataPath, isGoingToDebug);

            //Sets the current index in file we are working with
            int currentIndex = 0;
            while (trueSplit.Count > currentIndex)
            {
                //Debug.Log(trueSplit[currentIndex] + " currentIndex " + currentIndex + " length of split data " + trueSplit.Count);
                if(trueSplit[currentIndex].Contains("@"))
                {
                    scripted_variables.Add(new varableNode(trueSplit[currentIndex], trueSplit[currentIndex + 2]));
                    currentIndex = currentIndex + 3;
                }
                else
                {
                    Debug.LogError("ERROR, |" + trueSplit[currentIndex] + "| is not valid for a var, please check " + Files[i].FullName);
                    currentIndex = trueSplit.Count + 10;
                }
            }
        }
    }
    public List<string> getAllStarTypes()
    {
        return getNameFromFile(@"\common\star_classes");
    }

    

    
 
    public void getData_oneTxt_Version_Three(stellarisDataHolder parentNode, string fileName, FileInfo fileInfo)
    {
        //Used to remove the .txt from the fileName
        fileName = fileName.Replace(".txt", "");
        //Read file and removes comments
        //string entirerFile = parseCommands.readAndRemoveComments(fileInfo);
        //Removes the gunk from the file
        //List<string> trueSplit = parseCommands.removeInputGunk(entirerFile,my_persistentDataPath,isGoingToDebug);

        List<string> trueSplit = parseCommands.parseFile(fileInfo, my_persistentDataPath, isGoingToDebug);


        //Debug stuff
        if (isGoingToDebug)
        {
            try
            {
                string debugOutput = "";
                int numTillNewLine = 10;
                int currentIndex = 0;
                for (int i = 0; i < trueSplit.Count; i++)
                {
                    debugOutput += "|" + trueSplit[i] + "| ";
                    currentIndex++;
                    if (currentIndex >= numTillNewLine)
                    {
                        debugOutput += "\n";
                        currentIndex = 0;
                    }
                }
                StreamWriter debug_output = new StreamWriter(my_persistentDataPath + @"/Debug/" + fileName + "_rawOutput.txt");
                debug_output.Write(debugOutput);
                debug_output.Close();
            }
            catch(System.Exception e)
            {
                Debug.LogError("There was an error with debugging \n " + e);
            }
        }




        int currentIndexInParse = 0;
        //Don't know why but treat this has the headNode
        stellarisNode currentNode = parentNode.infoHolder.parent;     

        while (trueSplit.Count > currentIndexInParse)
        {
            //There is one extra CASE TO WORRY ABOUT that being  flags = { white_hole_system }

            //Equals cases include word = { and word = num
            if (trueSplit[currentIndexInParse].Contains("="))
            {
                //cases word = {
                if (trueSplit[currentIndexInParse + 1].Contains("{"))
                {
                    //After adding the node we know we must go down a level so we set the new parent to be currentNode
                    currentNode = parentNode.addChild(trueSplit[currentIndexInParse - 1], null, fileName, currentNode);
                    currentIndexInParse += 2;
                }
                //We know that the case is now word = num
                else
                {
                    // Debug.Log("Adding Sibling of " + trueSplit[currentIndexInParse + 1] + " it has a length of " + trueSplit[currentIndexInParse + 1].Length);

                    //After adding the node we know we do not go down a level so we don't change the parent
                    parentNode.addChild(trueSplit[currentIndexInParse - 1], trueSplit[currentIndexInParse + 1], fileName, currentNode);
                    currentIndexInParse += 2;
                }
            }
            //Case should only be }
            else if (trueSplit[currentIndexInParse].Contains("}"))
            {
                currentNode = currentNode.parent;
                currentIndexInParse += 1;

            }
            //case of @var = num
            else if (trueSplit[currentIndexInParse].Contains("@"))
            {
                try
                {
                    //Debug.LogError("YOU ADDED A NEW VARIBLE");
                    parentNode.variableHolder.Add(new varableNode(trueSplit[currentIndexInParse], trueSplit[currentIndexInParse + 2], fileName));
                }
                catch (System.Exception e)
                {
                    Debug.LogError("ERROR of " + e + " you are trying to parse for a varible");
                }
                currentIndexInParse += 3;
            }
            //case flags = { white_hole_system black_while feet_system }
            else if (trueSplit[currentIndexInParse].Contains("flags") && trueSplit[currentIndexInParse].Length <= 5)
            {
                currentNode = parentNode.addChild(trueSplit[currentIndexInParse], null, fileName, currentNode);
                currentIndexInParse += 2;

                //Case now white_hole_system black_while feet_system
                while (trueSplit[currentIndexInParse].Contains("}") == false)
                {
                    parentNode.addChild("flag", trueSplit[currentIndexInParse], fileName, currentNode);
                    currentIndexInParse += 1;
                }
            }
            else
            {
                currentIndexInParse++;
                //Debug.LogError("Error with " + trueSplit[currentIndexInParse]);
                //return;
            }
        }

        //List<string> teeth = parentNode.printOutTreeNames(parentNode.infoHolder.parent);

        //string outPut2 = "";

        //foreach (string beb in teeth)
        //{
        //    outPut2 += beb + "\n";
        //}

        //Debug.Log(outPut2);

        //string outPut = "";
        //for (int i = 0; i < trueSplit.Count; i++)
        //{
        //    outPut += trueSplit[i];
        //}
        //Debug.Log(outPut);


       // string outPutData = parentNode.printOutTree(parentNode.infoHolder.parent);

       // Debug.Log("WOOOOOOOOOOOOOOOOOOOOOOOW" + outPutData);
    }














    public List<string> getNameFromFile(string pathFromCommon)
    {
        List<string> starClasses = new List<string>();
        DirectoryInfo d = new DirectoryInfo(idGenerator._idGenerator.getSteamPath() + pathFromCommon);
        string tempPath = idGenerator._idGenerator.getSteamPath() + pathFromCommon;
        FileInfo[] Files = d.GetFiles("*.txt"); //Getting Text files

        foreach (FileInfo file in Files)
        {
            string fileName = tempPath + @"\" + file.Name;
            StreamReader reader = new StreamReader(fileName);

            string nextLine = reader.ReadLine();
            int numOfBrackets = 0;


            while (nextLine != null)
            {
                if (nextLine.Contains("{"))
                {
                    if (numOfBrackets == 0)
                    {
                        starClasses.Add(nextLine);
                    }
                    numOfBrackets++;
                }
                if (nextLine.Contains("}"))
                {
                    numOfBrackets--;
                }
                nextLine = reader.ReadLine();
            }

            reader.Close();

        }
        return starClasses;


    }
}
