using UnityEngine;
using UnityEngine.UI;

public class PlayerModeTracker : MonoBehaviour {

    Player player;
    Text text;

    void Start() {
        player = FindObjectOfType<Player>();
        text = GetComponent<Text>();
    }

    void Update() {
        text.text = ((player.AutoFireControls) ? "auto" : "semi") + "-" + ((player.ScreenRelativeControls) ? "relative" : "absolute");
    }
}
