using UnityEngine;

public class IntLockedDoor : Interactable
{
    private MeshCollider col;
    public AudioSource aud;
    public Animation handleAnim;
    //Door Bangs
    public AudioSource banging;
    //Jumpscare
    public Animation jumpscareDoor;
    public GameObject faceAndCam;
    public GameObject player;

    void Start()
    {
        col = GetComponent<MeshCollider>();
    }

    public override void Interact() {
        aud.Play();
        handleAnim.Play("TryHandle");
        col.enabled = false;
        Invoke("Banging", 4);
    }

    private void Banging() {
        banging.Play();
        handleAnim.Play("JumpscareHandle");
        Invoke("Jumpscare", 19.2f);
    }

    private void Jumpscare() {
        player.SetActive(false);
        faceAndCam.SetActive(true);
        jumpscareDoor.Play();
    }
}