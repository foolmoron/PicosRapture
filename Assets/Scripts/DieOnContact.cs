using UnityEngine;
using System.Collections;

public class DieOnContact : MonoBehaviour {

    public LayerMask DeadlyLayers;

    public GameObject SpawnOnDiePrefab;
    public Vector3 SpawnOnDiePosOffset;
    public Vector3 SpawnOnDieRotOffset;

    public void Die() {
        Destroy(gameObject);
        if (SpawnOnDiePrefab) {
            Instantiate(SpawnOnDiePrefab, transform.position + SpawnOnDiePosOffset, Quaternion.Euler(transform.rotation.eulerAngles + SpawnOnDieRotOffset));
        }
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
