using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaceInputController : MonoBehaviour, IDependency<RaceStateTracker>, IDependency<CarInputControl>
{
    private CarInputControl carControl;
    public void Construct(CarInputControl obj) => carControl = obj;

    private RaceStateTracker raceStateTracker;
    public void Construct(RaceStateTracker obj) => raceStateTracker = obj;

    private void Start()
    {
        raceStateTracker.Started += OnRaceStarted;
        raceStateTracker.Completed += OnRaceCompleted;

        carControl.enabled = false;
    }

    private void OnDestroy()
    {
        raceStateTracker.Started -= OnRaceStarted;
        raceStateTracker.Completed -= OnRaceCompleted;
    }
    private void OnRaceStarted()
    {
        carControl.enabled = true;
    }
    private void OnRaceCompleted()
    {
        carControl.Stop();
        carControl.enabled = false;
    }
}
