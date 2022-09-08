using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Waypoint : MonoBehaviour
{
    public TextMeshProUGUI waypointText;
    public float fadingSpeed;
    
    public void ShowWaypointText()
    {
        StartCoroutine(Fading.FadeInText(fadingSpeed, waypointText));
    }

    public void HideWaypointText()
    {
        StartCoroutine(Fading.FadeOutText(fadingSpeed, waypointText));
    }
}
