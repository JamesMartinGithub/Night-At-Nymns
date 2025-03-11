using System.Collections;
using UnityEngine;

public class FloorCreak : MonoBehaviour
{
    public AudioSource[] sounds;
    public AudioClip[] clips;
    public Transform origin;
    public Transform[] positions;
    public Transform Player;
    private int lastChosen = 0;
    private int[] choices = new int[2];

    private void OnEnable() {
        StartCoroutine("Creak");
    }

    private void OnDisable() {
        StopCoroutine("Creak");
    }

    private IEnumerator Creak() {
        while (true) {
            yield return new WaitForSeconds(Random.Range(4f, 10.0f));
            if (Player.position.y > 4.43f) {
                origin.position = positions[1].position;
            } else {
                origin.position = positions[0].position;
            }
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
            sounds[lastChosen].clip = clips[Random.Range(0, 3)];
            sounds[lastChosen].Play();
        }
    }
}