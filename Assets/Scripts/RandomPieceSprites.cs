using System;

using UnityEngine;
using System.Collections;

[Serializable]
public struct SpriteWithSprites {
    public SpriteRenderer Sprite;
    public Sprite[] PossibleSprites;
}

[ExecuteInEditMode]
public class RandomPieceSprites : MonoBehaviour {

    public SpriteWithSprites[] SpritesWithSprites;

    void Start() {
        for (int i = 0; i < SpritesWithSprites.Length; i++) {
            var spriteRenderer = SpritesWithSprites[i].Sprite;
            var sprites = SpritesWithSprites[i].PossibleSprites;
            if (spriteRenderer != null && sprites.Length > 0) {
                spriteRenderer.sprite = sprites.random();
            }
        }
    }

    void Update() {
        if (!Application.isPlaying) {
            Start();
        }
    }
}