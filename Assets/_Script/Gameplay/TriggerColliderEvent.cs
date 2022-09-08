using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class TriggerColliderEvent : MonoBehaviour
{
    public bool destroySelfAfter;
    public bool disableSelfAfter;

    public delegate void TriggerColliderFunction();
    public TriggerColliderFunction triggerColliderFunction;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            if(triggerColliderFunction!=null)
            {
                triggerColliderFunction.Invoke();

                if (destroySelfAfter) Destroy(this.gameObject);
                else if (disableSelfAfter) this.gameObject.SetActive(false);
            }

        }
    }
}
