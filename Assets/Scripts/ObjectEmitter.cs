using UnityEngine;
using System.Collections;

public class ObjectEmitter : MonoBehaviour {

    public GameObject ObjectToEmit;


    [Range(0.01f, 10f)]
    public float EmitInterval = 1f;
    float emitTime;


    [Range(0, 10)]
    public float EmitWidth = 1f;
    [Range(0, 10)]
    public float EmitHeight = 0f;

    public void Emit() {
        var pos = new Vector3(transform.position.x + (2 * Random.value - 1) * EmitWidth, transform.position.y + (2 * Random.value - 1) * EmitHeight, transform.position.z);
        var newObj = (GameObject) Instantiate(ObjectToEmit, pos, Quaternion.Euler(0, 0, Random.value * 360));
    }

    void Update() {
        if (emitTime < EmitInterval) {
            emitTime += Time.deltaTime;
            if (emitTime >= EmitInterval) {
                Emit();
                emitTime -= EmitInterval;
            }
        }
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
        }
    }
}
