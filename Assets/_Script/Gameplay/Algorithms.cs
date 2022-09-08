using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Algorithms
{
    public static float FindClosest(float[] arr, float target)
    {
        int n = arr.Length;
        int i = 0, j = n, mid = 0;
        
        while (i < j)
        {
            mid = (i + j) / 2;

            if (arr[mid] == target) return arr[mid];

            if (target < arr[mid])
            {

                if (mid > 0 && target > arr[mid - 1])
                    return GetClosest(arr[mid - 1], arr[mid], target);

                j = mid;
            }

            else
            {
                if (mid < n - 1 && target < arr[mid + 1])
                    return GetClosest(arr[mid], arr[mid + 1], target);
                i = mid + 1;
            }
        }

        return arr[mid];
    }
    public static float GetClosest(float val1, float val2, float target)
    {
        if (target - val1 >= val2 - target) return val2;
        else return val1;
    }

    public static bool IsInsideCircle(Vector2 centre, Vector2 point, float radius)
    {
        float distance = Vector2.Distance(centre, point);
        return distance < radius;
    }

}

