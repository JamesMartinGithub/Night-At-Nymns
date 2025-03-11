using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public string sceneName;
    public bool onEnable = false;
    public bool unlockFreeroam = false;

    public void LoadScene() {
        if (unlockFreeroam) {
            GameObject.Find("DontDestroyOnLoad").GetComponent<GameVariables>().freeroamUnlocked = true;
        }
        SceneManager.LoadScene(sceneName);
    }

    private void OnEnable() {
        if (onEnable) {
            LoadScene();
        }
    }
}