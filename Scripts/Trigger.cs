using UnityEngine;

public class Trigger : MonoBehaviour
{
    public float delay;
    public GameObject obj;
    protected private BoxCollider col;

    private void Awake() {
        col = GetComponent<BoxCollider>();
    }

    private void OnEnable() {
        col.enabled = true;
        obj.SetActive(false);
    }

    public virtual void TriggerEvent() {
        col.enabled = false;
        Invoke("ActivateObj", delay);
    }

    private void ActivateObj() {
        obj.SetActive(true);
    }
}