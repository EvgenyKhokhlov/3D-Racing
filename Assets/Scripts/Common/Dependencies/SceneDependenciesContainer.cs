using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SceneDependenciesContainer : Dependency
{
    [SerializeField] private RaceStateTracker raceStateTracker;
    [SerializeField] private CarInputControl carInputControl;
    [SerializeField] private TrackpointCircuit trackpointCircuit;
    [SerializeField] private Car car;
    [SerializeField] private CarCameraController carCameraController;
    [SerializeField] private RaceTimeTracker raceTimeTracker;
    [SerializeField] private RaceResult raceResultTime;

    protected override void BindAll(MonoBehaviour monoBehaviorInScene)
    {
        Bind<RaceStateTracker>(raceStateTracker, monoBehaviorInScene);
        Bind<CarInputControl>(carInputControl, monoBehaviorInScene);
        Bind<TrackpointCircuit>(trackpointCircuit, monoBehaviorInScene);
        Bind<Car>(car, monoBehaviorInScene);
        Bind<CarCameraController>(carCameraController, monoBehaviorInScene);
        Bind<RaceTimeTracker>(raceTimeTracker, monoBehaviorInScene);
        Bind<RaceResult>(raceResultTime, monoBehaviorInScene);
}

    private void Awake()
    {
        FindAllObjectToBind();
    }
}
