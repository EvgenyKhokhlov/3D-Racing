using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPressEnterText : MonoBehaviour, IDependency<RaceStateTracker>
{
    [SerializeField] private GameObject helpTextObject;

    private RaceStateTracker raceStateTracker;
    public void Construct(RaceStateTracker obj) => raceStateTracker = obj;

    private void Start()
    {
        raceStateTracker.PreparationStarted += OnPreparationStarted;

        helpTextObject.SetActive(true);
 
    }

    private void OnDestroy()
    {
        raceStateTracker.PreparationStarted -= OnPreparationStarted;
    }
    private void OnPreparationStarted()
    {
        helpTextObject.SetActive(false);
    }
}
