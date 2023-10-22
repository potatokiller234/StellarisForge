using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public static class parseCommands
{
    #region Parse Raw file into Standardized Raw File
    /// <summary>
    /// Parses a fileInfo, removing all comments and gunk, to return a List<string>
    /// </summary>
    /// <param name="fileInfo"></param>
    /// <param name="my_persistentDataPath"></param>
    /// <param name="isGoingToDebug"></param>
    /// <returns></returns>
    public static List<string> parseFile(FileInfo fileInfo, string my_persistentDataPath,bool isGoingToDebug)
    {
        string input = readAndRemoveComments(fileInfo);
        List<string> trueSplit = removeInputGunk(input, my_persistentDataPath, isGoingToDebug);
        return trueSplit;
    }
    /// <summary>
    /// Parsed a file and returns a string of that file with no comments
    /// </summary>
    /// <param name="filePath"></param>
    /// <returns></returns>
    private static string readAndRemoveComments(FileInfo filePath)
    {
        //Set up vars
        string output = "";
        StreamReader rd = new StreamReader(filePath.FullName);
        string tempString = "";
        int locationOfOctothorp = -1;
        //Read entire file
        while (rd.EndOfStream == false)
        {
            tempString = rd.ReadLine();
            locationOfOctothorp = tempString.IndexOf('#');
            if (locationOfOctothorp != -1)
            {
                //Removes everything after location of #
                tempString = tempString.Substring(0, locationOfOctothorp);
            }
            output += tempString + " ";
        }
        rd.Close();
        return output;
    }

    /// <summary>
    /// Remoces input gunk such as tabs, ect
    /// </summary>
    /// <param name="input"></param>
    /// <param name="my_persistentDataPath"></param>
    /// <param name="isGoingToDebug"></param>
    /// <returns></returns>
    private static List<string> removeInputGunk(string input, string my_persistentDataPath, bool isGoingToDebug)
    {
        input = input.Replace('\t', ' ');
        string[] output = input.Split('"');
        for (int i = 0; i < output.Length; i++)
        {
            output[i] = output[i].Trim();
        }



        //Debug stuff
        if (isGoingToDebug)
        {
            #region Debug
            try
            {
                string debugOutput = "";
                int numTillNewLine = 10;
                int currentIndex = 0;
                for (int i = 0; i < output.Length; i++)
                {
                    debugOutput += "|" + output[i] + "| ";
                    currentIndex++;
                    if (currentIndex >= numTillNewLine)
                    {
                        debugOutput += "\n";
                        currentIndex = 0;
                    }
                }
                StreamWriter debug_output = new StreamWriter(my_persistentDataPath + @"/Debug/" + " superRawOutput_" + System.Guid.NewGuid() + ".txt");
                debug_output.Write(debugOutput);
                debug_output.Close();
            }
            catch (System.Exception e)
            {
                Debug.LogError("There was an error with debugging \n " + e);
            }
            #endregion
        }

        List<string> teeth = new List<string>();
        List<string> result = new List<string>();
        //  Debug.Log(trueSplit.Count);
        for (int i = 0; i < output.Length; i++)
        {
            try
            {
                //Debug.Log(output[i]);
                teeth = makeSeperate(output[i]);

                for (int o = 0; o < teeth.Count; o++)
                {
                    result.Add(teeth[o]);
                }
            }
            catch (System.Exception e)
            {
                Debug.LogError(e);
            }
        }      
        return result;
    }
    /// <summary>
    /// If it is touching then make it seperate
    /// </summary>
    /// <param name="output"></param>
    /// <param name="input"></param>
    /// <returns></returns>
    private static List<string> makeSeperate(string input)
    {
        string[] split;
        //This case is for inputs such as {, =, }
        if (input.Length == 1)
        {
            return new List<string>() { input };
        }
        //Cases include {},{words=},{=beans},@beans,words, ect
        else
        {
            //Debug.Log("Before|" + input);
            bool didIFind = false;
            //Debug.Log("worl");
            List<int> indexOf_Equals = myStaticScripts.staticScripts.indexOfAll(input, '=');
            //Debug.Log("Figs");
            if (indexOf_Equals.Count != 0)
            {
                input = parse_specialTouch(input, indexOf_Equals);
                didIFind = true;
            }

            List<int> indexOf_LeftBracket = myStaticScripts.staticScripts.indexOfAll(input, '{');
            if (indexOf_LeftBracket.Count != 0)
            {
                input = parse_specialTouch(input, indexOf_LeftBracket);
                didIFind = true;
            }
            List<int> indexOf_RightBracket = myStaticScripts.staticScripts.indexOfAll(input, '}');
            if (indexOf_RightBracket.Count != 0)
            {
                input = parse_specialTouch(input, indexOf_RightBracket);
                didIFind = true;
            }
            // Debug.Log(input);
            if (didIFind == true)
            {
                split = input.Split(null);
            }
            else
            {
                return new List<string>() { input };
            }
        }

        List<string> feet = new List<string>();

        // string together = "After";
        for (int i = 0; i < split.Length; i++)
        {
            if (string.IsNullOrWhiteSpace(split[i]) == false)
            {
                // together += "|" + temp[i];
                feet.Add(split[i]);
            }
        }
        return feet;
    }

    /// <summary>
    ///takes the input string and adds spaces between each value
    /// </summary>
    /// <param name="input"></param>
    /// <param name="indexOfLookingForChar"></param>
    /// <returns></returns>
    private static string parse_specialTouch(string input, List<int> indexOfLookingForChar)
    {
        //takes the input string and adds spaces between each value
        for (int i = indexOfLookingForChar.Count; i-- > 0;)
        {
            input = input.Insert(indexOfLookingForChar[i] + 1, " ");
            input = input.Insert(indexOfLookingForChar[i], " ");
        }
        return input;
    }

    #endregion

    #region Parse (Only Single) Standardized raw file into abstract syntax tree
    /// <summary>
    /// Raw Stellaris File to Abstract Syntax Tree
    /// </summary>
    /// <param name="fileName"></param>
    /// <param name="fileInfo"></param>
    /// <param name="isGoingToDebug"></param>
    /// <param name="my_persistentDataPath"></param>
    /// <returns></returns>
    public static stellarisDataHolder parse_StandardRaw_to_ABT(FileInfo fileInfo, string my_persistentDataPath, bool isGoingToDebug)
    {
        stellarisDataHolder parent = new stellarisDataHolder("Static Galaxy");
        getData_oneTxt_Version_Three(parent,fileInfo.Name, fileInfo, isGoingToDebug, my_persistentDataPath);
        return parent;
    }
    /// <summary>
    /// Parse Standardized Raw Files into an abstract syntax tree
    /// </summary>
    /// <param name="parentNode"></param>
    /// <param name="fileName"></param>
    /// <param name="fileInfo"></param>
    /// <param name="isGoingToDebug"></param>
    /// <param name="my_persistentDataPath"></param>
    private static void getData_oneTxt_Version_Three(stellarisDataHolder parentNode, string fileName, FileInfo fileInfo, bool isGoingToDebug, string my_persistentDataPath)
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
                StreamWriter debug_output = new StreamWriter(my_persistentDataPath + @"/Debug/" + fileName + " rawOutput.txt");
                debug_output.Write(debugOutput);
                debug_output.Close();
            }
            catch (System.Exception e)
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
    }
    #endregion



    #region Old code
    /*
     *  private static List<string> removeInputGunk(string input, string my_persistentDataPath, bool isGoingToDebug)
    {
        input = input.Replace('\t', ' ');
        List<string> output = input.Split('"')
                             .Select((element, index) => index % 2 == 0  // If even index
                                                   ? element.Split(new[] { ' ' }, System.StringSplitOptions.RemoveEmptyEntries)  // Split the item
                                                   : new string[] { element })  // Keep the entire item
                             .SelectMany(element => element).ToList();

        for (int i = 0; i < output.Count; i++)
        {
            output[i] = output[i].Trim();
        }



        //Debug stuff
        if (isGoingToDebug)
        {
            try
            {
                string debugOutput = "";
                int numTillNewLine = 10;
                int currentIndex = 0;
                for (int i = 0; i < output.Count; i++)
                {
                    debugOutput += "|" + output[i] + "| ";
                    currentIndex++;
                    if (currentIndex >= numTillNewLine)
                    {
                        debugOutput += "\n";
                        currentIndex = 0;
                    }
                }
                StreamWriter debug_output = new StreamWriter(my_persistentDataPath + @"/Debug/" + " superRawOutput_" + System.Guid.NewGuid() + ".txt");
                debug_output.Write(debugOutput);
                debug_output.Close();
            }
            catch (System.Exception e)
            {
                Debug.LogError("There was an error with debugging \n " + e);
            }
        }

        List<string> teeth = new List<string>();
        List<string> result = new List<string>();
        //  Debug.Log(trueSplit.Count);
        for (int i = 0; i < output.Count; i++)
        {
            try
            {
                //Debug.Log(output[i]);
                teeth = makeSeperate(output[i]);

                for (int o = 0; o < teeth.Count; o++)
                {
                    result.Add(teeth[o]);
                }
            }
            catch (System.Exception e)
            {
                Debug.LogError(e);
            }
            //for(int o = 0;o<teeth.Count;o++)
            //{
            //    result.Add(teeth[i]);
            //}
        }
        output = result;
        return output;
    }
     * 
     */
    #endregion




}
