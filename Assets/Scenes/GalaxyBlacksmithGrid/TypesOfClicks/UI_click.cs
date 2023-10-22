using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class UI_click : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        //Debug.Log("Button was pressed");

        //Left mouse button
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            clickLeftMouse();
            //Debug.Log("Left was pressed");

        }
        //Right mouse button
        else if(eventData.button == PointerEventData.InputButton.Right)
        {
            clickRightMouse();
            //Debug.Log("Right was pressed");

        }
    }

    public abstract void clickLeftMouse();
    public abstract void clickRightMouse();
    public abstract bool canAction();




}
