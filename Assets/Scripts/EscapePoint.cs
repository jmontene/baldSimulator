using UnityEngine;

public class EscapePoint : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        var person = other.GetComponent<PersonAI>();
        if (person != null && person.IsEscaping())
        {
            Destroy(other.gameObject);
        }
    }
}