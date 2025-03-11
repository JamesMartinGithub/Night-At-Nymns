using UnityEngine;

public class IntPicture : Interactable
{
    public GameObject glass;
    public GameObject fallenPicture;
    public GameObject standingPicture;
    public AudioSource placeSound;
    public AudioSource fallSound;

    private void OnEnable() {
        fallSound.Play();
        standingPicture.SetActive(false);
        fallenPicture.SetActive(true);
        glass.SetActive(false);
    }

    private void OnDisable() {
        GetComponent<MeshCollider>().enabled = true;
        standingPicture.SetActive(true);
        fallenPicture.SetActive(false);
    }

    public override void Interact() {
        standingPicture.SetActive(true);
        glass.SetActive(true);
        placeSound.Play();
        fallenPicture.SetActive(false);
        GetComponent<MeshCollider>().enabled = false;
    }
}