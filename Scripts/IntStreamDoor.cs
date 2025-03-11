using UnityEngine;

public class IntStreamDoor : Interactable
{
    public GameObject closedDoor;
    public GameObject openDoor;
    public AudioSource aud;
    public Animation fade;
    private MeshCollider col;
    public Player player;
    public Controller controller;

    private void Start() {
        col = GetComponent<MeshCollider>();
    }

    public override void Interact() {
        if (controller.GetDay() == 9) {
            fade.Play("DoorFade");
            Invoke("EnablePlayer", 3.5f);
        } else {
            fade.Play("DoorFadeShort");
            Invoke("EnablePlayer", 2.2f);
        }
        Invoke("OpenDoor", 0.2f);
        player.enabled = false;
    }

    private void OpenDoor() {
        aud.Play();
        col.enabled = false;
        closedDoor.SetActive(false);
        openDoor.SetActive(true);
    }

    private void EnablePlayer() {
        player.enabled = true;
    }

    public void CloseDoor(bool allowOpening) {
        if (allowOpening) {
            col.enabled = true;
        } else {
            col.enabled = false;
        }
        closedDoor.SetActive(true);
        openDoor.SetActive(false);
    }
}