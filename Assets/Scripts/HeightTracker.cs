using UnityEngine;
using UnityEngine.UI;

public class HeightTracker : MonoBehaviour {

    public Transform Target;
    [SplitRange(0, 1, 100)]
    public float HeightMultiplierInText = 1;
    public float CurrentHighest;
    public float HighestEver;

    public Text CurrentScoreText;
    public Text BestScoreText;

    void Start() {
        var emitter = FindObjectOfType<ObjectEmitter>();
        FindObjectOfType<GameOver>().OnGameOver += () => {
            SaveHighscore();
            CurrentHighest = 0;
            emitter.Reset();
        };
        HighestEver = PlayerPrefs.GetFloat("highest");
    }

    void Update() {
        var currentHeight = Target.position.y;

        if (currentHeight > CurrentHighest) {
            CurrentHighest = currentHeight;
        }
        CurrentScoreText.text = (CurrentHighest * HeightMultiplierInText).ToString("0.00") + "m";
        BestScoreText.text = "Best: " + (HighestEver * HeightMultiplierInText).ToString("0.00") + "m";
    }

    public void SaveHighscore() {
        if (CurrentHighest > HighestEver) {
            HighestEver = CurrentHighest;
        }
        PlayerPrefs.SetFloat("highest", HighestEver);
    }
}
