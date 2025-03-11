using System.Collections;
using UnityEngine;

public class TorchFlicker : MonoBehaviour
{
    public AudioSource audOn;
    public AudioSource audOff;
    private Light torch;
    private float maxIntensity;

    private void Awake() {
        torch = GetComponent<Light>();
        maxIntensity = torch.intensity;
    }

    private void OnEnable() {
        torch.intensity = maxIntensity;
        StartCoroutine("Flicker");
    }

    private IEnumerator Flicker() {
        while (true) {
            yield return new WaitForSeconds(Random.Range(3.0f, 7.0f));
            if (torch.enabled) {
                audOff.Play();
            }
            torch.intensity = 0.1f;
            yield return new WaitForSeconds(Random.Range(0.04f, 0.8f));
            if (torch.enabled) {
                audOn.Play();
            }
            torch.intensity = maxIntensity;
        }
    }

    private void OnDisable() {
        StopCoroutine("Flicker");
        torch.intensity = maxIntensity;
    }
}