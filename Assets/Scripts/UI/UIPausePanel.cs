using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPausePanel : MonoBehaviour, IDependency<Pauser>
{
    [SerializeField] private GameObject panel;

    private Pauser pauser;
    public void Construct(Pauser obj) => pauser = obj;

    private void Start()
    {
        panel.SetActive(false);
        pauser.PauseStateChange += onPauseStateChange;
    }

    private void OnDestroy()
    {
        pauser.PauseStateChange -= onPauseStateChange;
    }
    private void onPauseStateChange(bool isPause)
    {
        panel.SetActive(isPause);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) == true)
        {
            pauser.ChangePauseState();
        }
    }

    public void UnPause()
    {
        pauser.UnPause();
    }
}
