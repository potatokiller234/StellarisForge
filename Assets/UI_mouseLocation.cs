using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UI_mouseLocation : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public int id;
    public void OnPointerEnter(PointerEventData eventData)
    {
        UI_mouseManager._UI_mouseManager.inGridArea(id);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        UI_mouseManager._UI_mouseManager.outOfGridArea(id);
    }
}
