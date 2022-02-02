using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] float thrustForce;
    [SerializeField] float torqueAmount;

    [Header("Thrust Sound")]
    [SerializeField] AudioClip thrustSound;
    [SerializeField] [Range(0, 1)] float thrustVolume;

    [Header("Particles")]
    [SerializeField] ParticleSystem thrustParticles;
    [SerializeField] ParticleSystem leftThrustParticles;
    [SerializeField] ParticleSystem rightThrustParticles;

    bool canThrust = false;
    bool canRotateLeft = false;
    bool canRotateRight = false;
    
    Rigidbody rb;
    AudioSource audioSource;

    void Awake() 
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();

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
            PlayEffectsThrust();
        }
        else
        {
            StopEffectsThrust();
        }
    }

    private void StopEffectsThrust()
    {
        audioSource.Stop();
        thrustParticles.Stop();
    }

    private void PlayEffectsThrust()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.pitch = 1f;
            audioSource.PlayOneShot(thrustSound, thrustVolume);
        }

        if (!thrustParticles.isPlaying)
        {
            thrustParticles.Play();
        }
    }

    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            canRotateLeft = true;
            PlayEffectsRotate(rightThrustParticles);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            canRotateRight = true;

            PlayEffectsRotate(leftThrustParticles);
        }
        else
        {
            StopEffectsRotate();
        }
    }

    private void StopEffectsRotate()
    {
        rightThrustParticles.Stop();
        leftThrustParticles.Stop();
    }

    private void PlayEffectsRotate(ParticleSystem particle)
    {
        if (!particle.isPlaying)
        {
            particle.Play();
        }
    }
}
