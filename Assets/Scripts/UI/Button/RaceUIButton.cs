using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RaceUIButton : UISelectableButton, IScriptableObjectPropety
{
    [SerializeField] private RaceInfo raceInfo;
    [SerializeField] private Image icon;
    [SerializeField] private Text title;

    public RaceInfo RaceInfo => raceInfo;

    private void Start()
    {
        ApplyProperty(raceInfo);
    }

    public override void OnPointerClick(PointerEventData eventData)
    {
        base.OnPointerClick(eventData);

        if (raceInfo == null) return;
        if (Interactable == false) return;

        SceneManager.LoadScene(raceInfo.SceneName);
    }

    public void ApplyProperty(ScriptableObject property)
    {
        if (property == null) return;

        if (property is RaceInfo == false) return;

        raceInfo = property as RaceInfo;

        icon.sprite = raceInfo.Icon;
        title.text = raceInfo.Title;
    }
}
