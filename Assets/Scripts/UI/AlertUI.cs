using UnityEngine;

public class AlertUI : MonoBehaviour
{
    [SerializeField] private GameObject _root;
    [SerializeField] private float _hideDelay = 3f;

    private float _timeRemaining;

    public void Show()
    {
        _root.SetActive(true);
        _timeRemaining = _hideDelay;
    }

    private void Hide()
    {
        _root.SetActive(false);
    }

    // Update is called once per frame
    private void Update()
    {
        if (!_root.activeInHierarchy) return;

        if (_timeRemaining > 0)
        {
            _timeRemaining -= Time.deltaTime;
            return;
        }

        Hide();
    }
}
