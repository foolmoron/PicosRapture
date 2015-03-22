using UnityEngine;

public class ConstantMotion : MonoBehaviour {

    public Vector3 MotionPerSecond;
    public Vector3 SpeedRandomness;
    public Vector3 NegativeRandomness;

    void Start() {
        var randomSpeed = Vector3.Scale(SpeedRandomness, new Vector3(Random.value, Random.value, Random.value));
        var randomNegatives = new Vector3(Random.value >= NegativeRandomness.x ? 1 : -1, Random.value >= NegativeRandomness.y ? 1 : -1, Random.value >= NegativeRandomness.z ? 1 : -1);
        MotionPerSecond = Vector3.Scale(MotionPerSecond + randomSpeed, randomNegatives);
    }
    
    void Update() {
        transform.position = transform.position + MotionPerSecond * Time.deltaTime;
    }
}
