using UnityEngine;
using System.Collections;


public class TweenColliderSize : MonoBehaviour {

    public float TargetSize = 0.7f;
    [Range(0, 1)]
    public float TweenSpeed = 0.1f;

    new CircleCollider2D collider;

    void Start() {
        collider = GetComponent<CircleCollider2D>();
    }

    void Update() {
        collider.radius = Mathf.Lerp(collider.radius, TargetSize, TweenSpeed);
    }
}