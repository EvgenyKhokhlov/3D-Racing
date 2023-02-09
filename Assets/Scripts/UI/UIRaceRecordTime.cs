using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIRaceRecordTime : MonoBehaviour, IDependency<RaceStateTracker>, IDependency<RaceResult>
{
    [SerializeField] private GameObject goldRecordObject;
    [SerializeField] private GameObject playerRecordObject;
    [SerializeField] private Text goldRecordText;
    [SerializeField] private Text playerRecordText;

    private RaceStateTracker raceStateTracker;
    public void Construct(RaceStateTracker obj) => raceStateTracker = obj;

    private RaceResult raceResultTime;
    public void Construct(RaceResult obj) => raceResultTime = obj;

    private void Start()
    {
        raceStateTracker.Started += OnRaceStarted;
        raceStateTracker.Completed += OnRaceCompleted;

        goldRecordObject.SetActive(false);
        playerRecordObject.SetActive(false);
    }

    private void OnDestroy()
    {
        raceStateTracker.Started -= OnRaceStarted;
        raceStateTracker.Completed -= OnRaceCompleted;
    }
    private void OnRaceStarted()
    {
        if (raceResultTime.PlayerRecordTime > raceResultTime.GoldTime || raceResultTime.RecordWasSet == false)
        {
            goldRecordObject.SetActive(true);
            goldRecordText.text = StringTime.SecondToTimeString(raceResultTime.GoldTime);
        }

        if (raceResultTime.RecordWasSet == true)
        {
            playerRecordObject.SetActive(true);
            playerRecordText.text = StringTime.SecondToTimeString(raceResultTime.PlayerRecordTime);
        }
    }

    private void OnRaceCompleted()
    {
        goldRecordObject.SetActive(false);
        playerRecordObject.SetActive(false);
    }
}
