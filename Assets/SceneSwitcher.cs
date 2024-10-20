using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    // Name of the scene to load
    public string sceneName;

    // This function will be called when the button is clicked
    public void SwitchScene()
    {
        // Load the scene by name
        SceneManager.LoadScene(sceneName);
    }
}