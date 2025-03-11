using UnityEngine;

public class TriggerAnimation : Trigger
{
    public Animation anim;

    private void OnEnable() {
        anim.Play();
        anim.Sample();
        anim.Stop();
        this.col.enabled = true;
        obj.SetActive(false);
    }

    public override void TriggerEvent() {
        this.col.enabled = false;
        Invoke("PlayAnim", delay);
    }

    private void PlayAnim() {
        anim.Play();
        if (this.obj != null) {
            obj.SetActive(true);
        }
    }
}