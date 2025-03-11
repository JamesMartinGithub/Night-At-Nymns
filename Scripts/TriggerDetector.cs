using UnityEngine;

public class TriggerDetector : MonoBehaviour
{
    public Interactor interactor;

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.layer == 11) {
            other.gameObject.GetComponent<Trigger>().TriggerEvent();
        } else {
            interactor.distance = 2.5f;
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.gameObject.layer == 12) {
            interactor.distance = 0.8f;
        }
    }
}