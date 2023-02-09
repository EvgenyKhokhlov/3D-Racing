using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(CarChassis))]
public class Car : MonoBehaviour
{
    public event UnityAction GearChanged;

    [SerializeField] private float maxSteerTorque;
    [SerializeField] private float maxBrakeTorque;

    [Header("Engine")]
    [SerializeField] private AnimationCurve engineTorqueCurve;
    [SerializeField] private float engineMaxTorque;
    [SerializeField] private float engineTorque;
    [SerializeField] private float engineRpm;
    public float EngineRpm => engineRpm;
    [SerializeField] private float engineMinRpm;
    [SerializeField] private float engineMaxRpm;
    public float EngineMaxRpm => engineMaxRpm;

    [Header("Gerabox")]
    [SerializeField] private float[] gears;
    [SerializeField] private float finalDriveRation;

    [SerializeField] private int selectedGearIndex;
    public int SelectedGearIndex => selectedGearIndex;

    [SerializeField] private float selectedGear;
    public float SelectedGear => selectedGear;
    [SerializeField] private float rearGear;

    [SerializeField] private float upShiftEngineRpm;
    [SerializeField] private float downShiftEngineRpm;

    [SerializeField] private float maxSpeed;

    public float LineraVelocity => chassis.LinearVelocity;
    public float NormalizeLineraVelocity => chassis.LinearVelocity / maxSpeed;
    public float WheelSpeed => chassis.GetWheelSpeed();
    public float MaxSpeed => maxSpeed;

    private CarChassis chassis;
    public Rigidbody Rigidbody => chassis == null ? GetComponent<Rigidbody>() : chassis.Rigidbody;

    //DEBUG
    [SerializeField] public float linearVelocity;
    public float ThrottleControl;
    public float SteerControl;
    public float BrakeControl;

    private void Start()
    {
        chassis = GetComponent<CarChassis>();
    }

    private void Update()
    {
        linearVelocity = LineraVelocity;

        UpdateEngineTorque();
        AutoGearShift();

        if (LineraVelocity >= maxSpeed) engineTorque = 0;

        chassis.MotorTorque = engineTorque * ThrottleControl;
        chassis.SteerAngle = maxSteerTorque * SteerControl;
        chassis.BreakTorque = maxBrakeTorque * BrakeControl;
    }

    private void AutoGearShift()
    {
        if (selectedGear < 0) return;

        if (engineRpm >= upShiftEngineRpm)        
            UpGear();
                               
        if (engineRpm < downShiftEngineRpm)
            DownGear();

        selectedGearIndex = Mathf.Clamp(selectedGearIndex, 0, gears.Length - 1);
    }
    public void UpGear()
    {
        ShiftGear(selectedGearIndex + 1);
        GearChanged?.Invoke();
    }
    public void DownGear()
    {
        ShiftGear(selectedGearIndex - 1);
    }
    public void ShiftToRearGear()
    {
        selectedGear = rearGear;
        GearChanged?.Invoke();
    }
    public void ShiftToFirstGear()
    {
        ShiftGear(0);
    }
    public void ShiftToNetral()
    {
        selectedGear = 0;
    }

    private void ShiftGear(int gearIndex)
    { 
        gearIndex = Mathf.Clamp(gearIndex, 0, gears.Length - 1);
        selectedGear = gears[gearIndex];
        selectedGearIndex = gearIndex;
    }

    private void UpdateEngineTorque()
    {
        engineRpm = engineMinRpm + Mathf.Abs(chassis.GetAvarageRpm() * selectedGear * finalDriveRation);
        engineRpm = Mathf.Clamp(engineRpm, engineMinRpm, engineMaxRpm);

        engineTorque = engineTorqueCurve.Evaluate(engineRpm / engineMaxRpm) * engineMaxTorque
                                                    * finalDriveRation * Mathf.Sign(selectedGear);
    }

    public void Reset()
    {
        chassis.Reset();

        chassis.MotorTorque = 0;
        chassis.BreakTorque = 0;
        chassis.SteerAngle = 0;

        ThrottleControl = 0;
        BrakeControl = 0;
        SteerControl = 0;
    }
    public void Respawn(Vector3 position, Quaternion rotation)
    {
        Reset();

        transform.position = position;
        transform.rotation = rotation;
    }
}
