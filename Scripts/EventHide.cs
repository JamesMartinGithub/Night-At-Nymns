using UnityEngine;

public class EventHide : MonoBehaviour
{
    public Animation anim;
    public AudioSource aud;
    public Footsteps footsteps;
    private bool activated = false;
    public GameObject visual1;
    public GameObject visual2;

    public void Hide() {
        anim.Play();
        aud.Play();
        activated = true;
        Invoke("Deactivate", 1);
    }

    private void OnDisable() {
        anim.Play();
        anim.Sample();
        anim.Stop();
        activated = false;
    }

    private void Deactivate() {
        gameObject.SetActive(false);
    }

    private void FixedUpdate() {
        if (!activated) {
            if (!footsteps.inside) {
                if (visual1.activeSelf) {
                    visual1.SetActive(false);
                    visual2.SetActive(false);
                }
            } else {
                if (!visual1.activeSelf) {
                    visual1.SetActive(true);
                    visual2.SetActive(true);
                }
            }
        }
    }
}