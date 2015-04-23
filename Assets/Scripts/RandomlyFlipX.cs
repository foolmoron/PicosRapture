using UnityEngine;
using System.Collections;

public class RandomlyFlipX : MonoBehaviour {

    void Start() {
        if (Random.value >= 0.5f) {
            transform.localScale = transform.localScale.withX(-transform.localScale.x);
        }
    }
}