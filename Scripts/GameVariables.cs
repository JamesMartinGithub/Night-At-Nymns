using UnityEngine;

public class GameVariables : MonoBehaviour
{
    public float volumeSliderVal;
    public bool freeroamUnlocked = false;
    public float senseVal = 1;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}