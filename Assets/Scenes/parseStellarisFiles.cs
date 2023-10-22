using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;

public class parseStellarisFiles : MonoBehaviour
{
    public string pathOfStellaris;
    public stellarisDataHolder dataHolder;
    public List<stellarisDataHolder> allFilesOutput;
    //Size limit on files trying to parse
    public int fileSizeLimit = 100000;
    // int maxTimes = 10000;
    //Check if there is # then just remove lin
    // 
    public bool isDoneParsing = false;
    private void Start()
    {
        //allFilesOutput = new List<stellarisDataHolder>();
        ////START---------------------------------------------------
        ////This code block reads all text files in a file path, stuffs it in a tree structure, and outputs it to Output on desktop
        //string filePath = @"C:\Users\Potato2\Desktop\Cuco";
        //getDataFromFile(filePath, "star_classes");
        ////Debug.Log(dataHolder.infoHolder.name + " name");

        //string output = dataHolder.printOutTree(dataHolder.infoHolder.parent);
        //Debug.Log(output);

        //string folderPath = @"C:\Users\Potato2\Desktop\Output\Output.txt";
        //StreamWriter modCreator = new StreamWriter(folderPath, false);
        //modCreator.Write(output);
        //modCreator.Close();
        ////END----------------------------------------------------


        //////Test_stellarisParse();
        ////testSteamMod();

    }






    public void startNewParse(string inputFilePath, string outputFilePath)
    {
        string[] cheeseMan = Directory.GetDirectories(inputFilePath, "*", SearchOption.TopDirectoryOnly);
        allFilesOutput = new List<stellarisDataHolder>();

        foreach (string teeth in cheeseMan)
        {
            Debug.Log(teeth);
        }

        //Only parsed one folder not all folders in input
        getDataFromFile(inputFilePath, "star_classes");
        allFilesOutput.Add(dataHolder);



        string output = allFilesOutput[0].printOutTree(dataHolder.infoHolder.parent);
        Debug.Log(output);
 
        StreamWriter modCreator = new StreamWriter(outputFilePath, false);
        modCreator.Write(output);
        modCreator.Close();

        //Done parsing
        isDoneParsing = true;
    }

    public void startNewParse_Files(string inputFilePath, string outputFilePath)
    {
        string[] cheeseMan = Directory.GetDirectories(inputFilePath, "*", SearchOption.TopDirectoryOnly);
        allFilesOutput = new List<stellarisDataHolder>();

        string output = "All Folders Found in Internal Input \n";
        foreach (string teeth in cheeseMan)
        {
            output += teeth + "\n";
        }
        Debug.Log(output);

        #region Parsing Input folder (does not parse folders inside of Input)
        //This has to be hardcoded, it is just parsing all .txt files in folder Input. If there are any other folders in input it gets ignored
        stellarisDataHolder temp_checkForNull = parseFolder(inputFilePath, "defaultBob", false);
        if (temp_checkForNull != null && temp_checkForNull.infoHolder.dataHolder != null)
        {
            allFilesOutput.Add(temp_checkForNull);
        }
        #endregion

        #region Parsing Input Folder's Folders (So any folder in Input folder will get parsed)
        for (int i = 0; i < cheeseMan.Length; i++)
        {
            //Gets a single mod directory
            string singleFilePath = cheeseMan[i];
            //If it exists do somthing
            if (Directory.Exists(singleFilePath))
            {
                string nameOfMod = Path.GetFileName(Path.GetDirectoryName(singleFilePath));
                Debug.Log(singleFilePath + " before parse " + i);
                temp_checkForNull = parseFolder(singleFilePath, nameOfMod, false);

                if (temp_checkForNull != null && temp_checkForNull.infoHolder.dataHolder != null)
                {
                    allFilesOutput.Add(temp_checkForNull);
                }
            }
        }
        #endregion

        #region Debug.Log the result and write result to Output file
        output = "";
        foreach (stellarisDataHolder tempSteam in allFilesOutput)
        {
            output += tempSteam.printOutTree(tempSteam.infoHolder.parent);
        }

        //Debug.LogWarning("Output for thing " + output);

        StreamWriter modCreator = new StreamWriter(outputFilePath, false);
        modCreator.Write(output);
        modCreator.Close();
        #endregion

        //Done parsing
        isDoneParsing = true;
    }

