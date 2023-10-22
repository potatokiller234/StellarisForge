using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_parseStellaris : MonoBehaviour
{
    public solarSystemSpawn SolarSystemSpawn;
    public Parse_Test parse_Test;
    public UI_parsedToButtons uI_parsedToButtons;
    public UI_scrollBarImageUpdate ui_scrollBarImageUpdate;

    public void startParsing()
    {
        resetBools();
        StartCoroutine(waitParsing());
    }


    IEnumerator waitParsing()
    {


        //We start the parsing from .txt to stellarisNodes
        parse_Test.startParse();
        yield return new WaitUntil(() => parse_Test.bob.isDoneParsing == true);
        //Stellaris data holder is now full of all the correct data
        //We now need to parse the Stellars Nodes into usable unity data
        SolarSystemSpawn.stellarisNode_to_UnityData();
        yield return new WaitUntil(() => SolarSystemSpawn.isDoneConvertingData == true);
        //Now the data is usable by unity
        //We now spawe in buttons to be used
        uI_parsedToButtons.spawnInButtons();
        cleanRef._cleanRef._UI_buttonsToStatic.moveToStatic();

      

        Debug.Log("WE DONE!!");



    }

    private void resetBools()
    {
        parse_Test.stellarisParser.isDoneParsing = false;
        SolarSystemSpawn.isDoneConvertingData = false;
    }

}
