using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackToTrackNotice : MonoBehaviour
{
    [SerializeField] private GameObject backToTrackNotice;

    private void Start()
    {
        backToTrackNotice.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        var carCollider = other.GetComponentInParent<Car>();

        if (carCollider != null)
            backToTrackNotice.SetActive(true);
    }

    private void OnTriggerExit(Collider other)
    {
        var carCollider = other.GetComponentInParent<Car>();

        if (carCollider != null)
            backToTrackNotice.SetActive(false);
    }
}