    //public void Test_stellarisParse()
    //{
    //    //Gets the filePath inside of Resources for testing Output
    //    //C:\Users\Potato2\Desktop\UnityGames\Stellaris\StellarisFinalGen\Assets\Scenes\TestAndExamples\ParseToFileDocuments
    //    string filePathInput = Application.dataPath + @"/Scenes/TestAndExamples/ParseToFileDocuments/Cuco";
    //    //Gets the data from the file and names headNode start_classes
    //    getDataFromFile(filePathInput, "star_classes");
    //    //Prints out the tree strcuture of it
    //    string output = dataHolder.printOutTree(dataHolder.infoHolder.parent);
    //    Debug.Log(output);

    //    //Gets the file path for the output
    //    //C:\Users\Potato2\Desktop\UnityGames\Stellaris\StellarisFinalGen\Assets\Scenes\TestAndExamples\ParseToFileDocuments\Output
    //    string folderPath = Application.dataPath + @"/Scenes/TestAndExamples/ParseToFileDocuments/Output/Output.txt";
    //    //Then writes it into a file if unity debug cuts it off

    //    StreamWriter modCreator = new StreamWriter(folderPath, false);
    //    modCreator.Write(output);
    //    modCreator.Close();
    //}


    /// <summary>
    /// Takes a filePath, parses it, then stores the parsed data in the dataHolder
    /// </summary>
    /// <param name="filePathInput">Folder path for file to read</param>
    /// <param name="nameOfHeadNode"></param>
    public stellarisDataHolder parseFolder(string filePathInput, string nameOfHeadNode, bool isDebug)//If can later change this so that it edits it own headNode, not a global one
    {
        dataHolder = null;
        string filePath = filePathInput;
        //Parses all the files in folder
        getDataFromFile(filePath, nameOfHeadNode);
        string output = dataHolder.printOutTree(dataHolder.infoHolder.parent);
        Debug.Log(isDebug + " is Debug");
        if(isDebug)
        {
            Debug.Log(output + " this is the output");
        }
        return dataHolder;
    }



    public void testSteamMod(string steamInputFilePath, string steamOutputFilePath)
    {
        string steamLink = steamInputFilePath;
        //C:\SteamLibrary\steamapps\workshop\content\281990
        string[] bob = steamLink.Split('\\');
        string modLink = "";
        allFilesOutput = new List<stellarisDataHolder>();

        bool stop = false;
        int place = 0;
        while(stop == false)
        {
            Debug.Log(bob.Length);
            if(bob[place] == "steamapps")
            {
                stop = true;
            }
            modLink += bob[place] + @"\";
            place++;
        }
        modLink += @"workshop\content\281990";

        //SteamLink filepath should be good
        //modLink for the stellaris filePath should be good now


        //C:\SteamLibrary\steamapps\workshop\content\281990\683230077\common\solar_system_initializers
        //C:\SteamLibrary\steamapps\workshop\content\281990\683230077
        //This is used to get all mod file paths
        string[] cheeseMan = Directory.GetDirectories(modLink, "*", SearchOption.TopDirectoryOnly);

        for(int i =0;i<cheeseMan.Length;i++)
        {
            //Gets a single mod directory
            string singleModFilePath = cheeseMan[i] + @"\common\solar_system_initializers";
            //If it exists do somthing
            if (Directory.Exists(singleModFilePath))
            {
                string nameOfMod = parseDescriptor(cheeseMan[i]);
                Debug.Log(singleModFilePath + " before parse " + i);
                stellarisDataHolder temp_checkForNull = parseFolder(singleModFilePath, nameOfMod, false);

                if(temp_checkForNull != null && temp_checkForNull.infoHolder.dataHolder != null)
                {
                    allFilesOutput.Add(temp_checkForNull);
                }
             //   parseFolder(singleModFilePath,,)
             //   string[] eggLo = Directory.GetFiles(singleModFilePath, "*.txt");
             //   Debug.Log(eggLo[0] + "  " + i);

             ////   Debug.Log("Its a good fella " + i);
            }
        }

        string output = "";
        foreach(stellarisDataHolder tempSteam in allFilesOutput)
        {
            output += tempSteam.printOutTree(tempSteam.infoHolder.parent);
        }

        StreamWriter modCreator = new StreamWriter(steamOutputFilePath, false);
        modCreator.Write(output);
        modCreator.Close();

        //C:\SteamLibrary\steamapps\workshop\content\281990\683230077\common\solar_system_initializers


        isDoneParsing = true;

    }


