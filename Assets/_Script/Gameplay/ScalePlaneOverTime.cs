using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScalePlaneOverTime : MonoBehaviour
{
    public float speed;
    public Vector3 maximumScale;
    public bool scaleX;
    public bool scaleY;
    public bool scaleZ;

    float time = 0f;
    Vector3 current = new Vector3(1, 1, 1);
    bool increase = true;

    private void OnValidate()
    {
        maximumScale.x = (maximumScale.x <= 0) ? 1 : maximumScale.x;
        maximumScale.y = (maximumScale.y <= 0) ? 1 : maximumScale.y;
        maximumScale.z = (maximumScale.z <= 0) ? 1 : maximumScale.z;
    }

    public void Scale()
    {
        if (increase) time += speed * Time.deltaTime;
        else time -= speed * Time.deltaTime;

        if (scaleX)
        {
            current.x = Mathf.Lerp(1f, maximumScale.x, time);
            if(1f >= current.x)
            {
                current.x = 1;
            }
            else if(current.x >= maximumScale.x)
            {
                current.x = maximumScale.x;
            }
        }

        if (scaleY)
        {
            current.y = Mathf.Lerp(1f, maximumScale.y, time);
            if (1f >= current.y)
            {
                current.y = 1;
            }
            else if (current.y >= maximumScale.y)
            {
                current.y = maximumScale.y;
            }
        }

        if (scaleZ)
        {
            current.z = Mathf.Lerp(1f, maximumScale.z, time);
            if (1f >= current.z)
            {
                current.z = 1;
            }
            else if (current.z >= maximumScale.z)
            {
                current.z = maximumScale.z;
            }
        }

        transform.localScale = current;
        
        if(current == maximumScale)
        {
            increase = false;
        }
        else if(current == new Vector3(1f,1f,1f))
        {
            increase = true;
        }
    }
    // Update is called once per frame
    void Update()
    {
        Scale();
    }
}
