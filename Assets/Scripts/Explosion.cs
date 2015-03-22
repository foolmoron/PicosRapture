using UnityEngine;
using System.Collections;

public class Explosion : MonoBehaviour {

    [Range(1, 60)]
    public int DestroyFrameDelay = 6;
    [Range(1, 60)]
    public int FlashDelay = 3;
    int frames;

    SpriteRenderer spriteRenderer;

    void Start() {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update() {
        if (frames < DestroyFrameDelay) {
            if (frames >= FlashDelay) {
                spriteRenderer.color = Color.black;
            }
            frames++;
        } else {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other) {
        var player = other.GetComponent<Player>();
        if (player) {
            player.CaughtInExplosion(this);
        }
    }
}
