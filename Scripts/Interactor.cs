using UnityEngine;

public class Interactor : MonoBehaviour
{
    public GameObject hand;
    public float distance;
    private int mask;
    private bool handActive = false;
    private RaycastHit hit;

    private void Start() {
        mask = LayerMask.GetMask("Interactable");
    }

    void Update()
    {
        if (Physics.Raycast(transform.position, transform.forward, out hit, distance, mask)) {
            if (!handActive) {
                hand.SetActive(true);
                handActive = true;
            }
            if (Input.GetKeyDown(KeyCode.E)) {
                hit.collider.gameObject.GetComponent<Interactable>().Interact();
            }
        } else {
            if (handActive) {
                hand.SetActive(false);
                handActive = false;
            }
        }
    }
}