using UnityEngine;

public class LookingAtEvent : MonoBehaviour
{
    public EventHide[] events;
    public float distance;
    private LayerMask eventMask;
    private LayerMask positionMask;

    private void Start() {
        eventMask = LayerMask.GetMask("LookAtEvent");
        positionMask = LayerMask.GetMask("LookAtPosition");
    }

    void FixedUpdate() {
        if (Physics.CheckSphere(transform.position, 0.01f, positionMask)) {
            if (Physics.Raycast(transform.position, transform.forward, distance, eventMask)) {
                foreach (EventHide e in events) {
                    if (e.gameObject.activeSelf) {
                        e.Hide();
                    }
                }
                this.enabled = false;
            }
        }
    }
}