    //Parses Descriptor Files to find the name of the mod
    public string parseDescriptor(string filePath)
    {
        string nameOfMod = "";
        StreamReader find_nameOfMod = new StreamReader(filePath + @"\descriptor.mod");

        bool done = false;
        while(done == false)
        {
            nameOfMod = find_nameOfMod.ReadLine();
            if(nameOfMod != null && nameOfMod.Contains("name"))
            {
                int startLocation = nameOfMod.IndexOf(@"=");
                nameOfMod = nameOfMod.Substring(startLocation + 2);
                nameOfMod = nameOfMod.Substring(0,nameOfMod.Length - 1);
                done = true;
            }

        }

        return nameOfMod;
    }
    public List<string> getAllStarTypes()
    {
        return getNameFromFile(@"\common\star_classes");
    }
    //public void getDataFromFile(string pathFromCommon)
    //{
    //    //List<string> starClasses = new List<string>();
    //    //DirectoryInfo d = new DirectoryInfo(pathOfStellaris + pathFromCommon);
    //    //Debug.Log(pathOfStellaris + pathFromCommon);
    //    //string tempPath = pathOfStellaris + pathFromCommon;
    //    //FileInfo[] Files = d.GetFiles("*.txt"); //Getting Text files

    //    List<string> starClasses = new List<string>();
    //    DirectoryInfo d = new DirectoryInfo(pathFromCommon);
    //    Debug.Log(pathFromCommon);
    //    string tempPath = pathFromCommon;
    //    FileInfo[] Files = d.GetFiles("*.txt"); //Getting Text files



    //    foreach (FileInfo file in Files)
    //    {
    //        string fileName = tempPath + @"\" + file.Name;
    //        StreamReader reader = new StreamReader(fileName);

    //        string nextLine = reader.ReadLine();
    //        int numOfBrackets = 0;

    //        string[] temp;
    //        while (nextLine != null)
    //        {
    //            //Called only at the start
    //            if (nextLine.Contains("{") && dataHolder == null)
    //            {
    //                //Call at the start to make the headNode
    //                temp = nextLine.Split(' ');
    //                dataHolder = new stellarisDataHolder(temp[0]);
    //            }
    //            else if(nextLine.Contains("{") == false && dataHolder != null)
    //            {
    //                temp = nextLine.Split(' ');

    //                List<string> secondTemp = new List<string>();

    //                for(int i =0;i<temp.Length;i++)
    //                {
    //                    if(string.IsNullOrWhiteSpace(temp[i]) == false)
    //                    {
    //                        secondTemp.Add(temp[i]);
    //                    }
    //                }

    //                temp = secondTemp.ToArray();
                    



    //                Debug.Log(dataHolder.infoHolder.parent);
    //                dataHolder.addChild(temp[0], temp[temp.Length - 1], dataHolder.infoHolder.parent);
    //                foreach(string teeth in temp)
    //                {
    //                    Debug.Log(teeth);
    //                }
    //            }
    //            else if (nextLine.Contains("{") && dataHolder != null)
    //            {
    //                temp = nextLine.Split(' ');
    //                //Adds the data to the dataHolder
    //                dataHolder.addChild(temp[0], null, dataHolder.infoHolder.parent);
    //                //Gets a refernce of that newly created node
    //                stellarisNode tempParent = dataHolder.findData(temp[0], null, dataHolder.infoHolder.parent);

    //                int posOfLeftBracket = 0;

    //                for(int i = 0;i<temp.Length;i++)
    //                {
    //                    if(temp[i] == "{")
    //                    {
    //                        posOfLeftBracket = i;
    //                    }
    //                }
    //                //Add one since we don't need to rest
    //                posOfLeftBracket++;

    //                bool done = false;

    //                while(done == false)
    //                {
    //                    dataHolder.addChild(temp[posOfLeftBracket], temp[posOfLeftBracket + 2], tempParent);


    //                    if (posOfLeftBracket + 4 >= temp.Length)
    //                    {
    //                        done = true;
    //                    }
    //                    else
    //                    {
    //                        posOfLeftBracket += 3;
    //                    }
    //                }
    //                //if (numOfBrackets == 0)
    //                //{
    //                //    starClasses.Add(nextLine);
    //                //}
    //                //numOfBrackets++;
    //            }
    //            //if (nextLine.Contains("}"))
    //            //{
    //            //    numOfBrackets--;
    //            //}
    //            nextLine = reader.ReadLine();
    //        }

    //        myStaticScripts.staticScripts.removeDuplicates(starClasses);
    //        reader.Close();
    //    }
    //}

    //Gets all data from files by going in the directory and getting all of the .txt
    public void getDataFromFile(string fullFilePath, string headNodeName)
    {
       // List<string> starClasses = new List<string>();
        DirectoryInfo d = new DirectoryInfo(fullFilePath);
        string tempPath = fullFilePath;
        FileInfo[] Files = d.GetFiles("*.txt"); //Getting Text files

        dataHolder = new stellarisDataHolder(headNodeName);

        foreach(FileInfo file in Files)
        {
            //Debug.LogWarning("Files " + file.Name);
            //In so the stack size would not cause a buffer overflow with files larger then 100kb (should make smaller but not now
            if(file.Length > fileSizeLimit)
            {
                Debug.Log("File " + file.Name + " is Too Long");
            }
            else
            {
                //For each .txt file it will parse it (Don't ask how it does parse it)
                string fileName = tempPath + @"\" + file.Name;
                //Debug.Log(fileName);
                nonTaskBasedGetData(fileName, file);
            }
        }


    }

