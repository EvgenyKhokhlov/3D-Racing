using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ResolutionSetting : Setting
{
    [SerializeField]
    private Vector2Int[] avalibleResolutions = new Vector2Int[]
    {
        new Vector2Int(800, 600),
        new Vector2Int(1280, 720),
        new Vector2Int(1600, 900),
        new Vector2Int(1920, 1080),
    };

    private int currentResolutionIndex = 0;

    public override bool isMinValue { get => currentResolutionIndex == 0; }
    public override bool isMaxValue { get => currentResolutionIndex == avalibleResolutions.Length - 1; }

    public override void SetNextValue()
    {
        if (isMaxValue == false)
        {
            currentResolutionIndex++;
        }
    }

    public override void SetPreviousValue()
    {
        if (isMinValue == false)
        {
            currentResolutionIndex--;
        }
    }

    public override object GetValue()
    {
        return avalibleResolutions[currentResolutionIndex];
    }

    public override string GetStringValue()
    {
        return avalibleResolutions[currentResolutionIndex].x + "x" + avalibleResolutions[currentResolutionIndex].y;
    }

    public override void Apply()
    {
        Screen.SetResolution(avalibleResolutions[currentResolutionIndex].x, avalibleResolutions[currentResolutionIndex].y, true);

        Save();
    }

    public override void Load()
    {
        currentResolutionIndex = PlayerPrefs.GetInt(title, avalibleResolutions.Length - 1);
    }

    private void Save()
    {
        PlayerPrefs.SetInt(title, currentResolutionIndex);
    }
}
