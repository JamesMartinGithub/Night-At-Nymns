using UnityEngine;

public class IntFridge : Interactable
{
    public Animation anim;

    public override void Interact() {
        anim.Play();
    }
}