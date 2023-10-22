using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_parsedButtonClicks : UI_click
{
    private data_system passedData;

    public override bool canAction()
    {
        if (passedData == null)
            return false;
        return true;
    }

    public override void clickLeftMouse()
    {
        if (canAction())
        {
            place_manager._place_manager.UI_buttonNodeOnClick(passedData,this.gameObject,place_manager._place_manager.supreamDude.getID(this.gameObject));
        }
    }

    public override void clickRightMouse()
    {
        if (canAction())
        {
            UI_menus._UI_menus.setNewColorPicker(this.gameObject);
            StartCoroutine(updateColor());
        }
    }
    public void setData(data_system data)
    {
        this.passedData = data;
    }

    IEnumerator updateColor()
    {
        colorPicker tempColor = UI_menus._UI_menus.colorPicker.GetComponent<colorPicker>();
        UI_nodeDataSystem tempSystem = this.GetComponent<UI_nodeDataSystem>();
        while(tempColor.isColorChoiceDone == false)
        {
            tempSystem.color = tempColor.color;
            tempSystem.updateKidColors(tempColor.color);
            yield return new WaitForEndOfFrame();
        }
    }



}
