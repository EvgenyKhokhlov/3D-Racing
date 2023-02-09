using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISelectableButtonContainer : MonoBehaviour
{
    [SerializeField] private Transform buttonContainer;

    public bool Interactable = true;

    public void SetInteractable(bool interactable) => Interactable = interactable;

    private UISelectableButton[] buttons;

    private int selectButtonIndex = 0;

    private void Start()
    {
        buttons = buttonContainer.GetComponentsInChildren<UISelectableButton>();

        if (buttons == null)
            Debug.Log("Button list is empty!");

        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].PointerEnter += OnPointerEnter;
        }

        if (Interactable == false) return;

        buttons[selectButtonIndex].SetFocus();
    }

    private void OnDestroy()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].PointerEnter -= OnPointerEnter;
        }
    }
    private void OnPointerEnter(UIButton button)
    {
        SelectButton(button);
    }

    private void SelectButton(UIButton button)
    {
        if (Interactable == false) return;

        buttons[selectButtonIndex].SetUnFocus();

        for (int i = 0; i < buttons.Length; i++)
        {
            if (button == buttons[i])
            {
                selectButtonIndex = i;
                button.SetFocus();
                break;
            }
        }
    }

    public void SelectNext()
    {
        //TODO
    }

    public void SelectPrevious()
    {
        //TODO
    }
}
