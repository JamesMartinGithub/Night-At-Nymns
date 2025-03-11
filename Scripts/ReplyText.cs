using UnityEngine;

public class ReplyText : MonoBehaviour
{
    public TMPro.TextMeshProUGUI replyText0;
    public TMPro.TextMeshProUGUI replyText;
    public GameObject[] day0Bubbles;
    public GameObject[] dayBubbles;
    private bool isZero = true;

    public void RenderReply(string message, bool zero) {
        ClearReply();
        if (zero) {
            isZero = true;
            replyText0.SetText(message);
        } else {
            isZero = false;
            replyText.SetText(message);
        }
        Invoke("Show", 0.1f);
    }

    void Show() {
        if (isZero) {
            int lines = Mathf.Clamp(replyText0.textInfo.lineCount, 1, 2);
            day0Bubbles[lines - 1].SetActive(true);
            replyText0.alpha = 1;
        } else {
            int lines = Mathf.Clamp(replyText.textInfo.lineCount, 1, 3);
            dayBubbles[lines - 1].SetActive(true);
            replyText.alpha = 1;
        }
    }

    public void ClearReply() {
        replyText0.alpha = 0;
        replyText.alpha = 0;
        replyText0.SetText("");
        replyText.SetText("");
        foreach (GameObject bubble in day0Bubbles) {
            bubble.SetActive(false);
        }
        foreach (GameObject bubble in dayBubbles) {
            bubble.SetActive(false);
        }
    }
}