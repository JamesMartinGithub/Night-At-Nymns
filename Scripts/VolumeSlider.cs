using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    public AudioMixer mixer;
    private Slider slider;
    private GameVariables gameVariables;

    private void Awake() {
        slider = GetComponent<Slider>();
        gameVariables = GameObject.Find("DontDestroyOnLoad").GetComponent<GameVariables>();
        slider.value = gameVariables.volumeSliderVal;
    }

    public void ChangeVolume() {
         mixer.SetFloat("Volume", Mathf.Lerp(-15, 15, slider.value));
        gameVariables.volumeSliderVal = slider.value;
    }
}