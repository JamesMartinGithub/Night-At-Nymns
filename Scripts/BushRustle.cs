using System.Collections;
using UnityEngine;

public class BushRustle : MonoBehaviour
{
    public AudioSource[] sounds;
    private int lastChosen = 0;
    private int[] choices = new int[2];

    private void OnEnable() {
        StartCoroutine("Rustle");
    }

    private void OnDisable() {
        StopCoroutine("Rustle");
    }

    private IEnumerator Rustle() {
        while (true) {
            yield return new WaitForSeconds(Random.Range(3.0f, 6.0f));
            if (lastChosen == 0) {
                choices[0] = 1;
            } else {
                choices[0] = 0;
            }
            if (lastChosen == 2) {
                choices[1] = 1;
            } else {
                choices[1] = 2;
            }
            lastChosen = choices[Random.Range(0, 2)];
            sounds[lastChosen].Play();
        }
    }
}