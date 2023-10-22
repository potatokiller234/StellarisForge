using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UI_scrollBarImageUpdate : MonoBehaviour
{
    public GameObject imageExample;
    public UI_parsedToButtons Instance_UI_parsedToButtons;
    public float yTopPos;
    public float yBottomPos;


    public Transform cool_parent;


    private RectTransform rect_imageExample;

    public List<float> UI_nodeDataModPositions;

    public RectTransform rectOfParent;


    // Start is called before the first frame update


    //public void doTheGunk()
    //{

    //}

    //public Vector2 ComponentMultiply(Vector2 v, Vector2 other)
    //{
    //    return new Vector2(v.x * other.x, v.y * other.y);
    //}

    //public void spawnInNewImage(float lengthOfImage, float currentYPos, Color colorOfImage)
    //{
    //    //Spawns in image
    //    GameObject newImage = Instantiate(imageExample);
    //    newImage.transform.SetParent(cool_parent);
    //    newImage.SetActive(true);

    //    //Sets image up good and nice
    //    RectTransform tempRect = newImage.GetComponent<RectTransform>();
    //    tempRect.anchoredPosition = new Vector2(tempRect.anchoredPosition.x, currentYPos - lengthOfImage);
    //    tempRect.sizeDelta = new Vector2(tempRect.sizeDelta.x, lengthOfImage);
    //    newImage.GetComponent<Image>().color = colorOfImage;

    //}





}
