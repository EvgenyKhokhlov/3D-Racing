using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EngineAndGearsIndicator : MonoBehaviour
{
    [SerializeField] private Car car;
    [SerializeField] private Text text;
    [SerializeField] private Image image;

    private void Update()
    {
        text.text = (car.SelectedGearIndex + 1).ToString();
        if (car.SelectedGear < 0) text.text = "R";

        image.fillAmount = car.EngineRpm / car.EngineMaxRpm;

        if (image.fillAmount < 0.8) image.color = Color.white;
        if (image.fillAmount >= 0.8 && image.fillAmount < 0.9) image.color = Color.yellow;
        if (image.fillAmount >= 0.9) image.color = Color.red;
    }
}
