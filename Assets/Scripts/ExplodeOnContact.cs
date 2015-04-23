using UnityEngine;
using System.Collections;

public class ExplodeOnContact : MonoBehaviour {

    [SplitRange(0f, 1, 50f)]
    public float AutoDestructTime = 2f;
    float autoDestructTime;

    public GameObject ExplosionPrefab;
    public Vector3 ExplosionOffset;

    public void Explode() {
        Destroy(gameObject);

        if (ExplosionPrefab) {
            Instantiate(ExplosionPrefab, transform.position + ExplosionOffset, Quaternion.identity);
        }
    }

    void Update() {
        if (autoDestructTime < AutoDestructTime) {
            autoDestructTime += Time.deltaTime;
            if (autoDestructTime >= AutoDestructTime) {
                Explode();
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other) {
        Explode();
    }

    void OnCollisionEnter2D(Collision2D coll) {
        Explode();
    }
}
