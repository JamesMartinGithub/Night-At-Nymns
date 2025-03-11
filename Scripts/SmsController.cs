using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SmsController : MonoBehaviour
{
    public Controller controller;
    public DesktopController desktopController;
    public ReplyText replyText;
    public GameObject yabbeChat;
    public GameObject forsenChat;
    private int day = 0;
    private bool choiceAllowed = true;
    private bool finished = false;
    public GameObject[] day0;
    public GameObject[] day15;
    public GameObject[] day6;
    public GameObject[] day7;
    public GameObject[] day8;
    public GameObject[] day9;
    public List<List<GameObject>> days; // 0=day0 1=day15 2=day6 3=day7 4=day8 5=day9
    private string textString = "";
    private string tryString = "";
    public GameObject sendButton;
    public TMPro.TextMeshProUGUI previewText;
    public GameObject choicePanel;
    public AudioSource sendSound;
    public GameObject day0Message;
    public GameObject day6Message;
    private bool day6EventDone = false;

    private void Start() {
        days = new List<List<GameObject>>();
        days.Add(day0.ToList());
        days.Add(day15.ToList());
        days.Add(day6.ToList());
        days.Add(day7.ToList());
        days.Add(day8.ToList());
        days.Add(day9.ToList());
    }

    public void Yabbe() {
        yabbeChat.SetActive(true);
        forsenChat.SetActive(false);
        if (!finished) {
            choicePanel.SetActive(true);
        }
    }

    public void Forsen() {
        yabbeChat.SetActive(false);
        forsenChat.SetActive(true);
        choicePanel.SetActive(false);
    }

    public void EnableEventChoice() {
        days[day-2][0].SetActive(false);
        days[day-2][1].SetActive(true);
    }

    public void Choice(string s) {
        if (choiceAllowed) {
            AddText(s);
            if (s == "startok" || s == "ok" || s == "event") {
                choiceAllowed = false;
                sendButton.SetActive(true);
            }
            switch (day) {
                case 0:
                    break;
                case >= 1 and <= 5:
                    if (s == "startsaw" || s == "startheard") {
                        days[1][0].SetActive(false);
                        days[1][1].SetActive(true);
                    }
                    if (s == "startsomething") {
                        days[1][0].SetActive(false);
                        days[1][2].SetActive(true);
                    }
                    if (s == "midsomeone") {
                        days[1][1].SetActive(false);
                        
                        if (day == 4) {
                            days[1][4].SetActive(true);
                        } else {
                            days[1][3].SetActive(true);
                        }
                    }
                    if (s == "midfellover") {
                        days[1][2].SetActive(false);
                        days[1][3].SetActive(true);
                    }
                    if (s == "endinside" || s == "endoutside" || s == "endknocking") {
                        choiceAllowed = false;
                        sendButton.SetActive(true);
                    }
                    break;
            }
        }
    }

    private void AddText(string s) {
        switch (s) {
            case "startok" or "ok":
                textString = "Everything's OK here";
                tryString = "ok";
                break;
            case "startsaw":
                textString += "I think I saw ";
                tryString += "saw";
                break;
            case "startheard":
                textString += "I think I heard ";
                tryString += "heard";
                break;
            case "startsomething":
                textString += "Something ";
                tryString += "something";
                break;
            case "midsomething":
                textString += "something strange ";
                tryString += "something";
                break;
            case "midsomeone":
                textString += "someone ";
                tryString += "someone";
                break;
            case "midfellover":
                textString += "fell over ";
                tryString += "fellover";
                break;
            case "endinside":
                textString += "inside";
                tryString += "inside";
                break;
            case "endoutside":
                textString += "outside";
                tryString += "outside";
                break;
            case "endknocking":
                textString += "knocking";
                tryString += "knocking";
                break;
            case "event":
                switch (day) {
                    case 6:
                        textString += "I couldn't see your keys outside";
                        break;
                    case 7:
                        textString += "The TV turned on by itself";
                        break;
                    case 8:
                        textString += "I found the back door open";
                        break;
                    case 9:
                        textString += "I thought I heard someone in the house";
                        break;
                }
                tryString += "event";
                break;
        }
        previewText.SetText(textString);
    }

    private void TryText() {
        choiceAllowed = false;
        finished = true;
        // check tryString for valid or not, then tell controller
        switch (day) {
            case 0:
                if (tryString == "ok") { //sawsomethingoutside
                    controller.TryEndDay(true);
                } else {
                    controller.TryEndDay(false);
                }
                break;
            case 1:
                if (tryString == "somethingfelloverinside") {
                    controller.TryEndDay(true);
                } else {
                    controller.TryEndDay(false);
                }
                break;
            case 2:
                if (tryString == "ok") {
                    controller.TryEndDay(true);
                } else {
                    controller.TryEndDay(false);
                }
                break;
            case 3:
                if (tryString == "heardsomeoneoutside" || tryString == "heardsomethingoutside") {
                    controller.TryEndDay(true);
                } else {
                    controller.TryEndDay(false);
                }
                break;
            case 4:
                if (tryString == "heardsomeoneknocking") {
                    controller.TryEndDay(true);
                } else {
                    controller.TryEndDay(false);
                }
                break;
            case 5:
                if (tryString == "sawsomeoneoutside") {
                    controller.TryEndDay(true);
                } else {
                    controller.TryEndDay(false);
                }
                break;
            case >= 6 and <= 9:
                if (tryString == "event") {
                    controller.TryEndDay(true);
                } else {
                    controller.TryEndDay(false);
                }
                break;
        }
    }

    public void SetDay(int d) {
        day = d;
        if (d >= 1 && d <= 5) {
            d = 1;
        }
        if (d >= 6) {
            d -= 4;
        }
        foreach (List<GameObject> day in days) {
            foreach (GameObject choice in day) {
                choice.SetActive(false);
            }
        }
        if (day != 10) {
            if (day != 6) {
                days[d][0].SetActive(true);
            } else {
                if (day6EventDone) {
                    days[d][0].SetActive(true);
                }
            }
        }
        choiceAllowed = true;
        textString = "";
        tryString = "";
        sendButton.SetActive(false);
        previewText.SetText("");
        finished = false;
        choicePanel.SetActive(true);
        replyText.ClearReply();
        if (day == 0) {
            day0Message.SetActive(true);
        } else {
            day0Message.SetActive(false);
        }
        if (day == 6) {
            day6Message.SetActive(true);
        } else {
            day6Message.SetActive(false);
        }
    }

    public void Send() {
        TryText();
        sendButton.SetActive(false);
        previewText.SetText("");
        choicePanel.SetActive(false);
        sendSound.Play();
        replyText.RenderReply(textString, day == 0 || day == 6);
        desktopController.DisableExit();
        GameObject sus = GameObject.Find("BackDoorSuspense");
        if (sus != null) {
            sus.SetActive(false);
        }
    }

    public void Clear() {
        if (!finished) {
            SetDay(day);
        }
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Backspace)) {
            Clear();
        }
    }

    public void OutsideEvent() {
        if (day == 6) {
            days[2][0].SetActive(true);
            day6EventDone = true;
        }
    }

    public void Day7Event() {
        if (day == 7) {
            days[3][0].SetActive(false);
            days[3][1].SetActive(true);
        }
    }

    public void Day8Event() {
        if (day == 8) {
            days[4][0].SetActive(false);
            days[4][1].SetActive(true);
        }
    }

    public void Day9Event() {
        if (day == 9) {
            Invoke("Day9EventDelayed", 3f);
        }
    }
    private void Day9EventDelayed() {
        days[5][0].SetActive(false);
        days[5][1].SetActive(true);
    }
}