using UnityEngine;
using System.Collections;

public class Explosion : MonoBehaviour {

    public AudioClip[] ExplosionSounds;

    void Start() {
        ExplosionSounds.random().Play();
    }

    void OnTriggerEnter2D(Collider2D other) {
        var player = other.GetComponent<Player>();
        if (player) {
            player.CaughtInExplosion(this);
        }
    }
}
