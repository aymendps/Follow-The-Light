using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowOrHideObject : MonoBehaviour
{
    public bool disableOnEnter;
    public bool enableOnExit;
    MeshRenderer[] mr;

    private void Awake()
    {
        mr = GetComponentsInChildren<MeshRenderer>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (disableOnEnter)
        {
            foreach(MeshRenderer m in mr)
            {
                m.enabled = false;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (enableOnExit)
        {
            foreach (MeshRenderer m in mr)
            {
                m.enabled = true;
            }
        }
    }
}
