using UnityEngine;

public class RockInt : Interactable
{
    public Player player;
    public Camera playerCam;
    public Controller controller;
    public GameObject rockLook;
    public GameObject UI;
    public GameObject torch;
    private bool looking = false;

    public override void Interact() {
        player.enabled = false;
        playerCam.enabled = false;
        rockLook.SetActive(true);
        controller.pauseDisallowed = true;
        looking = true;
        UI.SetActive(false);
        torch.SetActive(false);
    }

    private void Update() {
        if (Input.GetKey(KeyCode.Escape) && looking) {
            player.enabled = true;
            playerCam.enabled = true;
            rockLook.SetActive(false);
            controller.pauseDisallowed = false;
            looking = false;
            UI.SetActive(true);
            torch.SetActive(true);
        }
    }
}