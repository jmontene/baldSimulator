using UnityEngine;

public class TimedDestroyer : MonoBehaviour
{
    [SerializeField] private float _delay = 3f;

    // Update is called once per frame
    private void Update()
    {
        if (_delay > 0f)
        {
            _delay -= Time.deltaTime;
            return;
        }
        
        Destroy(gameObject);
    }
}
