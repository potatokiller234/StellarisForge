using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DEBUG_EntireDebug : MonoBehaviour
{
    public static DEBUG_EntireDebug DEBUG_entireDebug;

    //Debug vars
    public bool isLOAD = false;
    public bool isID_GEN = false;
    //Used to see if it should print out warning related to settings thing to default
    public bool isDefault = false;
    //If true prints out general info such as Application.persitatnDatapath
    public bool isGeneralInfo = false;
    //If true prints DLC related errors
    public bool isDLC_error = false;



    private void Awake()
    {
        DEBUG_entireDebug = this;
        //Debug.Log(DEBUG_entireDebug);
    }
    /// <summary>
    /// Debugs to console without the help of DEBUG_mode
    /// </summary>
    /// <param name="isCan"></param>
    /// <param name="thingToPrint"></param>
    public void printGenericInformation(bool isCan, string thingToPrint)
    {
        if (isCan)
            Debug.Log(thingToPrint);
    }
    public void printGenericErrorInformation(bool isCan, string thingToPrint)
    {
        if (isCan)
            Debug.Log(thingToPrint);
    }
    /// <summary>
    /// Debugs to console with a DEBUG_mode type
    /// </summary>
    /// <param name="isCan"></param>
    /// <param name="thingToPrint"></param>
    /// <param name="mode"></param>
    public void printInformation(bool isCan, DEBUG_mode mode, string thingToPrint)
    {
        if (isCan == false)
            return;

        if (mode == DEBUG_mode.FILE)
            Debug.LogWarning("Could not find file " + thingToPrint + ". Will add one");
        else if (mode == DEBUG_mode.FILE_PATH)
            Debug.LogWarning("Could not find filePath " + thingToPrint + ". Will add one");
        else if (mode == DEBUG_mode.ID_GEN)
            Debug.LogWarning(thingToPrint);
        else if (mode == DEBUG_mode.FILE_NAME)
            Debug.LogWarning("File " + thingToPrint + " does not have a name, defaulting to name of folder");
        else
            Debug.LogError("Invalid DEBUG_mode, DEBUG_mod " + mode + " not found");
    }

}
public enum DEBUG_mode
{
    FILE,
    FILE_PATH,
    ID_GEN,
    FILE_NAME
}
