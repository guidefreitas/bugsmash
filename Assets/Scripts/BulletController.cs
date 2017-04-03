using UnityEngine;
using System.Collections;
using System;

public class BulletController : MonoBehaviour {

    private GvrAudioSource audioSource;
    public float bounceSoundVelocity = 0.0f;
    void Start()
    {
        audioSource = GetComponent<GvrAudioSource>();
    }

	void OnCollisionEnter(Collision collision)
    {
        
        if(Math.Abs(collision.relativeVelocity.x) > bounceSoundVelocity ||
            Math.Abs(collision.relativeVelocity.y) > bounceSoundVelocity ||
            Math.Abs(collision.relativeVelocity.z) > bounceSoundVelocity)
        {
            audioSource.Play();
        }
    }
}
