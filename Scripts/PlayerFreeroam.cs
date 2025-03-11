using UnityEngine;

public class PlayerFreeroam : MonoBehaviour
{
    public float speed;
    public float sensitivity;
    public Transform camPos;
    private float safeX;
    public AudioSource torchAudio;
    public Light torch;
    private bool torchOn = false;
    private Vector3 moveDir;
    private int stairLM;
    private Transform movementTrans;
    private float safeYRot;
    public Transform stairDir;
    public CharacterController charController;
    public GameObject pauseMenu;
    private bool paused = false;
    public Zoom zoom;
    public float speedMult = 1;

    private void Start() {
        stairLM = LayerMask.GetMask("StairSlope");
        movementTrans = transform;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (!paused) {
                paused = true;
                pauseMenu.SetActive(true);
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                zoom.enabled = false;
            } else {
                paused = false;
                pauseMenu.SetActive(false);
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                zoom.enabled = true;
            }
        }
        if (!paused) {
            //Change move direction if on stairs
            if (Physics.Raycast(transform.position, Vector3.down, 2, stairLM)) {
                safeYRot = transform.eulerAngles.y % 360;
                if (safeYRot > 90 && safeYRot < 270) {
                    stairDir.localEulerAngles = new Vector3(Mathf.Lerp(-34.978f, 34.978f, (safeYRot - 90) / 180), 0, 0);
                } else {
                    stairDir.localEulerAngles = new Vector3(Mathf.Lerp(34.978f, -34.978f, ((safeYRot + 90) % 360) / 180), 0, 0);
                }
                movementTrans = stairDir;
            } else {
                movementTrans = transform;
            }
            //Player Movement
            moveDir = Vector3.zero;
            if (Input.GetKey(KeyCode.W)) {
                moveDir += -movementTrans.forward;
            }
            if (Input.GetKey(KeyCode.S)) {
                moveDir += movementTrans.forward;
            }
            if (Input.GetKey(KeyCode.D)) {
                moveDir += -movementTrans.right;
            }
            if (Input.GetKey(KeyCode.A)) {
                moveDir += movementTrans.right;
            }
            if ((Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S)) && (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.A))) {
                moveDir *= 0.75f;
            }
            charController.Move(transform.up * -(speed * speedMult));
            charController.Move(moveDir * -(speed * speedMult));
            //Camera Movement
            transform.Rotate(Vector3.up * Input.GetAxis(axisName: "Mouse X") * (sensitivity * speedMult));
            safeX = camPos.transform.localEulerAngles.x;
            if (safeX >= 180) {
                safeX -= 360;
            }
            camPos.localEulerAngles = new Vector3(Mathf.Clamp(safeX + (-Input.GetAxis(axisName: "Mouse Y") * (sensitivity * speedMult)), -80, 70), 0, 0);
            //
            if (Input.GetKeyDown(KeyCode.Mouse0)) {
                if (!torchOn) {
                    torchOn = true;
                    torch.enabled = true;
                    torchAudio.Play();
                } else {
                    torchOn = false;
                    torch.enabled = false;
                    torchAudio.Play();
                }
            }
        }
    }
}