    public void taskBasedGetData(string fileName, FileInfo file)
    {
        StreamReader reader = new StreamReader(fileName);

        stellarisDataHolder tempDataHolder = new stellarisDataHolder("Penis");
        getData_oneTxt_Version_Three(reader, tempDataHolder, file.Name);
        string outPutData = tempDataHolder.printOutTree(tempDataHolder.infoHolder.parent);
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
        reader.Close();
    }
    public void nonTaskBasedGetData(string fileName, FileInfo file)
    {
        //Debug.Log(fileName);
        StreamReader reader = new StreamReader(fileName);

        stellarisDataHolder tempDataHolder = new stellarisDataHolder("Penis");
        getData_oneTxt_Version_Three(reader, tempDataHolder, file.Name);
        string outPutData = tempDataHolder.printOutTree(tempDataHolder.infoHolder.parent);
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
        reader.Close();
    }


    ////Parses the text file into a tree strucutre (Don't ask how), Just know that the three strcutre does look like a tree
    //public void getDataFromOneTxt(StreamReader reader, string currentString, stellarisNode parentNode, bool readNextLine)
    //{
    //    string nextLine = "";
    //    if (readNextLine == false)
    //    {
    //        nextLine = reader.ReadLine();
    //        currentString += nextLine;
    //        Debug.Log(currentString);
    //    }
    //    if (nextLine == null)
    //    {
    //        return;
    //    }

    //    //Now I can parse it
    //    string[] parsedData;
    //    //#########################################
    //    if (currentString.Contains("#"))
    //    {
    //        //  Debug.Log("# called");
    //        parsedData = myStaticScripts.staticScripts.parseString(currentString);
    //        currentString = myStaticScripts.staticScripts.parsedToCurrent(parsedData, 0);
    //    }
    //    //@distance = 50
    //    else if (currentString.Contains("@"))
    //    {
    //        //   Debug.Log("@ called");

    //        parsedData = myStaticScripts.staticScripts.parseString(currentString);
    //        currentString = myStaticScripts.staticScripts.parsedToCurrent(parsedData, 0);
    //    }
    //    //	planet			= { key = pc_b_star }
    //    if (currentString.Contains("{") && currentString.Contains("}"))
    //    {
    //        //Debug.Log("Called");
    //        //Debug.Log(nextLine);


    //        parsedData = myStaticScripts.staticScripts.parseString(currentString);

    //        //   Debug.Log("BothCalled " + currentString + "\n" + myStaticScripts.staticScripts.stringReturn(parsedData));



    //        int eqDist = myStaticScripts.staticScripts.findString(parsedData, "=");
    //        int bracakDist = myStaticScripts.staticScripts.findString(parsedData, "{");

    //        while (parsedData[eqDist + 1] != "{")
    //        {
    //            dataHolder.addChild(parsedData[eqDist - 1], parsedData[eqDist + 1], parentNode);
    //            parsedData[eqDist] = "teeth";
    //            eqDist = myStaticScripts.staticScripts.findString(parsedData, "=");



    //            //if(parsedData[eqDist + 1] == "{")
    //            //{
    //            //   Debug.Log("BothLeftCalled \n" + myStaticScripts.staticScripts.stringReturn(parsedData) + "\n" + parsedData[eqDist - 1] + "\n" + parentNode.name);

    //            //    stellarisNode tempLeftBrack = dataHolder.addChild(parsedData[eqDist - 1], null, parentNode);
    //            //    //// Debug.Log(currentString);
    //            //   // Debug.Log(currentString);
    //            //    //currentString = myStaticScripts.staticScripts.parsedToCurrent(parsedData, eqDist + 2);
    //            //  //  Debug.Log(currentString);
    //            //    eqDist = -1;
    //            //    // Debug.Log(currentString);
    //            //    currentString = "";
    //            //}
    //            //else if(parsedData[eqDist + 1] == "}")
    //            //{
    //            //  //  Debug.Log("BothMiddleCalled");

