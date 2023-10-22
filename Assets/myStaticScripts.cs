using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class myStaticScripts : MonoBehaviour
{
    public static myStaticScripts staticScripts;

    private void Awake()
    {
        staticScripts = this;
    }
    public void removeDuplicates(List<string> dupe)
    {
        dupe.Sort();
        List<string> tempArray = new List<string>();


        string[] tempString;
        for(int i =0;i<dupe.Count;i++)
        {
            tempString = dupe[i].Split(' ');
            tempArray.Add(tempString[0]);
        }

        dupe.Clear();

        string pastString = null;

        for (int i = 0; i < tempArray.Count; i++)
        {
            if (pastString == tempArray[i])
            {

            }
            else
            {
                dupe.Add(tempArray[i]);
                pastString = tempArray[i];
            }
        }
    }

    public string realTrim(string input)
    {
        string output = "";

        for(int i =0;i<input.Length;i++)
        {
            if(input[i] != ' ' )
            {
                output += input[i];
            }
        }
        return output;
    }

    public void printList(List<string> thingToPrint)
    {
        string temp = "";
        foreach(string teeth in thingToPrint)
        {
            temp += teeth + "\n";
        }
        Debug.Log(temp);
    }
    public void debugMessage(string message)
    {
        Debug.Log(message);
    }
    /// <summary>
    /// parses string by splitting it into each individual string
    /// </summary>
    /// <param name="thingToBeParsed"></param>
    /// <returns></returns>
    public string[] parseString(string thingToBeParsed)
    {
        //POSSIBLE ERROR OR BUG If input is like count = { min =0 max = 2 }, it will not parse it
        //NOTE this method splits an input string (EXAMPLE - count = { min = 0 max = 2 } )
        //It will split if there is a space between words and return the output. If the 
        //So this would be temp[0] = count
        //                 temp[1] = =
        //                 temp[2] = {
        //                 temp[3] = min
        //                 temp[4] = =
        //                 temp[5] = 0
        //                 temp[6] = max
        //                 temp[7] = =
        //                 temp[8] = 2
        //                 temp[9] = }




        string[] temp = thingToBeParsed.Split(null);
        List<string> temp2 = new List<string>();
        bool stopAdding = false;

        //for(int i =0;i<temp.Length;i++)
        //{
        //    if (temp[i].Contains("=") && temp[i].Length > 1)
        //    {
        //        bool done = false;
        //        while(done == false)
        //        {
        //            int indexToAddSpace = temp[i].IndexOf('=');
        //            temp[i].Insert(indexToAddSpace," ");
        //            string[] splitError = temp[i].Split(null);


        //            if (temp[i].Contains("=") && temp[i].Length > 1)
        //            {
      
        //            }

        //        }
        //    }
        //}



        foreach (string bob in temp)
        {
            if (bob.Length >= 1)
            {
                if (stopAdding == true || bob.Contains("#"))
                {
                    stopAdding = true;
                }
                else
                {
                    temp2.Add(bob);
                }
            }
        }
        temp = temp2.ToArray();


        return temp;
    }
    /// <summary>
    /// Finds the first instance of "thingToFind"
    /// </summary>
    /// <param name="searchThrough"></param>
    /// <param name="thingToFind"></param>
    /// <returns></returns>
    public int findString(string[] searchThrough, string thingToFind)
    {
        for(int i =0;i<searchThrough.Length;i++)
        {
            if(searchThrough[i] == thingToFind)
            {
                return i;
            }
        }
        return -1;
    }

    /// <summary>
    /// Returns all indexes of char in a string
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public List<int> indexOfAll(string input, char charToLookFor)
    {
        List<int> indexes = new List<int>();

        for(int i = 0;i<input.Length;i++)
        {
            if (input[i] == charToLookFor)
                indexes.Add(i);
        }

        return indexes;
    }
    public int indexOfCharAt(string input, int numToSkip, char charToLookFor)
    {
        for (int i = 0; i < input.Length; i++)
        {
            if (input[i] == charToLookFor)
            {
                if (numToSkip > 0)
                {
                    numToSkip--;
                }
                else
                {
                    return i;
                }
            }
        }
        return -1;
    }



    public string parsedToCurrent(string[] parsedThings, int indexToStartAdding)
    {
        if(parsedThings == null)
        {
            return "";
        }

        string final = "";
        for(int i =indexToStartAdding;i<parsedThings.Length;i++)
        {
            final += parsedThings[i] + " ";
        }
        if(final == null)
        {
            Debug.LogError("Should not be null");
        }
        return final;
    }
    public string[] parsedToRemoveParsed(string[] parsedThings, int indexToStartAdding)
    {
        List<string> final = new List<string>();
        for (int i = indexToStartAdding; i < parsedThings.Length; i++)
        {
            final.Add(parsedThings[i]);
        }
        if (final == null)
        {
            Debug.LogError("Should not be null");
        }
        return parsedThings = final.ToArray();
    }
    public void printOutparsedString(string[] parsed)
    {
        string thingToPrint = "";
        foreach(string ted in parsed)
        {
            thingToPrint += ted + "\n";
        }
        Debug.Log(thingToPrint);
    }

    public string stringReturn(string[] parsed)
    {
        string thingToPrint = "";
        foreach (string ted in parsed)
        {
            thingToPrint += ted + "\n";
        }
        return thingToPrint;
    }
    public int numOfQuotations(string[] parsed)
    {
        int bob = 0;
        for(int i =0;i<parsed.Length;i++)
        {
            if (parsed[i].Contains("\""))
            {
                bob++;
            }
        }
        return bob;
    }

    public bool stringToBool(string input)
    {
        if(input == "yes")
        {
            return true;
        }
        else if (input == "no")
        {
            return false;
        }
        else
        {
            Debug.LogError("Invalid bool conversation " + input);
            return false;
        }
             
            
    }

    public int getRandom(double firstNum, double secondNum)
    {
        return Random.Range((int)firstNum, ((int)secondNum) + 1);
    }


}
