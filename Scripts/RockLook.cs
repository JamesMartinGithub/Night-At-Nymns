using Unity.VisualScripting;
using UnityEngine;

public class RockLook : MonoBehaviour
{
    public Transform rock;
    public Transform rockY;
    public float sensitivity;

    void Update()
    {
        rock.Rotate(Vector3.up * Input.GetAxis(axisName: "Mouse X") * sensitivity);
        rockY.Rotate(rockY.right * Input.GetAxis(axisName: "Mouse Y") * sensitivity);
    }
}