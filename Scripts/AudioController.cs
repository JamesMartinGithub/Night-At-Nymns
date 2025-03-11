using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

public class AudioController : MonoBehaviour
{
    public AudioMixer mixer;
    private (float, float) lowpassFreqs = (3300, 22000);
    private (float, float) eqGain = (0.55f, 1f);

    private void Start() {
        mixer.SetFloat("CutoffFreq", lowpassFreqs.Item2);
        mixer.SetFloat("FreqGain", eqGain.Item2);
        mixer.SetFloat("CutoffFreq2", lowpassFreqs.Item1);
        mixer.SetFloat("FreqGain2", eqGain.Item1);
    }

    public void TransitionOutside() {
        
        StartCoroutine("EnumerateOutside");
    }
    private IEnumerator EnumerateOutside() {
        for (float t = 0; t < 1; t += 0.02f) {
            yield return new WaitForSeconds(0.01f);
            mixer.SetFloat("CutoffFreq", Mathf.Lerp(lowpassFreqs.Item1, lowpassFreqs.Item2, t));
            mixer.SetFloat("FreqGain", Mathf.Lerp(eqGain.Item1, eqGain.Item2, t));
            mixer.SetFloat("CutoffFreq2", Mathf.Lerp(lowpassFreqs.Item2, lowpassFreqs.Item1, t));
            mixer.SetFloat("FreqGain2", Mathf.Lerp(eqGain.Item2, eqGain.Item1, t));
        }
        mixer.SetFloat("CutoffFreq", lowpassFreqs.Item2);
        mixer.SetFloat("FreqGain", eqGain.Item2);
        mixer.SetFloat("CutoffFreq2", lowpassFreqs.Item1);
        mixer.SetFloat("FreqGain2", eqGain.Item1);
    }

    public void TransitionInside() {
        StartCoroutine("EnumerateInside");
    }
    private IEnumerator EnumerateInside() {
        for (float t = 0; t < 1; t += 0.02f) {
            yield return new WaitForSeconds(0.01f);
            mixer.SetFloat("CutoffFreq", Mathf.Lerp(lowpassFreqs.Item2, lowpassFreqs.Item1, t));
            mixer.SetFloat("FreqGain", Mathf.Lerp(eqGain.Item2, eqGain.Item1, t));
            mixer.SetFloat("CutoffFreq2", Mathf.Lerp(lowpassFreqs.Item1, lowpassFreqs.Item2, t));
            mixer.SetFloat("FreqGain2", Mathf.Lerp(eqGain.Item1, eqGain.Item2, t));
        }
        mixer.SetFloat("CutoffFreq", lowpassFreqs.Item1);
        mixer.SetFloat("FreqGain", eqGain.Item1);
        mixer.SetFloat("CutoffFreq2", lowpassFreqs.Item2);
        mixer.SetFloat("FreqGain2", eqGain.Item2);
    }

    public void SetInside() {
        mixer.SetFloat("CutoffFreq", lowpassFreqs.Item1);
        mixer.SetFloat("FreqGain", eqGain.Item1);
        mixer.SetFloat("CutoffFreq2", lowpassFreqs.Item2);
        mixer.SetFloat("FreqGain2", eqGain.Item2);
    }

    public void TransitionInsideSlow() {
        StartCoroutine("EnumerateInsideSlow");
    }
    private IEnumerator EnumerateInsideSlow() {
        for (float t = 0; t < 1; t += 0.02f) {
            yield return new WaitForSeconds(0.2f);
            mixer.SetFloat("CutoffFreq", Mathf.Lerp(lowpassFreqs.Item2, lowpassFreqs.Item1, t));
            mixer.SetFloat("FreqGain", Mathf.Lerp(eqGain.Item2, eqGain.Item1, t));
        }
        mixer.SetFloat("CutoffFreq", lowpassFreqs.Item1);
        mixer.SetFloat("FreqGain", eqGain.Item1);
    }
}