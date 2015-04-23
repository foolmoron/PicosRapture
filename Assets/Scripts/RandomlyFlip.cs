using UnityEngine;
using System.Collections;

public class RandomlyFlip : MonoBehaviour {

    public bool FlipX;
    public bool FlipY;

    void Start() {
        if (FlipX && Random.value >= 0.5f) {
            transform.localScale = transform.localScale.withX(-transform.localScale.x);
        }
        if (FlipY && Random.value >= 0.5f) {
            transform.localScale = transform.localScale.withY(-transform.localScale.y);
        }
    }
}