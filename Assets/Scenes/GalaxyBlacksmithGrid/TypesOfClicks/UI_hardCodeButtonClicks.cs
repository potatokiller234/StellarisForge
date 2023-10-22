using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_hardCodeButtonClicks : UI_click
{
    public gridPlaceType hardCodeType;
    public override bool canAction()
    {
        //Not needed
        return false;
        //throw new System.NotImplementedException();
    }

    public override void clickLeftMouse()
    {
        switch (hardCodeType)
        {
            case gridPlaceType.hyperLane:
                place_manager._place_manager.UI_buttonNode_Hyperlane();
                break;
            case gridPlaceType.wormHole:
                place_manager._place_manager.UI_buttonNode_Wormhole();
                break;
            case gridPlaceType.empireSpawn:
                place_manager._place_manager.UI_buttonNode_EmpireSpawn();
                break;
            case gridPlaceType.nebula:
                place_manager._place_manager.UI_buttonNode_Nebula();
                break;
            default:
                Debug.LogError("GridType " + hardCodeType + " isInvalid or not Implmented for Default");
                break;
        }
    }

    public override void clickRightMouse()
    {
        UI_menus._UI_menus.setNewColorPicker(this.gameObject);
    }
}
