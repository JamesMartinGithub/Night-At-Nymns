using System;
using UnityEngine;

public class IntNews : Interactable
{
    public GameObject news;
    public Animation newsAnim;
    public GameObject staticObj;
    public TMPro.TextMeshProUGUI date;
    public SmsController smsController;
    private MeshCollider col;

    private void Awake() {
        col = GetComponent<MeshCollider>();
    }

    private void Start() {
        date.text = DateTime.Now.Day.ToString() + " OCT";
    }

    private void OnEnable() {
        newsAnim.Stop();
        newsAnim.Play();
        smsController.Day7Event();
        col.enabled = false;
    }

    public override void Interact() {
        news.SetActive(false);
    }

    private void OnDisable() {
        staticObj.SetActive(false);
        newsAnim.Play();
        newsAnim.Sample();
        newsAnim.Stop();
    }

    private void FixedUpdate() {
        if (staticObj.activeSelf) {
            col.enabled = true;
        }
    }
}