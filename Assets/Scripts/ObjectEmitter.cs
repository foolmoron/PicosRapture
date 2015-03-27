using UnityEngine;
using System.Collections;

public class ObjectEmitter : MonoBehaviour {

    public GameObject ObjectToEmit;

    public bool HeightBasedEmit = true;
    public AnimationCurve EmitIntervalAtHeight;
    public float EmitHeightInterval = 2f;
    float previousEmitHeight;

    [Range(0, 10)]
    public float EmitWidth = 1f;
    [Range(0, 10)]
    public float EmitHeight = 0f;

    public void Emit(Vector3 emitPos) {
        var randomPos = new Vector3(emitPos.x + (2 * Random.value - 1) * EmitWidth, emitPos.y + (2 * Random.value - 1) * EmitHeight, emitPos.z);
        var newObj = (GameObject) Instantiate(ObjectToEmit, randomPos, Quaternion.Euler(0, 0, Random.value * 360));
        newObj.tag = "Emitted";
    }

    void Update() {
        // height-based emit
        if (HeightBasedEmit && (previousEmitHeight + EmitHeightInterval) <= transform.position.y) {
            Emit(transform.position.withY(previousEmitHeight + EmitHeightInterval));
            previousEmitHeight += EmitHeightInterval;

            EmitHeightInterval = EmitIntervalAtHeight.Evaluate(previousEmitHeight);
        }
    }

    public void Reset() {
        var allEmitted = GameObject.FindGameObjectsWithTag("Emitted");
        for (int i = 0; i < allEmitted.Length; i++) {
            Destroy(allEmitted[i]);
        }
        previousEmitHeight = 0;
    }

    void OnDrawGizmos() {
        Gizmos.color = Color.yellow;
        if (EmitHeight == 0 && EmitWidth == 0) {
            Gizmos.DrawWireSphere(transform.position, 1f);
        } else {
            var tl = transform.position + new Vector3(-EmitWidth, EmitHeight);
            var tr = transform.position + new Vector3(EmitWidth, EmitHeight);
            var bl = transform.position + new Vector3(-EmitWidth, -EmitHeight);
            var br = transform.position + new Vector3(EmitWidth, -EmitHeight);
            Gizmos.DrawLine(tl, tr);
            Gizmos.DrawLine(tr, br);
            Gizmos.DrawLine(br, bl);
            Gizmos.DrawLine(bl, tl);

            Gizmos.color = Color.green;
            Gizmos.DrawLine(transform.position.withX(-EmitWidth), transform.position.withX(EmitWidth));
        }
    }
}
