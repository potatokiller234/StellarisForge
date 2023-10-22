using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_generalButtonFunctions : MonoBehaviour
{
    [Header("Forward/In-ward")]
    public Vector2 moveDistance;
    public GameObject thingToMove;
    public GameObject UI_forwardButton;
    public GameObject UI_inWardButton;
    public float moveTime = 1;


    public void UI_button_Forward()
    {
        RectTransform tempRect = thingToMove.GetComponent<RectTransform>();
        Vector2 endDistnace = new Vector2(tempRect.anchoredPosition.x - moveDistance.x, tempRect.anchoredPosition.y + moveDistance.y);
        StartCoroutine(lerpPos(tempRect, endDistnace, moveTime, UI_inWardButton));
        UI_forwardButton.SetActive(false);
    }
    public void UI_button_InWard()
    {
        RectTransform tempRect = thingToMove.GetComponent<RectTransform>();
        Vector2 endDistnace = new Vector2(tempRect.anchoredPosition.x - moveDistance.x, tempRect.anchoredPosition.y - moveDistance.y);
        StartCoroutine(lerpPos(tempRect, endDistnace, moveTime,UI_forwardButton));
        UI_inWardButton.SetActive(false);
    }

    IEnumerator lerpPos(RectTransform thingToMove, Vector2 distance, float timeToReachTarget, GameObject buttonToEnableAtEnd)
    {
        Vector3 startPos = thingToMove.anchoredPosition;
        Vector2 endPos;
        if (distance.x == 0)
            endPos = new Vector2(0, 0);
        else
            endPos = thingToMove.anchoredPosition - new Vector2(distance.x,distance.y);

        float checkTime = Time.deltaTime/timeToReachTarget;

        while(checkTime < 1)
        {
            thingToMove.anchoredPosition = Vector2.Lerp(startPos, endPos, checkTime);
            checkTime += Time.deltaTime/timeToReachTarget;
            yield return new WaitForEndOfFrame();
        }
        thingToMove.anchoredPosition = endPos;
        buttonToEnableAtEnd.SetActive(true);
    }
}
