using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public class CharacterPerceptionAI : MonoBehaviour
{
    [SerializeField] private float _lookDistance = 5f;
    [SerializeField] private float _lookAngle = 180f;
    [SerializeField] private int _angleDivision = 10;
    [SerializeField] private string _checkTag;
    [SerializeField] private LayerMask _layer;

    [SerializeField] private AlertUI _alert;

    // Update is called once per frame
    private void FixedUpdate()
    {
        var direction = transform.forward;
        var foundTarget = Raycast(direction);
        
        for (var i = 0; i < _angleDivision && !foundTarget; i++)
        {
            var angle = _lookAngle / 2 / _angleDivision * (i + 1);
            var currentDirection = Quaternion.AngleAxis(angle, Vector3.up) * direction;
            foundTarget = Raycast(currentDirection);

            if (foundTarget) continue;
            currentDirection = Quaternion.AngleAxis(-angle, Vector3.up) * direction;
            foundTarget = Raycast(currentDirection);
        }

        if (foundTarget)
        {
            _alert.Show();
        }
    }

    private bool Raycast(Vector3 direction)
    {
        var currentTransform = transform;
        var position = currentTransform.position;
        var ray = new Ray(position, direction);
        
        Debug.DrawLine(position, position + direction * _lookDistance, Color.red, Time.deltaTime);

        if (Physics.Raycast(ray, out var hit, _lookDistance, _layer.value))
        {
            return hit.collider.gameObject.tag.Equals(_checkTag);
        }
        return false;
    }
}
