using UnityEngine;

public class Zoom : MonoBehaviour
{
    public Camera cam;
    private int fov = 60;

    void Start() {
        cam = GetComponent<Camera>();
    }

    void FixedUpdate() {
        if (Input.GetKey(KeyCode.Mouse1)) {
            if (fov > 20) {
                fov -= 2;
                cam.fieldOfView = fov;
            }
        } else if (fov < 60) {
            fov += 2;
            cam.fieldOfView = fov;
        }
    }
}