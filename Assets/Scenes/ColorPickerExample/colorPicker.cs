using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class colorPicker : MonoBehaviour
{
    RectTransform Rect;
    Texture2D colorPickerTexture;
    public Color color;

    public bool isColorChoiceDone = true;
    void Start()
    {
        Rect = GetComponent<RectTransform>();
        colorPickerTexture = GetComponent<Image>().mainTexture as Texture2D;
    }

    // Update is called once per frame
    void Update()
    {
        if(RectTransformUtility.RectangleContainsScreenPoint(Rect,Input.mousePosition) && isColorChoiceDone == false)
        {
            Vector2 delta;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(Rect, Input.mousePosition, null, out delta);

            float width = Rect.rect.width;
            float height = Rect.rect.height;
            delta += new Vector2(width * .5f, height * .5f);

            float x = Mathf.Clamp(delta.x / width, 0f, 1f);
            float y = Mathf.Clamp(delta.y / width, 0f, 1f);

            int texX = Mathf.RoundToInt(x * colorPickerTexture.width);
            int texY = Mathf.RoundToInt(y * colorPickerTexture.height);

            Color temp = colorPickerTexture.GetPixel(texX, texY);
            if (temp.a != 0)
                color = temp;
            if (Input.GetMouseButtonDown(0))
            {
                UI_menus._UI_menus.disableColorPicker();
                isColorChoiceDone = true;
            }
        }
    }

}
