using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BallControllerLockMovement : MonoBehaviour
{
    public enum Behaviour
    {
        LockPlayerControls,
        UnlockPlayerControls,
    }
    public BallController player;
    public Behaviour behaviour;
    public bool useCollision;
    public bool useTrigger;
    public bool destroySelf;
    // Start is called before the first frame update
    private void OnCollisionEnter(Collision collision)
    {
        if (!useCollision) return;

        if(collision.gameObject.tag == "Player")
        {
            if(behaviour == Behaviour.LockPlayerControls)
            {
                player.playerControl = false;
            }
            else
            {
                player.playerControl = true;
            }

            if (destroySelf) Destroy(this.gameObject);
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        if (!useTrigger) return;

        if (other.gameObject.tag == "Player")
        {
            if (behaviour == Behaviour.LockPlayerControls)
            {
                player.playerControl = false;
            }
            else
            {
                player.playerControl = true;
            }

            if (destroySelf) Destroy(this.gameObject);
        }
    }
}
