using UnityEngine;

public class Footsteps : MonoBehaviour
{
    public AudioClip[] gravelSteps;
    public AudioClip[] woodSteps;
    public AudioClip[] stoneSteps;
    private AudioSource aud;
    private Animation anim;
    private int index;
    public bool onStairs;
    public bool inside;
    private bool animPlaying = false;
    public CharacterController character;
    public Player player;

    private void Start() {
        aud = GetComponent<AudioSource>();
        anim = GetComponent<Animation>();
        aud.clip = stoneSteps[0];
        index = 0;
        onStairs = false;
        inside = true;
    }

    public void Step() {
        index = Random.Range(0, 4);
        if (inside) {
            aud.volume = 0.35f;
            if (onStairs) {
                aud.clip = woodSteps[index];
            } else {
                aud.clip = stoneSteps[index];
            }
        } else {
            aud.volume = 1;
            aud.clip = gravelSteps[index];
        }
        aud.Play();
    }

    private void Update() {
        if (character.velocity.magnitude != 0 && player.enabled) {
            if (!animPlaying) {
                animPlaying = true;
                anim.Play();
            }
        } else {
            if (animPlaying) {
                animPlaying = false;
                anim.Stop();
            }
        }
    }
}