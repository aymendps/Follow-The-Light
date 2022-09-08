using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BallController : MonoBehaviour
{
    #region Public Variables
    public MoveMode movementMode;
    public bool playerControl;
    [Header("Velocity Settings")]
    public float movementSpeed;
    public float maximumSpeed;
    public float cameraSpeed;
    [Header("Jump Settings")]
    public float jumpPower;
    public float airResistance;
    public Collider playerCollider;
    [Range(0f, 1f)] public float bounciness;
    [Range(0f,1f)] public float bounceTimeWindow;
    public float bounceRadius;
    public int maximumBounceTimes;
    public float distanceToGround;

    [Header("Other Gameobjects & Colliders")]
    public string targetGlowTag;
    public string waypointTag;
    public Material glowMaterial;
    public int jumpWallLayer;
    public int groundLayer;
    public GameObject cameraTarget;

    #endregion

    #region Private Variables

    Vector3 direction;
    Quaternion directionCorrection;
    float cameraRotationDirection;

    Light gl;
    Rigidbody rb;
    TrailRenderer tr;
    Vector3 boundsX;
    Vector3 boundsZ;
    Vector3 initialScale;
    readonly float[] cameraAnglePresets = {0f, 90f, 180f, 270f, 360f};
    float tempAngle = 0f;

    bool usedJumpWall;
    float jumpPowerMulti;
    float jumpMaxPowerMulti;
    Vector2 lastJumpPosition = Vector2.positiveInfinity;
    bool canBounce = false;
    float currentBounceTime = 0f;


    #endregion

    #region Inspector, Gizmos
    private void OnValidate()
    {
        distanceToGround = (distanceToGround <= 0) ? 0.1f : distanceToGround;
        jumpPower = (jumpPower <= 0) ? 6 : jumpPower;
        movementSpeed = (movementSpeed <= 0) ? 10 : movementSpeed;
        maximumSpeed = (maximumSpeed <= 0) ? 8 : maximumSpeed;
        cameraSpeed = (cameraSpeed <= 0) ? 1 : cameraSpeed;
        airResistance = (airResistance <= 0) ? 1 : airResistance;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, Vector3.left * distanceToGround);
        Gizmos.DrawRay(transform.position, Vector3.right * distanceToGround);
        Gizmos.DrawRay(transform.position, Vector3.forward * distanceToGround);
        Gizmos.DrawRay(transform.position, Vector3.back * distanceToGround);
        Gizmos.DrawRay(transform.position + new Vector3(playerCollider.bounds.extents.x, 0, 0), Vector3.down * distanceToGround);
        Gizmos.DrawRay(transform.position - new Vector3(playerCollider.bounds.extents.x, 0, 0), Vector3.down * distanceToGround);
        Gizmos.DrawRay(transform.position + new Vector3(0, 0, playerCollider.bounds.extents.z), Vector3.down * distanceToGround);
        Gizmos.DrawRay(transform.position - new Vector3(0, 0, playerCollider.bounds.extents.z), Vector3.down * distanceToGround);
    }
    #endregion
    private void Awake()
    {
        gl = GetComponent<Light>();
        rb = GetComponent<Rigidbody>();
        tr = GetComponent<TrailRenderer>();
        boundsX = new Vector3(playerCollider.bounds.extents.x,0,0);
        boundsZ = new Vector3(0, 0, playerCollider.bounds.extents.z);
        initialScale = transform.localScale;
        jumpPowerMulti = 1f;
        jumpMaxPowerMulti = jumpPowerMulti + bounciness * maximumBounceTimes;
    }

    private void Update()
    {
        //CheckScale();
        UpdateMovementAxis();
        UpdateCameraTarget();
        UpdateTrail();
        UpdateJumpState();

        if (Keyboard.current.rKey.wasPressedThisFrame)
        {
            transform.position = new Vector3(0f, 0f, -10f);
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.Sleep();
        }
        if (Keyboard.current.yKey.wasPressedThisFrame)
        {
            transform.position = new Vector3(38.5f, -6.8f, -17.9f);
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.Sleep();
        }
    }

    private void FixedUpdate()
    {
        UpdateMovement();
    }

    #region Updates
    public void UpdateMovement()
    {
        switch (movementMode)
        {
            case MoveMode.ApplyForce:
                if (!IsGrounded() && !usedJumpWall)
                {
                    rb.AddForce(directionCorrection * direction * movementSpeed / airResistance);
                    if(!canBounce)
                    {
                        rb.velocity = Vector3.ClampMagnitude(rb.velocity, maximumSpeed);
                    }
                }
                else
                {
                    rb.AddForce(directionCorrection * direction * movementSpeed);
                }
                break;
                
            case MoveMode.Velocity:
                Vector3 velocity = new Vector3(direction.x * movementSpeed, rb.velocity.y, direction.z 
                    * movementSpeed);
                rb.velocity = velocity;
                break;
        } 
    }

    public void CheckScale()
    {
        if(transform.localScale != initialScale)
        {
            transform.localScale = initialScale;
        }
    }
    
    //public void UpdateMovementMode()
    //{
    //    if (IsGrounded() && doThisOnce)
    //    {
    //        rb.velocity /= 2;
    //        rb.angularVelocity /= 2;
    //        movementMode = MoveMode.ApplyForce;
    //        speed = 12;
    //        Debug.Log("False");
    //        doThisOnce = false;
    //    }
    //    else if (!IsGrounded() && !doThisOnce)
    //    {
    //        movementMode = MoveMode.Velocity;
    //        speed = 5;
    //        Debug.Log("True");
    //        doThisOnce = true;
    //    }
    //}

    public void UpdateTrail()
    {
        tr.material.color = gl.color;
    }

    public void UpdateCameraTarget()
    {
        cameraTarget.transform.position = transform.position;
        cameraTarget.transform.Rotate(0f, cameraRotationDirection * cameraSpeed * Time.deltaTime, 0f, Space.World);
    }

    public void UpdateMovementAxis()
    {
        directionCorrection = Quaternion.Euler(0f, cameraTarget.transform.rotation.eulerAngles.y, 0f);
    }
    public void UpdateJumpState()
    {
        Vector2 p = new Vector2(transform.position.x, transform.position.z);
        if (Algorithms.IsInsideCircle(lastJumpPosition, p, bounceRadius) && !canBounce)
        {
            canBounce = true;
        }
        else if(!Algorithms.IsInsideCircle(lastJumpPosition, p, bounceRadius) && canBounce)
        {
            canBounce = false;
            lastJumpPosition = Vector2.positiveInfinity;
        }
    }

    #endregion

    #region Boolean
    public bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down + boundsX, distanceToGround)
            || Physics.Raycast(transform.position, Vector3.down - boundsX, distanceToGround)
            || Physics.Raycast(transform.position, Vector3.down + boundsZ, distanceToGround)
            || Physics.Raycast(transform.position, Vector3.down - boundsZ, distanceToGround);
    }

    public bool IsOnWall()
    {
        return Physics.Raycast(transform.position, Vector3.left, distanceToGround)
            || Physics.Raycast(transform.position, Vector3.right, distanceToGround)
            || Physics.Raycast(transform.position, Vector3.forward, distanceToGround)
            || Physics.Raycast(transform.position, Vector3.back, distanceToGround);
    }
    #endregion

    #region Player Input
    public void OnMove(InputValue value)
    {
        if (!playerControl)
        {
            direction = Vector3.zero;
            return;
        }

        Vector2 val = value.Get<Vector2>();
        direction =  new Vector3(val.x, 0, val.y);
    }

    public void OnLook(InputValue value)
    {
        if (!playerControl)
        {
            cameraRotationDirection = 0f;
            return;
        }

        cameraRotationDirection = value.Get<float>();
        
    }

    public void OnFixedLook()
    {
        if (!playerControl) return;

        if (cameraTarget.transform.rotation.eulerAngles.y == tempAngle)
        {
            cameraTarget.transform.Rotate(0f, 90f, 0f, Space.World);
            tempAngle = cameraTarget.transform.rotation.eulerAngles.y;
        }
        else
        {
            tempAngle = Algorithms.FindClosest(cameraAnglePresets, cameraTarget.transform.rotation.eulerAngles.y);
            cameraTarget.transform.eulerAngles = new Vector3(45f, tempAngle, 0f);
        }

    }

    public void OnToggleHardMode()
    {
        if (!playerControl) return;

        bool state = !LevelManager.instance.hardMode;
        LevelManager.instance.hardMode = state;
        LevelManager.instance.hardModeIcon.enabled = state;
    }

    public void OnBounce()
    {
        if (!playerControl) return;

        if (IsOnWall() && !IsGrounded() && usedJumpWall)
        {
            rb.AddForce(directionCorrection * (Vector3.up + direction) * jumpPower, ForceMode.VelocityChange);
        }
        else if(IsGrounded())
        {
            if (canBounce && jumpPowerMulti < jumpMaxPowerMulti && currentBounceTime > Time.time)
            {
                jumpPowerMulti += bounciness;
            }
            else if(!canBounce || currentBounceTime < Time.time)
            {
                jumpPowerMulti = 1f;
            }

            if(!Algorithms.IsInsideCircle(lastJumpPosition, 
                new Vector2(transform.position.x, transform.position.z), bounceRadius))
            {
                lastJumpPosition = new Vector2(transform.position.x, transform.position.z);
            }
            rb.AddForce(Vector3.up * jumpPower * jumpPowerMulti, ForceMode.VelocityChange);
        }
    }

    #endregion
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag(targetGlowTag))
        {
            collision.gameObject.GetComponent<WallGlowLogic>().ChangeMaterial(gl,glowMaterial);
        }

        if (collision.gameObject.layer == jumpWallLayer)
        {
            usedJumpWall = true;
        }
        else if(collision.gameObject.layer == groundLayer && canBounce)
        {
            currentBounceTime = Time.time + bounceTimeWindow;
            usedJumpWall = false;
        }
        else
        {
            usedJumpWall = false;
        }
    }

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.CompareTag(waypointTag))
        {
            Debug.Log("enter waypoint range");
            other.gameObject.GetComponent<Waypoint>().ShowWaypointText();
        }
    }

    private void OnTriggerExit(Collider other) {
        if(other.gameObject.CompareTag(waypointTag))
        {
            Debug.Log("exit waypoint range");
            other.gameObject.GetComponent<Waypoint>().HideWaypointText();
        }
    }



}

public enum MoveMode
{
    ApplyForce,
    Velocity
}
