using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallFakeMainMenu : MonoBehaviour
{
    [HideInInspector] public Light gl;
    [HideInInspector] public Rigidbody rb;
    TrailRenderer tr;

    public GameObject cameraTarget;
    public float maximumSpeed;

    void Awake()
    {
        gl = GetComponent<Light>();
        rb = GetComponent<Rigidbody>();
        tr = GetComponent<TrailRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateCameraTarget();
        UpdateTrail();
    }

    private void FixedUpdate()
    {
        rb.velocity = Vector3.ClampMagnitude(rb.velocity, maximumSpeed);
    }

    public void UpdateCameraTarget()
    {
        cameraTarget.transform.position = transform.position;
    }

    public void UpdateTrail()
    {
        tr.material.color = gl.color;
    }
}
