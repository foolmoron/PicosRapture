using System;

using UnityEngine;
using System.Collections;

[Serializable]
public struct SpriteWithColors {
    public SpriteRenderer Sprite;
    public Color[] PossibleColors;
}

[ExecuteInEditMode]
public class RandomPieceColors : MonoBehaviour {

    public SpriteWithColors[] SpritesWithColors;

    void Start() {
        for (int i = 0; i < SpritesWithColors.Length; i++) {
            var sprite = SpritesWithColors[i].Sprite;
            var colors = SpritesWithColors[i].PossibleColors;
            if (sprite != null && colors.Length > 0) {
                sprite.color = colors.random();
            }
        }
    }

    void Update() {
        if (!Application.isPlaying) {
            Start();
        }
    }
}