    //            //   Debug.LogError("This should never run");
    //            //    foreach(string computer in parsedData)
    //            //    {
    //            //        Debug.Log(computer);
    //            //    }
    //            //}
    //            //else
    //            //{
    //            //    //Good
    //            //    //Debug.Log("BothRightCalled \n" + myStaticScripts.staticScripts.stringReturn(parsedData) + "\n" + parsedData[eqDist - 1] + "\n" + parentNode.name);

    //            //    dataHolder.addChild(parsedData[eqDist - 1], parsedData[eqDist + 1], parentNode);
    //            //    parsedData[eqDist] = "teeth";
    //            //   // Debug.Log(myStaticScripts.staticScripts.stringReturn(parsedData));
    //            //    eqDist = myStaticScripts.staticScripts.findString(parsedData, "=");
    //            // //   Debug.Log(parsedData[eqDist] + "\n" + eqDist);

    //            //}
    //        }
    //        stellarisNode tempLeftBrack = dataHolder.addChild(parsedData[eqDist - 1], null, parentNode);
    //        currentString = myStaticScripts.staticScripts.parsedToCurrent(parsedData, bracakDist + 1);
    //        Debug.Log("Current string after both " + currentString);
    //        getDataFromOneTxt(reader, currentString, tempLeftBrack, false);


    //    }
    //    //	}
    //    else if (currentString.Contains("}"))
    //    {
    //        if (currentString == "}")
    //        {
    //            getDataFromOneTxt(reader, currentString, parentNode, true);
    //        }
    //        else
    //        {
    //            parsedData = myStaticScripts.staticScripts.parseString(currentString);
    //            int eqDist = myStaticScripts.staticScripts.findString(parsedData, "=");
    //            int brackDist = myStaticScripts.staticScripts.findString(parsedData, "}");

    //            while (eqDist != -1 && eqDist < brackDist)
    //            {
    //                stellarisNode tempLeftBrack = dataHolder.addChild(parsedData[eqDist - 1], parsedData[eqDist + 1], parentNode);
    //                currentString = myStaticScripts.staticScripts.parsedToCurrent(parsedData, myStaticScripts.staticScripts.findString(parsedData, "}") + 1);
    //                parsedData[eqDist] = "teeth";
    //                eqDist = myStaticScripts.staticScripts.findString(parsedData, "=");
    //                brackDist = myStaticScripts.staticScripts.findString(parsedData, "}");
    //                Debug.Log(tempLeftBrack.name + " is its parent is " + tempLeftBrack.parent.name);
    //            }
    //            currentString = myStaticScripts.staticScripts.parsedToCurrent(parsedData, brackDist + 1);
    //            Debug.Log(currentString + " currentString adter } ");
    //            getDataFromOneTxt(reader, currentString, parentNode.parent, false);

    //        }

    //    }
    //    //sc_b = {
    //    else if (currentString.Contains("{"))
    //    {
    //        parsedData = myStaticScripts.staticScripts.parseString(currentString);

    //        // Debug.Log("RightCalled " + currentString + "\n" + myStaticScripts.staticScripts.stringReturn(parsedData));

    //        stellarisNode tempLeftBrack = dataHolder.addChild(parsedData[0], null, parentNode);
    //        currentString = "";
    //        getDataFromOneTxt(reader, currentString, tempLeftBrack, true);
    //    }
    //    //type = rocky_asteroid_belt
    //    else
    //    {
    //        Debug.Log("None was called " + parentNode.name);
    //        getDataFromOneTxt(reader, currentString, parentNode, true);
    //    }

    //}

    //public void getData_oneTxt(StreamReader reader, string currentString, stellarisNode parentNode, stellarisNode trueParent, bool readNextLine)
    //{
    //    // Debug.Log("CurrentString causing error " + currentString);
    //    // Debug.Log("StartCalled");
    //    //If at end of file just stop all reading
    //    if (reader.EndOfStream == true)
    //    {
    //        return;
    //    }

    //    if (readNextLine == false)
    //    {
    //        return;
    //    }

    //    //Checks to see if we should read next line or not
    //    string nextLine = "";
    //    if (readNextLine == true)
    //    {
    //        nextLine = reader.ReadLine();
    //        currentString += nextLine;
    //        //Debug.Log(currentString);
    //        if (reader.EndOfStream == true)
    //        {
    //            return;
    //        }
    //    }

    //    //Debug.Log("MiddleCalled " + currentString);

    //    //All about what things we need to parse and what we can condense it too
    //    #region Info on what the parser parses
    //    //Checks to see which thing it needs to parse
    //    //List of types of parse
    //    //name = {
    //    //name = data
    //    //name = { name = data}
    //    //name = { name = data name = data}
    //    //}
    //    //name = {
    //    //         name = data
    //    //         name = data
    //    //       }
    //    //"Words"
    //    //name = "Words"

