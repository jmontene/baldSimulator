using UnityEngine;
using UnityEngine.UI;

public class TimerUI : MonoBehaviour
{
    [SerializeField] private Image _fillImage;

    public void SetPercentage(float percentage)
    {
        _fillImage.fillAmount = percentage;
    }
}
