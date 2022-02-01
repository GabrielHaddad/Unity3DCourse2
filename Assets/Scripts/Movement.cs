using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float thrustForce;
    [SerializeField] float torqueAmount;
    bool canThrust = false;
    bool canRotateLeft = false;
    bool canRotateRight = false;
    Rigidbody rb;

    void Awake() 
    {
        rb = GetComponent<Rigidbody>();
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    void FixedUpdate() 
    {
        ThrustRocket();
        RotateRocket();
    }

    void ThrustRocket()
    {
        if (canThrust)
        {
            rb.AddRelativeForce(Vector3.up * thrustForce);
            canThrust = false;
        }
    }

    void RotateRocket()
    {
        if (canRotateLeft)
        {
            AddTorque(torqueAmount);
            canRotateLeft = false;
        }
        else if (canRotateRight)
        {
            AddTorque(-torqueAmount);
            canRotateRight = false;
        }
    }

    void AddTorque(float torque)
    {
        rb.AddRelativeTorque(Vector3.forward * torque);
    }

    void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            canThrust = true;
        }
    }

    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            canRotateLeft = true;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            canRotateRight = true;
        }
    }
}
