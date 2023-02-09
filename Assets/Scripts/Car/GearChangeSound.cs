using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GearChangeSound : MonoBehaviour
{
    [SerializeField] private Car car;
    [SerializeField] private AudioSource gearAudio;
    void Start()
    {
        car.GearChanged += OnGearChanged;
    }

    private void OnDestroy()
    {
        car.GearChanged -= OnGearChanged;
    }

    private void OnGearChanged()
    {
        gearAudio.Play();
    }
}
