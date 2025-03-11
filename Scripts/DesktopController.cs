using UnityEngine;
using UnityEngine.UI;

public class DesktopController : MonoBehaviour
{
    public Animation anim;
    public GameObject error;
    public GameObject errorTaskbar;
    public GameObject errorBlocker;
    public Camera cam;
    public GameObject notification;
    public GameObject minesweeper;
    public GameObject minesweeperTaskbar;
    public GameObject fileexplorer;
    public GameObject fileexplorerTaskbar;
    public GameObject[] folders;
    private int folderIndex = 0;
    public GameObject sms;
    public GameObject smsTaskbar;
    public SmsController smsCon;
    public IntDesktop interactable;
    private bool exitAllowed = true;
    // Background
    public RectTransform highlight;
    public RectTransform[] apps;
    public Image[] appsHighlight;
    public Color highlightColor;
    private bool backgroundPressed = false;
    private Vector2 bgPressStart;
    private Vector2 max;
    private Vector2 min;
    private Vector2 xCoords;
    private Vector2 yCoords;
    private Vector2 canvasRes = new Vector2(1920, 1080);
    private float resDiffRatio;

    private void Start() {
        highlight.gameObject.GetComponent<Image>().color = highlightColor;
        resDiffRatio = Screen.width / canvasRes.x;
    }

    public void ExcelLaunch() {
        anim.Play("ExcelLaunch");
        ClearAppHighlights();
    }

    public void ErrorClose() {
        error.SetActive(false);
        errorTaskbar.SetActive(false);
        errorBlocker.SetActive(false);
    }

    public void NotificationClose() {
        notification.SetActive(false);
    }

    public void MinesweeperLaunch() {
        minesweeper.SetActive(true);
        minesweeperTaskbar.SetActive(true);
        ClearAppHighlights();
    }

    public void MinesweeperClose() {
        minesweeper.SetActive(false);
        minesweeperTaskbar.SetActive(false);
    }

    public void FileExplorerLaunch() {
        fileexplorer.SetActive(true);
        fileexplorerTaskbar.SetActive(true);
        ClearAppHighlights();
    }

    public void FileExplorerClose() {
        folderIndex = 0;
        folders[0].SetActive(true);
        folders[1].SetActive(false);
        folders[2].SetActive(false);
        //
        fileexplorer.SetActive(false);
        fileexplorerTaskbar.SetActive(false);
    }

    public void FolderOpen(bool open) {
        if (open) {
            folderIndex = Mathf.Clamp(folderIndex + 1, 0, 2);
        } else {
            folderIndex = Mathf.Clamp(folderIndex - 1, 0, 2);
        }
        for (int i = 0; i < 3; i++) {
            if (i == folderIndex) {
                folders[i].SetActive(true);
            } else {
                folders[i].SetActive(false);
            }
        }
    }

    public void SmsLaunch() {
        smsCon.Yabbe();
        sms.SetActive(true);
        smsTaskbar.SetActive(true);
        ClearAppHighlights();
    }

    public void SmsClose() {
        sms.SetActive(false);
        smsTaskbar.SetActive(false);
    }

    public void DisableExit() {
        exitAllowed = false;
    }

    public void AllowExit() {
        exitAllowed = true;
    }

    public void SetToDesktopCam() {
        interactable.SetDesktopCam();
        SmsLaunch();
    }

    public void SetToPlayerCam() {
        interactable.SetPlayerCam();
    }

    private void ClearAppHighlights() {
        appsHighlight[0].color = new Color(highlightColor.r, highlightColor.g, highlightColor.b, 0);
        appsHighlight[1].color = new Color(highlightColor.r, highlightColor.g, highlightColor.b, 0);
        appsHighlight[2].color = new Color(highlightColor.r, highlightColor.g, highlightColor.b, 0);
        appsHighlight[3].color = new Color(highlightColor.r, highlightColor.g, highlightColor.b, 0);
    }

