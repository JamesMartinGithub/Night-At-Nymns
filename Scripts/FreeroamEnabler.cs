using UnityEngine;

public class FreeroamEnabler : MonoBehaviour
{
    public GameObject freeroamButton;

    private void Awake() {
        if (GameObject.Find("DontDestroyOnLoad").GetComponent<GameVariables>().freeroamUnlocked) {
            freeroamButton.SetActive(true);
        }
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}