using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class UIButtonSound : MonoBehaviour
{
    [SerializeField] private AudioClip click; 
    [SerializeField] private AudioClip hover; 

    private new AudioSource audio;

    private UIButton[] uiButtons;
    private void Start()
    {
        audio = GetComponent<AudioSource>();

        uiButtons = GetComponentsInChildren<UIButton>(true);

        for (int i = 0; i < uiButtons.Length; i++)
        {
            uiButtons[i].PointerEnter += OnPointerEnter;
            uiButtons[i].PointerClick += OnPointerClicked;
        }
    }

    private void OnDestroy()
    {
        for (int i = 0; i < uiButtons.Length; i++)
        {
            uiButtons[i].PointerEnter -= OnPointerEnter;
            uiButtons[i].PointerClick -= OnPointerClicked;
        }
    }

    private void OnPointerClicked(UIButton button)
    {
        audio.PlayOneShot(hover);
    }

    private void OnPointerEnter(UIButton button)
    {
        audio.PlayOneShot(click);
    }
}
