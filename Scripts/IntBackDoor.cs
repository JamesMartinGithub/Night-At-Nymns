using UnityEngine;

public class IntBackDoor : Interactable
{
    public AudioSource slideSound;
    public Animation fade;
    public GameObject closedDoor;
    public GameObject openDoor;
    private MeshCollider col;

    private void Awake() {
        col = GetComponent<MeshCollider>();
    }

    public override void Interact() {
        fade.Play("DoorFadeShort");
        slideSound.Play();
        col.enabled = false;
        Invoke("CloseDoor", 1);
    }

    private void CloseDoor() {
        openDoor.SetActive(false);
        closedDoor.SetActive(true);
    }

    private void OnEnable() {
        openDoor.SetActive(true);
        closedDoor.SetActive(false);
        col.enabled = true;
    }

    private void OnDisable() {
        col.enabled = true;
        CloseDoor();
    }
}