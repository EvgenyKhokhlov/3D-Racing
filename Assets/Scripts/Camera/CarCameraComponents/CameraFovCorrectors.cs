using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFovCorrectors : CarCameraComponent
{
    [SerializeField] private float minFieldOfView;
    [SerializeField] private float maxFieldOfView;

    private float defaultFov;

    private void Start()
    {
        camera.fieldOfView = defaultFov;
    }
    private void Update()
    {
        camera.fieldOfView = Mathf.Lerp(minFieldOfView, maxFieldOfView, car.NormalizeLineraVelocity);
    }
}
