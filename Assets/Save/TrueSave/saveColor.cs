using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class saveColor
{
    float r, g, b, a;

    public saveColor(Color colorToSet)
    {
        r = colorToSet.r;
        g = colorToSet.g;
        b = colorToSet.b;
        a = colorToSet.a;
    }

    public void setColor(Color colorToSet)
    {
        r = colorToSet.r;
        g = colorToSet.g;
        b = colorToSet.b;
        a = colorToSet.a;

    }

    public Color getColor()
    {
        return new Color(r, g, b, a);
    }
}
