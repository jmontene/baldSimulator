using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public class CharacterPerceptionAI : MonoBehaviour
{
    [SerializeField] private float _lookDistance = 5f;
    [SerializeField] private float _lookAngle = 180f;
    [SerializeField] private int _angleDivision = 10;

    // Update is called once per frame
    private void Update()
    {
        var direction = transform.forward;
        Raycast(direction);
        
        for (var i = 0; i < _angleDivision; i++)
        {
            var angle = _lookAngle / 2 / _angleDivision * (i + 1);
            var currentDirection = Quaternion.AngleAxis(angle, Vector3.up) * direction;
            Raycast(currentDirection);
            currentDirection = Quaternion.AngleAxis(-angle, Vector3.up) * direction;
            Raycast(currentDirection);
        }
    }

    private void Raycast(Vector3 direction)
    {
        var currentTransform = transform;
        var position = currentTransform.position;
        var ray = new Ray(position, currentTransform.forward);
        
        Debug.DrawRay(position, direction, Color.red, Time.deltaTime);
        
        if (Physics.Raycast(ray, out var hit, _lookDistance))
        {
            
        }
    }
}
