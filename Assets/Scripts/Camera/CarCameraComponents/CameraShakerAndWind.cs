using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShakerAndWind : CarCameraComponent
{
    [SerializeField][Range(0f, 1f)] private float normalizeSpeedShake;
    [SerializeField] private float shakeAmount;
    [SerializeField] private AudioSource windSound;
    private void Update()
    {
        if (car.NormalizeLineraVelocity >= normalizeSpeedShake)
        {
            transform.localPosition += Random.insideUnitSphere * shakeAmount * Time.deltaTime;
            if (!windSound.isPlaying) windSound.Play();
        }
        else if (windSound.isPlaying)
        {
            windSound.Stop();
        }
        
    }
}
