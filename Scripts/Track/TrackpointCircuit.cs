using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum TrackType
{
    Circular,
    Sprint
}

public class TrackpointCircuit : MonoBehaviour
{
    public event UnityAction<TrackPoint> TrackPointTriggered;
    public event UnityAction<int> LapCopmleted;

    [SerializeField] private TrackType type;
    public TrackType Type => type;

    private TrackPoint[] points;

    private int LapsCopmleted = -1;

    private void Awake()
    {
        BuildCircuit();
    }
    private void Start()
    {
        for (int i = 0; i < points.Length; i++)
        {
            points[i].Triggered += OnTrackPointTriggered;
        }

        points[0].AssignedAsTarget();
    }
    private void OnDestroy()
    {
        for (int i = 0; i < points.Length; i++)
        {
            points[i].Triggered -= OnTrackPointTriggered;
        }
    }
    private void OnTrackPointTriggered(TrackPoint trackPoint)
    {
        if (trackPoint.IsTarget == false) return;

        trackPoint.Passed();
        trackPoint.Next?.AssignedAsTarget();

        TrackPointTriggered?.Invoke(trackPoint);

        if ( trackPoint.IsLast == true)
        {
            LapsCopmleted++;

            if (type == TrackType.Sprint)
                LapCopmleted?.Invoke(LapsCopmleted);

            if (type == TrackType.Circular)
                if(LapsCopmleted > 0)
                    LapCopmleted?.Invoke(LapsCopmleted);
        }        
    }

    [ContextMenu(nameof(BuildCircuit))]
    private void BuildCircuit()
    {
        points = TrackCircuitBuilder.Build(transform, type);
    }
}
