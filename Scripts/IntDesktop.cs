using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class IntDesktop : Interactable
{
    public Player player;
    public Camera playerCamera;
    public AudioListener playerListener;
    public Transform playerCam;
    public Transform desktopCam;
    public Transform desktopCamPos;
    public Controller controller;
    public bool desktopMode;
    public Image ui;
    public GameObject torch;
    public MeshCollider col;
    public GameObject crash;

    private void Awake() {
        col = GetComponent<MeshCollider>();
    }

    public override void Interact() {
        desktopCam.position = playerCam.position;
        desktopCam.rotation = playerCam.rotation;
        player.enabled = false;
        ui.enabled = false;
        torch.SetActive(false);
        playerCamera.enabled = false;
        playerListener.enabled = false;
        desktopCam.gameObject.SetActive(true);
        StartCoroutine("CamMovement");
        desktopMode = true;
        col.enabled = false;
    }

    public void Escape() {
        StartCoroutine("CamMovementRev");
    }

    private IEnumerator CamMovement() {
        Vector3 startPos = playerCam.position;
        Vector3 endPos = desktopCamPos.position;
        Quaternion startRot = playerCam.rotation;
        Quaternion endRot = desktopCamPos.rotation;
        for (float t = 0; t < 1; t += 0.02f) {
            desktopCam.position = Vector3.Lerp(startPos, endPos, t);
            desktopCam.rotation = Quaternion.Lerp(startRot, endRot, t);
            yield return new WaitForSeconds(0.01f);
        }
        desktopCam.position = desktopCamPos.position;
        desktopCam.rotation = desktopCamPos.rotation;
        controller.UnlockCursor();
        if (controller.GetDay() == 6) {
            crash.SetActive(true);
        }
    }

    private IEnumerator CamMovementRev() {
        Vector3 startPos = desktopCamPos.position;
        Vector3 endPos = playerCam.position;
        Quaternion startRot = desktopCamPos.rotation;
        Quaternion endRot = playerCam.rotation;
        for (float t = 0; t < 1; t += 0.02f) {
            desktopCam.position = Vector3.Lerp(startPos, endPos, t);
            desktopCam.rotation = Quaternion.Lerp(startRot, endRot, t);
            yield return new WaitForSeconds(0.01f);
        }
        desktopCam.gameObject.SetActive(false);
        player.enabled = true;
        ui.enabled = true;
        torch.SetActive(true);
        playerCamera.enabled = true;
        playerListener.enabled = true;
        controller.LockCursor();
        desktopMode = false;
        col.enabled = true;
    }

    public void SetDesktopCam() {
        desktopCam.position = desktopCamPos.position;
        desktopCam.rotation = desktopCamPos.rotation;
        controller.UnlockCursor();
        player.enabled = false;
        ui.enabled = false;
        torch.SetActive(false);
        playerCamera.enabled = false;
        playerListener.enabled = false;
        desktopCam.gameObject.SetActive(true);
        desktopMode = true;
        col.enabled = false;
    }

    public void SetPlayerCam() {
        controller.LockCursor();
        player.enabled = true;
        ui.enabled = true;
        torch.SetActive(true);
        playerCamera.enabled = true;
        playerListener.enabled = true;
        desktopCam.gameObject.SetActive(false);
        desktopMode = false;
        col.enabled = true;
    }
}