using UnityEngine;
using System.Collections;

public class Explosion : MonoBehaviour {
    void OnTriggerEnter2D(Collider2D other) {
        var player = other.GetComponent<Player>();
        if (player) {
            player.CaughtInExplosion(this);
        }
    }
}
