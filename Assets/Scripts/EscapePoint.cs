using System;
using UnityEngine;

public class EscapePoint : MonoBehaviour
{
    private GameManager _gameManager;

    private void Awake()
    {
        _gameManager = FindObjectOfType<GameManager>();
    }

    private void OnTriggerStay(Collider other)
    {
        var person = other.GetComponent<PersonAI>();
        if (person == null || !person.IsEscaping()) return;
        _gameManager.OnPersonEscaped(person);
        Destroy(other.gameObject);
    }
}