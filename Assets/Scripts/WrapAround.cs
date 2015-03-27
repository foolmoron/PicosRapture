using UnityEngine;
using System.Collections;

public class WrapAround : MonoBehaviour {

    public Transform[] ObjectsToWrapAround;

    [Range(0, 100)]
    public float VerticalWrapRadius = 20f;
    [Range(0, 100)]
    public float HorizontalWrapRadius = 10f;

    public bool RandomizeVertical;
    public bool RandomizeHorizontal;

    void Update() {
        var currentX = transform.position.x;
        var currentY = transform.position.y;
        for (int i = 0; i < ObjectsToWrapAround.Length; i++) {
            var obj = ObjectsToWrapAround[i];

            var diffX = (obj.position.x - currentX);
            if (diffX > HorizontalWrapRadius || diffX < -HorizontalWrapRadius) {
                var sign = Mathf.Sign(diffX);
                obj.position = obj.position.withX(currentX - (sign * HorizontalWrapRadius) + (diffX - (sign * HorizontalWrapRadius)));
                if (RandomizeVertical) {
                    obj.position = obj.position.withY(transform.position.y + Random.Range(-VerticalWrapRadius, VerticalWrapRadius));
                }
            }

            var diffY = (obj.position.y - currentY);
            if (diffY > VerticalWrapRadius || diffY < -VerticalWrapRadius) {
                var sign = Mathf.Sign(diffY);
                obj.position = obj.position.withY(currentY - (sign * VerticalWrapRadius) + (diffY - (sign * VerticalWrapRadius)));
                if (RandomizeHorizontal) {
                    obj.position = obj.position.withX(transform.position.x + Random.Range(-HorizontalWrapRadius, HorizontalWrapRadius));
                }
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
