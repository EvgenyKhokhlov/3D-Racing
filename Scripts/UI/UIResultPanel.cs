using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIResultPanel : MonoBehaviour, IDependency<RaceStateTracker>, IDependency<RaceResult>, IDependency<RaceTimeTracker>
{
    [SerializeField] private GameObject resultPanelObject;
    [SerializeField] private GameObject levelPassedPanelObject;
    [SerializeField] private Text goldRecordText;
    [SerializeField] private Text playerRecordText;

    private RaceStateTracker raceStateTracker;
    public void Construct(RaceStateTracker obj) => raceStateTracker = obj;

    private RaceResult raceResultTime;
    public void Construct(RaceResult obj) => raceResultTime = obj;

    private RaceTimeTracker raceTimeTracker;
    public void Construct(RaceTimeTracker obj) => raceTimeTracker = obj;

    private void Start()
    {
        raceStateTracker.Completed += OnRaceCompleted;

        resultPanelObject.SetActive(false);
        levelPassedPanelObject.SetActive(false);
    }
    private void OnDestroy()
    {
        raceStateTracker.Completed -= OnRaceCompleted;
    }

    private void OnRaceCompleted()
    {
        resultPanelObject.SetActive(true);
    
        playerRecordText.text = StringTime.SecondToTimeString(raceTimeTracker.CurrentTime);

        if (raceResultTime.PlayerRecordTime > raceResultTime.GoldTime || raceResultTime.RecordWasSet == false)
        {
            goldRecordText.text = StringTime.SecondToTimeString(raceResultTime.GoldTime);
        }

        if (raceResultTime.RecordWasSet == true)
        {
            goldRecordText.text = StringTime.SecondToTimeString(raceResultTime.PlayerRecordTime);
        }

        if (raceTimeTracker.CurrentTime < raceResultTime.GoldTime)
            levelPassedPanelObject.SetActive(true);
    }
}
