using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelActivator : MonoBehaviour
{
    public const string raceCompleteSaveMark = "_race_comlete";

    [SerializeField] private LevelActivator rootLevel;

    private UISelectableButton button;

    public bool isCompleted;
    private void Awake()
    {
        button = GetComponent<UISelectableButton>();

        if ((button as RaceUIButton) != null)
        {
            if (PlayerPrefs.GetString((button as RaceUIButton).RaceInfo.SceneName + raceCompleteSaveMark) == "true")
                isCompleted = true;
        }
    }
    private void Start()
    {
        if (rootLevel == null)
            button.TurnOnInteractible();
        else
        {
            if (button != null)
            {
                if (rootLevel.isCompleted)
                    button.TurnOnInteractible();
                else
                    button.TurnOffInteractible();

            }
        }           
    }
}
