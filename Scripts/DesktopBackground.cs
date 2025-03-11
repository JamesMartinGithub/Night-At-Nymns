using UnityEngine;
using UnityEngine.EventSystems;

public class DesktopBackground : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public DesktopController controller;

    public void OnPointerDown(PointerEventData eventData) {
        controller.BackgroundPressed();
    }

    public void OnPointerUp(PointerEventData eventData) {
        controller.BackgroundReleased();
    }
}