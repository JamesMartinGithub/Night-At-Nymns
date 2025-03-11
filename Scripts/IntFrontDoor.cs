using UnityEngine;

public class IntFrontDoor : Interactable
{
    public bool goOutside;
    public Player player;
    public Transform playerPos;
    public Transform position;
    public Animation fadeAnim;
    public AudioSource aud;
    public AudioController audioController;
    public Footsteps footsteps;
    public SmsController sms;
    private bool ready = true;

    public override void Interact() {
        if (ready) {
            ready = false;
            player.enabled = false;
            if (goOutside) {
                fadeAnim.Play("DoorFadeLong");
                Invoke("OutsideAmbientAudio", 2.4f);
                sms.OutsideEvent();
            } else {
                fadeAnim.Play("DoorFade");
                Invoke("InsideAmbientAudio", 1.4f);
            }
            aud.Play();
            Invoke("MovePlayer", 1.5f);
            Invoke("EnablePlayer", 4f);
        }
    }

    private void MovePlayer() {
        playerPos.position = position.position;
        playerPos.rotation = position.rotation;
    }

    private void EnablePlayer() {
        player.enabled = true;
        ready = true;
        if (goOutside) {
            footsteps.inside = false;
        } else {
            footsteps.inside = true;
        }
    }

    private void OutsideAmbientAudio() {
        audioController.TransitionOutside();
    }
    private void InsideAmbientAudio() {
        audioController.TransitionInside();
    }
}