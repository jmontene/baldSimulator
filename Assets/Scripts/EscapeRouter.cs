using System.Collections.Generic;
using UnityEngine;

public class EscapeRouter : MonoBehaviour
{
    public bool TryGetClosestEscapePoint(Vector3 position, out Vector3 escapePoint)
    {
        escapePoint = position;
        if (transform.childCount == 0) return false;
        
        var minDistance = float.MaxValue;
        escapePoint = transform.GetChild(0).position;

        for (var i = 1; i < transform.childCount; i++)
        {
            var currentPoint = transform.GetChild(i).position;
            var distance = Vector3.Distance(currentPoint, position);
            if (!(distance < minDistance)) continue;
            minDistance = distance;
            escapePoint = currentPoint;
        }

        return true;
    }
}
