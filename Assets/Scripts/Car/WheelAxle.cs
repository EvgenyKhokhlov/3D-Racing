using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WheelAxle
{
    [SerializeField] private WheelCollider leftWheelColliders;
    [SerializeField] private WheelCollider rightWheelColliders;

    [SerializeField] private Transform leftWheelMesh;
    [SerializeField] private Transform rightWheelMesh;

    [SerializeField] private bool isMotor;
    public bool IsMotor => isMotor;
    [SerializeField] private bool isSteer;
    public bool IsSteer => isSteer;

    [SerializeField] private float wheelWidth;

    [SerializeField] private float antiRollForce;

    [SerializeField] private float additionalWheelDownForce;

    [SerializeField] private float baseForwardStiffnes = 1.5f;
    [SerializeField] private float stabilityForwardFactor = 1.0f;

    [SerializeField] private float baseSidewayStiffnes = 2.0f;
    [SerializeField] private float stabilitySidewayFactor = 1.0f;

    private WheelHit leftWheelHit;
    private WheelHit rightWheelHit;

    //Public API
    public void Update()
    {
        UpdateWheelHits();

        ApplyAntiRoll();
        ApplyDownForce();
        CorrectStiffess();

        SyncMeshTransform();
    }

    public void ConfiguredVehicleSubsteps(float speedThreshold, int speedBelowThreshold, int stepsAboveThreshold)
    {
        leftWheelColliders.ConfigureVehicleSubsteps(speedThreshold, speedBelowThreshold, stepsAboveThreshold);
        rightWheelColliders.ConfigureVehicleSubsteps(speedThreshold, speedBelowThreshold, stepsAboveThreshold);
    }

    private void UpdateWheelHits()
    {
        leftWheelColliders.GetGroundHit(out leftWheelHit);
        rightWheelColliders.GetGroundHit(out rightWheelHit);
    }

    private void CorrectStiffess()
    {
        WheelFrictionCurve leftForward = leftWheelColliders.forwardFriction;
        WheelFrictionCurve rightForward = rightWheelColliders.forwardFriction;

        WheelFrictionCurve leftSideways = leftWheelColliders.sidewaysFriction;
        WheelFrictionCurve rightSideways = rightWheelColliders.sidewaysFriction;

        leftForward.stiffness = baseForwardStiffnes + Mathf.Abs(leftWheelHit.forwardSlip) * stabilityForwardFactor;
        rightForward.stiffness = baseForwardStiffnes + Mathf.Abs(rightWheelHit.forwardSlip) * stabilityForwardFactor;

        leftSideways.stiffness = baseSidewayStiffnes + Mathf.Abs(leftWheelHit.sidewaysSlip) * stabilitySidewayFactor;
        rightSideways.stiffness = baseSidewayStiffnes + Mathf.Abs(rightWheelHit.sidewaysSlip) * stabilitySidewayFactor;

        leftWheelColliders.forwardFriction = leftForward;
        rightWheelColliders.forwardFriction = rightForward;

        leftWheelColliders.sidewaysFriction = leftSideways;
        rightWheelColliders.sidewaysFriction = rightSideways;
    }

    private void ApplyDownForce()
    {
        if (leftWheelColliders.isGrounded == true)
            leftWheelColliders.attachedRigidbody.AddForceAtPosition(leftWheelHit.normal * -additionalWheelDownForce
                * leftWheelColliders.attachedRigidbody.velocity.magnitude, leftWheelColliders.transform.position);

        if (rightWheelColliders.isGrounded == true)
            rightWheelColliders.attachedRigidbody.AddForceAtPosition(rightWheelHit.normal * -additionalWheelDownForce
                * rightWheelColliders.attachedRigidbody.velocity.magnitude, rightWheelColliders.transform.position);
    }

    private void ApplyAntiRoll()
    {
        float travelL = 1.0f;
        float travelR = 1.0f;

        if (leftWheelColliders.isGrounded == true)
            travelL = (-leftWheelColliders.transform.InverseTransformPoint(leftWheelHit.point).y - leftWheelColliders.radius) / leftWheelColliders.suspensionDistance;
        if (rightWheelColliders.isGrounded == true)
            travelR = (-rightWheelColliders.transform.InverseTransformPoint(rightWheelHit.point).y - rightWheelColliders.radius) / rightWheelColliders.suspensionDistance;

        float forceDir = (travelL - travelR);

        if (leftWheelColliders.isGrounded == true)
            leftWheelColliders.attachedRigidbody.AddForceAtPosition(leftWheelColliders.transform.up * -forceDir * antiRollForce, leftWheelColliders.transform.position);
        if (rightWheelColliders.isGrounded == true)
            rightWheelColliders.attachedRigidbody.AddForceAtPosition(rightWheelColliders.transform.up * forceDir * antiRollForce, rightWheelColliders.transform.position);
    }

    public void ApplySteerAngle(float steerAngle, float wheelBaseLenght)
    {
        if (isSteer == false) return;

        float radius = Mathf.Abs(wheelBaseLenght * Mathf.Tan(Mathf.Deg2Rad * ( 90 - Mathf.Abs(steerAngle))));
        float angleSing = Mathf.Sign(steerAngle);

        if (steerAngle > 0)
        {
            leftWheelColliders.steerAngle = Mathf.Rad2Deg * Mathf.Atan(wheelBaseLenght / (radius + (wheelWidth * 0.5f))) * angleSing;
            rightWheelColliders.steerAngle = Mathf.Rad2Deg * Mathf.Atan(wheelBaseLenght / (radius - (wheelWidth * 0.5f))) * angleSing; ;
        }
        else if (steerAngle < 0)
        {
            leftWheelColliders.steerAngle = Mathf.Rad2Deg * Mathf.Atan(wheelBaseLenght / (radius - (wheelWidth * 0.5f))) * angleSing;
            rightWheelColliders.steerAngle = Mathf.Rad2Deg * Mathf.Atan(wheelBaseLenght / (radius + (wheelWidth * 0.5f))) * angleSing;
        }
        else
        {
            leftWheelColliders.steerAngle = 0;
            rightWheelColliders.steerAngle = 0;
        }        
    }

    public void ApplyMotorTorque(float motorTorque)
    {
        if (isMotor == false) return;

        leftWheelColliders.motorTorque = motorTorque;
        rightWheelColliders.motorTorque = motorTorque;
    }

    public void ApplyBreakTorque(float brakeTorque)
    {
        leftWheelColliders.brakeTorque = brakeTorque;
        rightWheelColliders.brakeTorque = brakeTorque;
    }

    public float GetAvarageRpm()
    {
        return (leftWheelColliders.rpm + rightWheelColliders.rpm) * 0.5f;
    }

    public float GetRadius()
    {
        return leftWheelColliders.radius;
    }

    //Private
    private void SyncMeshTransform()
    {
        UpdateWheelTransform(leftWheelColliders, leftWheelMesh);
        UpdateWheelTransform(rightWheelColliders, rightWheelMesh);
    }

    private void UpdateWheelTransform(WheelCollider wheelCollider, Transform wheelTransform)
    {
        Vector3 position;
        Quaternion rotation;

        wheelCollider.GetWorldPose(out position, out rotation);
        wheelTransform.position = position;
        wheelTransform.rotation = rotation;
    }
}