    //    //We can condense this to
    //    //name = {
    //    //name = data
    //    //name = data name = data
    //    //}
    //    //"Words"
    //    //name = "Words"
    //    #endregion Info on what the parser parses
    //    myStaticScripts.staticScripts.parseString(currentString);
    //    //Starting permatiros that need to be calcuated each time the program runs (NOTE: In the future might change this so it is more efficent)

    //    //BUG IF { next to thing it will cause error
    //    string[] parsedData = myStaticScripts.staticScripts.parseString(currentString);


    //    int eqDist = myStaticScripts.staticScripts.findString(parsedData, "=");
    //    int leftBracketDis = myStaticScripts.staticScripts.findString(parsedData, "{");
    //    bool hasEqual = false;
    //    bool hasLeftBracket = false;
    //    if (eqDist != -1)
    //    {
    //        hasEqual = true;
    //    }
    //    if (leftBracketDis != -1)
    //    {
    //        hasLeftBracket = true;
    //    }
    //    if (parsedData.Length == 0)
    //    {
    //        currentString = "";
    //        getData_oneTxt(reader, currentString, parentNode, trueParent, true);
    //        return;
    //    }

    //    //@name = data
    //    if (currentString.Contains("@") && (currentString.IndexOf('@') < currentString.IndexOf('=')))
    //    {
    //        string newName = parsedData[eqDist - 1].Substring(1, parsedData[eqDist - 1].Length - 1);
    //        //Debug.Log(newName + " is the new with @");
    //        dataHolder.variableHolder.Add(new varableNode(newName, parsedData[eqDist + 1]));
    //        currentString = "";
    //        getData_oneTxt(reader, currentString, parentNode, trueParent, true);
    //    }
    //    //name = {
    //    else if (hasEqual && hasLeftBracket)
    //    {
    //        stellarisNode tempNode = dataHolder.addChild(parsedData[eqDist - 1], null, parentNode);
    //        //name = { name = data } for example
    //        if (parsedData.Length >= 4)
    //        {
    //            Debug.Log("Before Fixed " + currentString);
    //            //what this does is turn name = { name = data } into name = data }
    //            currentString = myStaticScripts.staticScripts.parsedToCurrent(parsedData, leftBracketDis + 1);
    //            //Debug.Log("After Fixed " + currentString);
    //            getData_oneTxt(reader, currentString, tempNode, trueParent, false);
    //        }
    //        //It would bean taht parsedData is just name = {
    //        else
    //        {
    //            currentString = "";
    //            getData_oneTxt(reader, currentString, tempNode, trueParent, true);
    //        }
    //    }
    //    else if (hasEqual && false == currentString.Contains("\""))
    //    {
    //        //name = data name = data }
    //        //What this loop will do is add untill there is is no equals left
    //        int max = 100;
    //        while (eqDist != -1)
    //        {
    //            max--;
    //            if (max <= 1)
    //            {
    //                Debug.Log("MAX LIMIT EXCEEDED");
    //                eqDist = -1;
    //            }
    //            if (parsedData.Length >= 4)
    //            {
    //                Debug.Log("this one is longer then four");

    //                Debug.Log("name first " + parsedData[eqDist - 1]);
    //                Debug.Log("data first " + parsedData[eqDist + 1]);

    //                //Add the value to dataHolder
    //                stellarisNode tempNode = dataHolder.addChild(parsedData[eqDist - 1], parsedData[eqDist + 1], parentNode);
    //                //Remove the added bit to make parsedData shorter
    //                parsedData = myStaticScripts.staticScripts.parsedToRemoveParsed(parsedData, eqDist + 2);
    //                //End condition and used if called again
    //                eqDist = myStaticScripts.staticScripts.findString(parsedData, "=");

    //            }
    //            //It would bean taht parsedData is just name = data
    //            else
    //            {
    //                string combined = "";
    //                foreach (string tetth in parsedData)
    //                {
    //                    combined += tetth + " ";
    //                }


    //                Debug.Log(parsedData.Length + "Length Of it," + eqDist + " + eqDist " + combined + " combined");

    //                if ((eqDist - 1) > -1)
    //                {
    //                    Debug.Log("name first " + parsedData[0]);
    //                    Debug.Log("data first " + parsedData[1]);
    //                }
    //                else
    //                {
    //                    Debug.Log("This one has an error with eQDist");
    //                    Debug.Log("name first " + parsedData[0]);
    //                    Debug.Log("data first " + parsedData[1]);
    //                }




