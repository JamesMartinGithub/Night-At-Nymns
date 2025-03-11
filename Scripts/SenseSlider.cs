using UnityEngine;
using UnityEngine.UI;

public class SenseSlider : MonoBehaviour
{
    public Player player;
    private Slider slider;
    private GameVariables gameVariables;

    private void Awake() {
        slider = GetComponent<Slider>();
        gameVariables = GameObject.Find("DontDestroyOnLoad").GetComponent<GameVariables>();
        slider.value = gameVariables.senseVal;
    }

    public void ChangeSense() {
        gameVariables.senseVal = slider.value;
    }
}