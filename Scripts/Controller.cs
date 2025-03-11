using UnityEngine;

public class Controller : MonoBehaviour
{
    public Player player;
    private int day = 0;
    private bool paused = false;
    public IntDesktop desktop;
    public SmsController sms;
    public IntStreamDoor streamDoor;
    //UI
    public Animation dayTextAnim;
    public GameObject dayTextObj;
    public TMPro.TextMeshProUGUI dayText;
    public GameObject reloadIcon;
    public DesktopController desktopController;
    public bool pauseDisallowed = true;
    public Zoom zoom;
    public Interactor interactor;
    public GameObject pauseMenu;
    //Day Objects
    public LookingAtEvent lookingAtEvent; //Day0
    public GameObject redLight; //Day 0
    public GameObject fallenPicture; //Day 1
    public GameObject elkNoise; //Day 2
    public TorchFlicker torchFlicker; //Day 2, Day 5-> (excluding 8)
    public GameObject gravelWalk; //Day 3
    public GameObject fridgeScreen;
    public GameObject bushRustle; //Day 3->
    public GameObject doorKnock; //Day 4
    public GameObject floorCreaks; //Day 4->
    public GameObject figure; //Day 5
    public GameObject rock; //Day 6
    public GameObject news; //Day 7
    public GameObject backDoor; //Day 8
    public GameObject footstepsInside; //Day 9
    public GameObject jumpscare; //Day 10
    //Intro
    public GameObject intro;
    public AudioSource introSound;
    public GameObject introFade;
    public AudioController audioController;

    void Start() {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        //INTRO
        Invoke("PrePlayIntro", 3);
        //FOR TESTING
        /*
        intro.SetActive(false);
        player.gameObject.SetActive(true);
        audioController.SetInside();
        day = 8;
        SetupDay();
        */
    }

    private void PrePlayIntro() {
        introSound.Play();
        player.enabled = false;
        Invoke("PlayIntro", 0.6f);
        pauseDisallowed = true;
    }
    private void PlayIntro() {
        intro.GetComponent<Animation>().Play();
        Invoke("StopIntro", 32);
        Invoke("IntroAmbience", 14.05f);
    }
    private void IntroAmbience() {
        audioController.TransitionInsideSlow();
    }
    private void StopIntro() {
        intro.SetActive(false);
        player.gameObject.SetActive(true);
        dayTextAnim.Stop();
        dayTextAnim.Play("DayTextFadeHalf");
        Invoke("SetupDay", 4.633f);
    }

    public void TryEndDay(bool correct) {
        player.enabled = false;
        dayTextAnim.Stop();
        
        if (correct) {
            day += 1;
            dayTextAnim.Play("DayTextFade");
        } else {
            ResetEvents();
            dayTextAnim.Play("DayTextFadeRedo");
        }
        dayText.text = "Day " + day.ToString();
        LockCursor();
        Invoke("SetupDay", 8.58f);
    }

    private void SetupDay() {
        reloadIcon.SetActive(false);
        pauseDisallowed = false;
        dayTextAnim.Stop();
        dayTextObj.SetActive(false);
        sms.SetDay(day);
        if (day != 10) {
            streamDoor.CloseDoor(true);
        }
        player.enabled = true;
        desktopController.SetToPlayerCam();
        desktopController.AllowExit();
        desktopController.SmsLaunch();
        switch (day) {
            case 0:
                lookingAtEvent.enabled = true;
                redLight.SetActive(true);

                //streamDoor.CloseDoor(false);
                desktopController.SetToDesktopCam();
                break;
            case 1:
                lookingAtEvent.enabled = false;
                redLight.SetActive(false);
                //
                Invoke("PictureFall", 3);
                break;
            case 2:
                fallenPicture.SetActive(false);
                //
                elkNoise.SetActive(true);
                torchFlicker.enabled = true;
                break;
            case 3:
                //elkNoise.SetActive(false);
                torchFlicker.enabled = false;
                //
                gravelWalk.SetActive(true);
                fridgeScreen.SetActive(true);
                bushRustle.SetActive(true);
                break;
            case 4:
                gravelWalk.SetActive(false);
                fridgeScreen.SetActive(false);
                //
                doorKnock.SetActive(true);
                floorCreaks.SetActive(true);
                break;
            case 5:
                doorKnock.SetActive(false);
                //
                figure.SetActive(true);
                torchFlicker.enabled = true;
                break;
            case 6:
                figure.SetActive(false);
                //
                rock.SetActive(true);

                desktopController.SetToDesktopCam();
                break;
            case 7:
                rock.SetActive(false);
                //
                news.SetActive(true);
                break;
            case 8:
                news.SetActive(false);
                torchFlicker.enabled = false;
                //
                backDoor.SetActive(true);
                break;
            case 9:
                torchFlicker.enabled = true;
                backDoor.SetActive(false);
                //
                Invoke("PlayFootstepsInside", 2);
                break;
            case 10:
                footstepsInside.SetActive(false);
                //
                streamDoor.gameObject.SetActive(false);
                jumpscare.SetActive(true);
                break;
        }
    }

    private void ResetEvents() {
        //Day 0
        lookingAtEvent.enabled = false;
        redLight.SetActive(false);
        //Day 1
        fallenPicture.SetActive(false);
        //Day 2
        //elkNoise.SetActive(false);
        //torchFlicker.enabled = false;
        //Day 3
        gravelWalk.SetActive(false);
        fridgeScreen.SetActive(false);
        //bushRustle.SetActive(false);
        //Day 4
        doorKnock.SetActive(false);
        //floorCreaks.SetActive(false);
        //Day 5
        figure.SetActive(false);
        //Day 6
        rock.SetActive(false);
        //Day 7
        news.SetActive(false);
        //Day 8
        backDoor.SetActive(false);
        //Day 9
        footstepsInside.SetActive(false);
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape) && !desktop.desktopMode && !pauseDisallowed) {
            if (paused) {
                Unpause();
            } else {
                Pause();
            }
        }
    }

    private void Pause() {
        player.enabled = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        paused = true;
        zoom.enabled = false;
        interactor.enabled = false;
        pauseMenu.SetActive(true);
    }
    private void Unpause() {
        player.enabled = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        paused = false;
        zoom.enabled = true;
        interactor.enabled = true;
        pauseMenu.SetActive(false);
    }

    public void UnlockCursor() {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        paused = true;
    }
    public void LockCursor() {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        paused = false;
    }

    private void PictureFall() {
        fallenPicture.SetActive(true);
    }

    private void PlayFootstepsInside() {
        footstepsInside.SetActive(true);
        sms.Day9Event();
    }

    public int GetDay() {
        return day;
    }
}