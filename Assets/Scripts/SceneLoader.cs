using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    // Start is called before the first frame update
    public void LoadScene(string sceneName){
        SceneManager.LoadScene(sceneName);
    }
}