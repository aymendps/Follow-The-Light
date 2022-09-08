using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public BallController playerModel;
    public Vector3 moveBy;
    public float speed;
    public bool waitForPlayer;
    public TriggerColliderEvent triggerEvent;

    bool fromInitialtoDestination;
    Vector3 destination;
    Vector3 initialPosition;
    //Rigidbody rb;
    private void Awake()
    {
        initialPosition = transform.position;
        destination = initialPosition  + moveBy;
        fromInitialtoDestination = true;
        //rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        if (waitForPlayer && triggerEvent != null) 
            triggerEvent.triggerColliderFunction += StopWaitingForPlayer;
    }

    public void StopWaitingForPlayer()
    {
        waitForPlayer = false;
        Debug.Log("Stopped Waiting");
        triggerEvent.triggerColliderFunction -= StopWaitingForPlayer;
    }

    private void FixedUpdate()
    {
        if(!waitForPlayer)
        {
            if(fromInitialtoDestination)
            {
                transform.position = Vector3.MoveTowards(transform.position, destination, speed * Time.deltaTime);
                
                //rb.MovePosition(position);

                if(Vector3.Distance(transform.position,destination) < 0.001f)
                {
                    fromInitialtoDestination = false;
                }
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, initialPosition, speed * Time.deltaTime);

                //rb.MovePosition(position);

                if (Vector3.Distance(transform.position, initialPosition) < 0.001f)
                {
                    fromInitialtoDestination = true;
                }
            }

        }
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && playerModel.IsGrounded())
        {
            if(other.transform.parent == null)
            {
                other.transform.SetParent(this.transform);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.transform.SetParent(null);
        }
    }

}
