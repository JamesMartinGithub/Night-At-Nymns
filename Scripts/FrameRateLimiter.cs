using UnityEngine;

public class FrameRateLimiter : MonoBehaviour
{
    void Start() {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;
        Application.runInBackground = false;
    }
}