    public void BackgroundPressed() {
        backgroundPressed = true;
        bgPressStart = WorldToCanvasPosition(cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x / resDiffRatio, Input.mousePosition.y / resDiffRatio, 308)));
    }

    public void BackgroundReleased() {
        backgroundPressed = false;
        highlight.offsetMax = Vector2.zero;
        highlight.offsetMin = canvasRes;
    }

    public void Update() {
        if (Input.GetKeyDown(KeyCode.Escape) && interactable.desktopMode && exitAllowed) {
            interactable.Escape();
        }
        if (backgroundPressed) {
            Vector2 vec = WorldToCanvasPosition(cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x / resDiffRatio, Input.mousePosition.y / resDiffRatio, 308)));
            if (vec.x < bgPressStart.x) {
                max.x = -(canvasRes.x - bgPressStart.x);
                min.x = vec.x;
                xCoords.x = min.x;
                xCoords.y = bgPressStart.x;
            } else {
                max.x = -(canvasRes.x - vec.x);
                min.x = bgPressStart.x;
                xCoords.x = min.x;
                xCoords.y = vec.x;
            }
            if (vec.y < bgPressStart.y) {
                max.y = -(canvasRes.y - bgPressStart.y);
                min.y = vec.y;
                yCoords.x = min.y;
                yCoords.y = bgPressStart.y;
            } else {
                max.y = -(canvasRes.y - vec.y);
                min.y = bgPressStart.y;
                yCoords.x = min.y;
                yCoords.y = vec.y;
            }
            highlight.offsetMax = max;
            highlight.offsetMin = min;
            // Check for app highlighting
            if (apps[0].anchoredPosition.x > xCoords.x && apps[0].anchoredPosition.x < xCoords.y && apps[0].anchoredPosition.y > yCoords.x && apps[0].anchoredPosition.y < yCoords.y) {
                appsHighlight[0].color = new Color(highlightColor.r, highlightColor.g, highlightColor.b, 0.2627451f);
            } else {
                appsHighlight[0].color = new Color(highlightColor.r, highlightColor.g, highlightColor.b, 0);
            }
            if (apps[1].anchoredPosition.x > xCoords.x && apps[1].anchoredPosition.x < xCoords.y && apps[1].anchoredPosition.y > yCoords.x && apps[1].anchoredPosition.y < yCoords.y) {
                appsHighlight[1].color = new Color(highlightColor.r, highlightColor.g, highlightColor.b, 0.2627451f);
            } else {
                appsHighlight[1].color = new Color(highlightColor.r, highlightColor.g, highlightColor.b, 0);
            }
            if (apps[2].anchoredPosition.x > xCoords.x && apps[2].anchoredPosition.x < xCoords.y && apps[2].anchoredPosition.y > yCoords.x && apps[2].anchoredPosition.y < yCoords.y) {
                appsHighlight[2].color = new Color(highlightColor.r, highlightColor.g, highlightColor.b, 0.2627451f);
            } else {
                appsHighlight[2].color = new Color(highlightColor.r, highlightColor.g, highlightColor.b, 0);
            }
            if (apps[3].anchoredPosition.x > xCoords.x && apps[3].anchoredPosition.x < xCoords.y && apps[3].anchoredPosition.y > yCoords.x && apps[3].anchoredPosition.y < yCoords.y) {
                appsHighlight[3].color = new Color(highlightColor.r, highlightColor.g, highlightColor.b, 0.2627451f);
            } else {
                appsHighlight[3].color = new Color(highlightColor.r, highlightColor.g, highlightColor.b, 0);
            }
        }
    }

    private Vector2 WorldToCanvasPosition (Vector3 position) {
        Canvas canvas = GetComponent<Canvas>();
        RectTransform canvasRect = GetComponent<RectTransform>();
        Vector2 screenPoint = RectTransformUtility.WorldToScreenPoint(cam, position);
        Vector2 result;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRect, screenPoint, canvas.renderMode == RenderMode.ScreenSpaceOverlay ? null : GetComponent<Camera>(), out result);
        result = canvas.transform.TransformPoint(result); //1874 below
        result = new Vector2(Mathf.Clamp(((result.x - (canvasRes.x / 2)) * 1.09f) + (canvasRes.x / 2), 0, (canvasRes.x * 0.976f)), Mathf.Clamp(((result.y - (canvasRes.y / 2)) * 1.07f) + (canvasRes.y / 2), 0, canvasRes.y));
        return result;
    }
}