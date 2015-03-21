using UnityEngine;
using System.Collections;

public class ExplodeOnContact : MonoBehaviour {

    [SplitRange(0f, 1, 50f)]
    public float AutoDestructTime = 2f;
    float autoDestructTime;

    public void Explode() {
        Destroy(GetComponent<Rigidbody2D>());
        Destroy(GetComponent<Collider2D>());
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
}
