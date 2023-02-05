using UnityEngine;

public class EscapePoint : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        var person = other.GetComponent<PersonAI>();
        if (person != null && person.IsEscaping())
        {
            Destroy(other.gameObject);
        }
    }
}