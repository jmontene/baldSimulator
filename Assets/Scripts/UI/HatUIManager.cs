using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HatUIManager : MonoBehaviour
{
    private HatUI[] _hatUIs;

    private void Awake()
    {
        _hatUIs = GetComponentsInChildren<HatUI>();
        foreach (var currentUI in _hatUIs)
        {
            currentUI.Manager = this;
        }
    }

    public void OnShowUI(HatUI hatUI)
    {
        foreach (var currentUI in _hatUIs)
        {
            if (currentUI == hatUI) continue;
            currentUI.Hide();
        }
    }
}
