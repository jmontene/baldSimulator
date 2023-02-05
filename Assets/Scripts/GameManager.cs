using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject _gameFinishedUI;
    
    private int _personsCount;
    private int _escapedPersonsCount;
    private int _escapedPersonsBald;
    
    public int PersonsCount
    {
        get => _personsCount;
        set => _personsCount = value;
    }

    public int EscapedPersonsCount
    {
        get => _escapedPersonsCount;
        set => _escapedPersonsCount = value;
    }

    public int EscapedPersonsBald
    {
        get => _escapedPersonsBald;
        set => _escapedPersonsBald = value;
    }

    // Start is called before the first frame update
    private void Awake()
    {
        _personsCount = FindObjectsOfType<PersonAI>().Length;
    }

    public void OnPersonEscaped(PersonAI person)
    {
        if (!person.HasHair)
        {
            _escapedPersonsBald++;
        }

        _escapedPersonsCount++;

        if (IsGameFinished())
        {
            OnGameFinished();
        }
    }

    private void OnGameFinished()
    {
        Instantiate(_gameFinishedUI);
    }

    public bool IsGameFinished()
    {
        return _escapedPersonsCount == _personsCount;
    }
}
