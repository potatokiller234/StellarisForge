using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu_rockSpin : MonoBehaviour
{
    public bool rotateX;
    public bool rotateY;
    public bool rotateZ;
    public float rotationSpeed;


    Vector3 startAngle;

    // Update is called once per frame
    void Update()
    {
        startAngle = Vector3.zero;
        if(rotateX)
        {
            startAngle.x += (rotationSpeed * Time.deltaTime);
        }
        if (rotateY)
        {
            startAngle.y += (rotationSpeed * Time.deltaTime);
        }
        if (rotateZ)
        {
            startAngle.z += (rotationSpeed * Time.deltaTime);
        }
        this.transform.Rotate(startAngle);
    }
}