    //                stellarisNode tempNode = dataHolder.addChild(parsedData[eqDist - 1], parsedData[eqDist + 1], parentNode);
    //                currentString = "";
    //                parsedData = null;
    //                //We know it is -1 because we started with name = data since we already put in name = data we are left with parsedData = null
    //                //If it is null then eqDist will always = -1
    //                eqDist = -1;
    //            }
    //        }
    //        //We will be left with ""
    //        currentString = myStaticScripts.staticScripts.parsedToCurrent(parsedData, 0);
    //        //Debug.Log("After equls fixed " + currentString);
    //        if (currentString == "")
    //        {
    //            //Debug.Log("Null got called");
    //            getData_oneTxt(reader, currentString, parentNode, trueParent, true);
    //        }
    //        //We will be left with }
    //        else
    //        {
    //            //Debug.Log("Non got called");

    //            getData_oneTxt(reader, currentString, parentNode, trueParent, false);
    //        }
    //    }
    //    //"sc_a" || name = "sc_a" || name = "sc a"
    //    else if (currentString.Contains("\""))
    //    {
    //        //"sc_a"
    //        if (hasEqual == false)
    //        {
    //            stellarisNode tempNode = dataHolder.addChild(parsedData[0], parsedData[0], parentNode);
    //            currentString = "";
    //            getData_oneTxt(reader, currentString, parentNode, trueParent, true);
    //            return;
    //        }

    //        int amountOfQuotations = myStaticScripts.staticScripts.numOfQuotations(parsedData);

    //        //name = "sc_a"
    //        if (amountOfQuotations == 1)
    //        {
    //            stellarisNode tempNode = dataHolder.addChild(parsedData[eqDist - 1], parsedData[eqDist + 1], parentNode);
    //            currentString = "";
    //            getData_oneTxt(reader, currentString, parentNode, trueParent, true);
    //        }
    //        //name = "sc a"
    //        else
    //        {
    //            //We can hard code 2 since we always know that the quotes will start at point
    //            string[] tempData = myStaticScripts.staticScripts.parsedToRemoveParsed(parsedData, 2);
    //            string fullData = myStaticScripts.staticScripts.parsedToCurrent(tempData, 0);
    //            stellarisNode tempNode = dataHolder.addChild(parsedData[eqDist - 1], fullData, parentNode);
    //            currentString = "";
    //            getData_oneTxt(reader, currentString, parentNode, trueParent, true);

    //        }







    //    }
    //    //We should now be left with only }
    //    else if (currentString.Contains("}"))
    //    {
    //        currentString = "";
    //        getData_oneTxt(reader, currentString, parentNode.parent, trueParent, true);
    //    }
    //}

