using UnityEngine;
using UnityEngine.UI;

public class TimerUI : HatUI
{
    [SerializeField] private Image _fillImage;
    [SerializeField] private GameObject _root;
    
    public void SetPercentage(float percentage)
    {
        _fillImage.fillAmount = percentage;
    }
    
    protected override void OnShow()
    {
        _root.SetActive(true);
    }

    protected override void OnHide()
    {
        _root.SetActive(false);
    }
}
