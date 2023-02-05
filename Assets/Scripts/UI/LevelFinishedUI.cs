using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelFinishedUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _scoreText;

    private GameManager _gameManager;
    
    // Start is called before the first frame update
    void Awake()
    {
        _gameManager = FindObjectOfType<GameManager>();

        _scoreText.text = $"{_gameManager.EscapedPersonsBald} / {_gameManager.PersonsCount}";
    }
}
