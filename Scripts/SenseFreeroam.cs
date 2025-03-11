using UnityEngine;

public class SenseFreeroam : MonoBehaviour
{
    public PlayerFreeroam player;
    private GameVariables gameVariables;

    private void Awake() {
        gameVariables = GameObject.Find("DontDestroyOnLoad").GetComponent<GameVariables>();
        player.speedMult = gameVariables.senseVal;
    }
}