using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class RaceResult : MonoBehaviour, IDependency<RaceStateTracker>, IDependency<RaceTimeTracker>
{
    public const string timeRecordSaveMark = "_player_best_time";
    public const string raceCompleteSaveMark = "_race_comlete";

    public event Action ResultUpdate;

    [SerializeField] private float goldTime;
    private float playerRecordTime;
    private float currentTime;

    public float GoldTime => goldTime;
    public float PlayerRecordTime => playerRecordTime;
    public float CurrentTime => currentTime;
    public bool RecordWasSet => playerRecordTime != 0;

    private RaceStateTracker raceStateTracker;
    public void Construct(RaceStateTracker obj) => raceStateTracker = obj;

    private RaceTimeTracker raceTimeTracker;
    public void Construct(RaceTimeTracker obj) => raceTimeTracker = obj;

    private void Awake()
    {
        LoadRecordTime();
    }
    private void Start()
    {
        raceStateTracker.Completed += OnRaceCompleted;
    }
    private void OnDestroy()
    {
        raceStateTracker.Completed -= OnRaceCompleted;
    }

    private void OnRaceCompleted()
    {
        float absoluteRecord = GetAbsoluteRecord();

        if (raceTimeTracker.CurrentTime < goldTime)
            SaveRaceComlete();

        if (raceTimeTracker.CurrentTime < absoluteRecord || playerRecordTime == 0)
        {
            playerRecordTime = raceTimeTracker.CurrentTime;

            SaveRecordTime();
        }

        currentTime = raceTimeTracker.CurrentTime;

        ResultUpdate?.Invoke();
    }

    public float GetAbsoluteRecord()
    {
        if (playerRecordTime < goldTime && playerRecordTime != 0)
            return playerRecordTime;      
        else
            return goldTime;
    }

    private void LoadRecordTime()
    {
        playerRecordTime = PlayerPrefs.GetFloat(SceneManager.GetActiveScene().name + timeRecordSaveMark, 0);
    }

    private void SaveRecordTime()
    {
        PlayerPrefs.SetFloat(SceneManager.GetActiveScene().name + timeRecordSaveMark, playerRecordTime);
    }

    private void SaveRaceComlete()
    {
        PlayerPrefs.SetString(SceneManager.GetActiveScene().name + raceCompleteSaveMark, "true");
    }
}
