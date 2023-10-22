using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_generate : UI_click
{
    public override bool canAction()
    {
        //Not used
        return false;
    }

    public override void clickLeftMouse()
    {
        UI_menus._UI_menus.setPostion(this.gameObject, UI_menus._UI_menus.generate, false);
    }

    public override void clickRightMouse()
    {
        //Not used
    }
}
