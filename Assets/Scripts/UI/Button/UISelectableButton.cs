using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class UISelectableButton : UIButton
{
    [SerializeField] private Image selectImage;
    [SerializeField] private Image imageToHide;

    public UnityEvent OnSelect;
    public UnityEvent OnUnSelect;

    public override void SetFocus()
    {
        base.SetFocus();

        selectImage.enabled = true;
        OnSelect?.Invoke();
    }

    public override void SetUnFocus()
    {
        base.SetUnFocus();

        selectImage.enabled = false;
        OnUnSelect?.Invoke();
    }
    public override void TurnOffInteractible()
    {
        Interactable = false;

        if (imageToHide != null)
            imageToHide.enabled = true;
    }

    public override void TurnOnInteractible()
    {
        Interactable = true;

        if (imageToHide != null)
            imageToHide.enabled = false;
    }
}
