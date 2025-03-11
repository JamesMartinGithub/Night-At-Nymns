using UnityEngine;
using UnityEngine.EventSystems;

public class MinesweeperButton : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData) {
        if (eventData.button == PointerEventData.InputButton.Left) {
            GameObject.Find("Minesweeper").GetComponent<Minesweeper>().GridPress(int.Parse(name[0].ToString()), int.Parse(name[2].ToString()), true);
        } else if (eventData.button == PointerEventData.InputButton.Right) {
            GameObject.Find("Minesweeper").GetComponent<Minesweeper>().GridPress(int.Parse(name[0].ToString()), int.Parse(name[2].ToString()), false);
        }
    }
}