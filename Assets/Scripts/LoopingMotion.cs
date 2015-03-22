using UnityEngine;

public class LoopingMotion : MonoBehaviour {

    public AnimationCurve XPosCurve;
    public AnimationCurve YPosCurve;
    public AnimationCurve ZPosCurve;
    public Vector3 TimeMultiplier = Vector3.one;
    public Vector3 TimeMultiplierRandomness;

    float time;
    float previousXOffset;
    float previousYOffset;
    float previousZOffset;

    void Start() {
        TimeMultiplier += new Vector3((Random.value * 2 - 1) * TimeMultiplierRandomness.x, (Random.value * 2 - 1) * TimeMultiplierRandomness.y, (Random.value * 2 - 1) * TimeMultiplierRandomness.z);
    }

    void Update() {
        time += Time.deltaTime;
        var xOffset = XPosCurve.Evaluate(time * TimeMultiplier.x);
        var yOffset = YPosCurve.Evaluate(time * TimeMultiplier.y);
        var zOffset = ZPosCurve.Evaluate(time * TimeMultiplier.z);
        transform.position = new Vector3(transform.position.x - previousXOffset + xOffset, transform.position.y - previousYOffset + yOffset, transform.position.z - previousZOffset + zOffset);
        previousXOffset = xOffset;
        previousYOffset = yOffset;
        previousZOffset = zOffset;
    }
}
