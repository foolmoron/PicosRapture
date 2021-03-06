﻿using UnityEngine;

public class HeightTracker : MonoBehaviour {

    public Transform Target;
    [SplitRange(0, 1, 100)]
    public float HeightMultiplierInText = 1;
    public float CurrentHighest;
    public float HighestEver;

    public TextMesh CurrentScoreText;
    public TextMesh BestScoreText;

    public Color RegularColor;
    public Color NewHighColor;

    void Start() {
        var emitter = FindObjectOfType<ObjectEmitter>();
        FindObjectOfType<GameOver>().OnGameOver += () => {
            if (CurrentHighest >= HighestEver) {
                CurrentScoreText.color = NewHighColor;
                BestScoreText.color = NewHighColor;
            }
            SaveHighscore();
        };
        FindObjectOfType<CharacterSelect>().OnCharacterSelect += () => {
            CurrentHighest = 0;
            CurrentScoreText.color = RegularColor;
            BestScoreText.color = RegularColor;
        };
        HighestEver = PlayerPrefs.GetFloat("highest");
    }

    void Update() {
        CurrentHighest = Mathf.Max(CurrentHighest, Target.position.y);

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
