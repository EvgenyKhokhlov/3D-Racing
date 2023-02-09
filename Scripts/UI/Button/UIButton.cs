using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class UIButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] protected bool Interactable;

    private bool focuse = false;
    public bool Focus => focuse;

    public UnityEvent OnClick;

    public event UnityAction<UIButton> PointerEnter;
    public event UnityAction<UIButton> PointerExit;
    public event UnityAction<UIButton> PointerClick;

    public virtual void SetFocus()
    {
        if (Interactable == false) return;

        focuse = true;
    }

    public virtual void SetUnFocus()
    {
        if (Interactable == false) return;

        focuse = false;
    }

    public virtual void OnPointerEnter(PointerEventData eventData)
    {
        if (Interactable == false) return;

        PointerEnter?.Invoke(this);
    }

    public virtual void OnPointerExit(PointerEventData eventData)
    {
        if (Interactable == false) return;

        PointerExit?.Invoke(this);
    }

    public virtual void OnPointerClick(PointerEventData eventData)
    {
        if (Interactable == false) return;
 
        PointerClick?.Invoke(this);
        OnClick?.Invoke();
    }

    public virtual void TurnOffInteractible() { }
    public virtual void TurnOnInteractible() { }

}
