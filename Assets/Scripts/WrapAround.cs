using UnityEngine;
using System.Collections;

public class WrapAround : MonoBehaviour {

    public Transform[] ObjectsToWrapAround;

    [Range(0, 100)]
    public float VerticalWrapRadius = 20f;
    [Range(0, 100)]
    public float HorizontalWrapRadius = 10f;

    void Update() {
        var currentX = transform.position.x;
        var currentY = transform.position.y;
        for (int i = 0; i < ObjectsToWrapAround.Length; i++) {
            var obj = ObjectsToWrapAround[i];

            var diffX = (obj.position.x - currentX);
            if (diffX > HorizontalWrapRadius) {
                obj.position = obj.position.withX(currentX - HorizontalWrapRadius + (diffX - HorizontalWrapRadius));
            } else if (diffX < -HorizontalWrapRadius) {
                obj.position = obj.position.withX(currentX + HorizontalWrapRadius + (diffX + HorizontalWrapRadius));
            }

            var diffY = (obj.position.y - currentY);
            if (diffY > VerticalWrapRadius) {
                obj.position = obj.position.withY(currentY - VerticalWrapRadius + (diffY - VerticalWrapRadius));
            } else if (diffY < -VerticalWrapRadius) {
                obj.position = obj.position.withY(currentY + VerticalWrapRadius + (diffY + VerticalWrapRadius));
            }
        }
    }

    void OnDrawGizmos() {
        Gizmos.color = Color.blue;
        var tl = transform.position + new Vector3(-HorizontalWrapRadius, VerticalWrapRadius);
        var tr = transform.position + new Vector3(HorizontalWrapRadius, VerticalWrapRadius);
        var bl = transform.position + new Vector3(-HorizontalWrapRadius, -VerticalWrapRadius);
        var br = transform.position + new Vector3(HorizontalWrapRadius, -VerticalWrapRadius);
        Gizmos.DrawLine(tl, tr);
        Gizmos.DrawLine(tr, br);
        Gizmos.DrawLine(br, bl);
        Gizmos.DrawLine(bl, tl);
    }
}
