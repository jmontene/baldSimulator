using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    [SerializeField] private string _sceneName;
    [SerializeField] private bool _currentScene;
    
    private void Awake()
    {
        var button = GetComponent<Button>();
        button.onClick.AddListener(() =>
        {
            if (_currentScene)
            {
                SceneManager.LoadScene(gameObject.scene.name);
                return;
            }
            
            SceneManager.LoadScene(_sceneName);
        });
    }
}
