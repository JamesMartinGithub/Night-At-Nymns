using UnityEngine;

public class BackDoorEventCaller : MonoBehaviour
{
    public SmsController sms;
    public Light torch;
    public GameObject meow;

    private void OnEnable() {
        sms.Day8Event();
        torch.intensity = 0.3f;
        meow.SetActive(false);
    }

    private void OnDisable() {
        torch.intensity = 0.81f;
    }
}