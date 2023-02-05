using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class HatUI : MonoBehaviour
{
    public HatUIManager Manager { get; set; }

    protected abstract void OnShow();
    protected abstract void OnHide();

    public void Show()
    {
        if (Manager != null)
        {
            Manager.OnShowUI(this);
        }
        
        OnShow();
    }

    public void Hide()
    {
        OnHide();
    }
}
