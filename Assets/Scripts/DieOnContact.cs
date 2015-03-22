using UnityEngine;
using System.Collections;

public class DieOnContact : MonoBehaviour {

    public LayerMask DeadlyLayers;

    public void Die() {
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (((1 << other.gameObject.layer) & DeadlyLayers) != 0)
            Die();
    }

    void OnCollisionEnter2D(Collision2D coll) {
        if (((1 << coll.gameObject.layer) & DeadlyLayers) != 0)
            Die();
    }
}
