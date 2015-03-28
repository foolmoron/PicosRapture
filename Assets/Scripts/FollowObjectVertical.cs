using UnityEngine;
using System.Collections;

public class FollowObjectVertical : MonoBehaviour {

    public Transform Target;

    void Update() {
        transform.position = transform.position.withY(Target.position.y);
    }
}