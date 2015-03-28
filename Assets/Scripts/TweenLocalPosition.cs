using UnityEngine;
using System.Collections;


public class TweenLocalPosition : MonoBehaviour {

    public Vector3 TargetLocalPosition;
    [Range(0, 1)]
    public float TweenSpeed = 0.1f;

    void Update() {
        transform.localPosition = Vector3.Lerp(transform.localPosition, TargetLocalPosition, TweenSpeed);
    }
}