    public void getData_oneTxt_Version_Three(StreamReader reader, stellarisDataHolder parentNode, string fileName)
    {
        //Used to remove the .txt from the fileName
        fileName = fileName.Replace(".txt", "");
        //One thing to consider here is the # sybol because everything after it gets killed
        string entirerFile = "";
        string tempString;
        int locationOfOctothorp = -1;
        //We must remove the # before we combine all the files because if not we would not know when the comment would end
        //For example this code #beans
        //                      planet_bob 
        //If we just stuck it all together it would look like #beans planet_bob
        //Then planet_bob would become a comment and not a data point
        while (reader.EndOfStream == false)
        {
            tempString = reader.ReadLine();
            locationOfOctothorp = tempString.IndexOf('#');
            if(locationOfOctothorp != -1)
            {
                tempString = tempString.Substring(0, locationOfOctothorp);
            }
            entirerFile += tempString + " ";
        }
        //After this point all comments have been removed

        //Debug.Log(entirerFile);
        string[] split = entirerFile.Split(null);
        List<string> trueSplit = split.ToList();

        List<string> tempStringList = new List<string>();
        List<string> tempStringListOutput = new List<string>();

        int currentTrueSplitIndex = 0;
        while (currentTrueSplitIndex < trueSplit.Count)
        {
            //This seperates a string in trueSplit so that it can be split if it has more
            tempStringList = makeSeperate(tempStringList, trueSplit[currentTrueSplitIndex]);
            if (tempStringList != null)
            {
                //This will go through the list of tempStringList and add strings after the input index or i of trueSplit
                for (int o = 0; o < tempStringList.Count; o++)
                {
                    if(tempStringList[o].Length > 0)
                    {
                        tempStringListOutput.Add(tempStringList[o]);
                    }
                }
            }
            //If it = null then an ERROR accourd so we would return a null
            else
            {
                Debug.LogError("ERROR FIX ME");
                return;
            }
            currentTrueSplitIndex++;
        }
        trueSplit = tempStringListOutput;
        //So now we 100% know each thing such as word,{,=,} are alone

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
                if(trueSplit[currentIndexInParse + 1].Contains("{"))
                {
                    //After adding the node we know we must go down a level so we set the new parent to be currentNode
                    currentNode = parentNode.addChild(trueSplit[currentIndexInParse - 1], null,fileName, currentNode);
                    currentIndexInParse += 2;
                }
                //We know that the case is now word = num
                else
                {
                   // Debug.Log("Adding Sibling of " + trueSplit[currentIndexInParse + 1] + " it has a length of " + trueSplit[currentIndexInParse + 1].Length);

                    //After adding the node we know we do not go down a level so we don't change the parent
                    parentNode.addChild(trueSplit[currentIndexInParse - 1], trueSplit[currentIndexInParse + 1],fileName, currentNode);
                    currentIndexInParse += 2;
                }
            }
            //Case should only be }
            else if(trueSplit[currentIndexInParse].Contains("}"))
            {
                currentNode = currentNode.parent;
                currentIndexInParse += 1;

            }
            //case of @var = num
            else if(trueSplit[currentIndexInParse].Contains("@"))
            {
                try
                {
                    //Debug.LogError("YOU ADDED A NEW VARIBLE");
                    parentNode.variableHolder.Add(new varableNode(trueSplit[currentIndexInParse], trueSplit[currentIndexInParse + 2], fileName));
                }
                catch(System.Exception e)
                {
                    Debug.LogError("ERROR of " + e + " you are trying to parse for a varible");
                }
                currentIndexInParse += 3;
            }
            //case flags = { white_hole_system black_while feet_system }
            else if (trueSplit[currentIndexInParse].Contains("flags") && trueSplit[currentIndexInParse].Length <= 5)
            {
                currentNode = parentNode.addChild(trueSplit[currentIndexInParse], null,fileName, currentNode);
                currentIndexInParse += 2;

                //Case now white_hole_system black_while feet_system
                while (trueSplit[currentIndexInParse].Contains("}") == false)
                {
                    parentNode.addChild("flag", trueSplit[currentIndexInParse],fileName, currentNode);
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

        List<string> teeth = parentNode.printOutTreeNames(parentNode.infoHolder.parent);

        string outPut2 = "";

        foreach(string beb in teeth)
        {
            outPut2 += beb + "\n";
        }

        Debug.Log(outPut2);

        string outPut = "";
        for (int i = 0; i < trueSplit.Count; i++)
        {
            outPut += trueSplit[i];
        }
        Debug.Log(outPut);


        string outPutData = parentNode.printOutTree(parentNode.infoHolder.parent);

        Debug.Log("WOOOOOOOOOOOOOOOOOOOOOOOW" + outPutData);
    }

    public List<string> makeSeperate(List<string> output, string input)
    {
        output.Clear();
        //This case is for inputs such as {, =, }
        if (input.Length == 1)
        {
            output.Add(input);
            return output;
        }
        //Cases include {},{words=},{=beans},@beans,words, ect
        else
        {
            List<int> indexOf_Equals =       myStaticScripts.staticScripts.indexOfAll(input, '=');
            if(indexOf_Equals.Count != 0)
                input = parse_specialTouch(input, indexOf_Equals);

            List<int> indexOf_LeftBracket =  myStaticScripts.staticScripts.indexOfAll(input, '{');
            if (indexOf_LeftBracket.Count != 0)
                input = parse_specialTouch(input, indexOf_LeftBracket);

            List<int> indexOf_RightBracket = myStaticScripts.staticScripts.indexOfAll(input, '}');
            if (indexOf_RightBracket.Count != 0)
                input = parse_specialTouch(input, indexOf_RightBracket);

        }
        string[] split = input.Split(null);

        for(int i =0;i<split.Length;i++)
        {
            if(string.IsNullOrWhiteSpace(split[i]) == false && split[i].Length > 0)
            {
                output.Add(split[i]);
                //Debug.Log("In Index " + i + " it is " + split[i]);
            }
        }

        output = split.ToList<string>();

        return output;
    }

    public string parse_specialTouch(string input, List<int> indexOfLookingForChar)
    {
        //takes the input string and adds spaces between each value
        for(int i = indexOfLookingForChar.Count; i-- > 0;)
        {
            input = input.Insert(indexOfLookingForChar[i] + 1, " ");
            input = input.Insert(indexOfLookingForChar[i], " ");
        }
        return input;
    }



    




    

    public List<string> getNameFromFile(string pathFromCommon)
    {
        List<string> starClasses = new List<string>();
        DirectoryInfo d = new DirectoryInfo(pathOfStellaris + pathFromCommon);
        string tempPath = pathOfStellaris + pathFromCommon;
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
