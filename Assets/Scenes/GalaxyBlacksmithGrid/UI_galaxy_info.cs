using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI_galaxy_info : UI_click
{
    [Header("Galaxy_Info")]
    public TextMeshProUGUI galaxyName;
    public TextMeshProUGUI version;
    public TextMeshProUGUI stellarisMapTitle;
    public TextMeshProUGUI currentProjectName;
    public TextMeshProUGUI steamInfo;

    [Header("In Title")]
    public TextMeshProUGUI title_galaxyName;

    public override bool canAction()
    {
        //Not used
        return false;
    }

    public override void clickLeftMouse()
    {
        UI_menus._UI_menus.setPostion(this.gameObject, UI_menus._UI_menus.galaxy_info, false);
    }

    public override void clickRightMouse()
    {
        //Not used
    }
}
