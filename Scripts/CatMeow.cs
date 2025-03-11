using UnityEngine;

public class CatMeow : MonoBehaviour
{
    public AudioSource aud;

    private void OnEnable() {
        InvokeRepeating("PlaySound", 7.2f, 7.2f);
    }

    private void OnDisable() {
        CancelInvoke("PlaySound");
    }

    private void PlaySound() {
        aud.Play();
    }
}