using UnityEngine;

[ExecuteInEditMode]
public class PicoCamera : MonoBehaviour {

    public Player Player;
    Rigidbody2D playerRigidbody;
    public float OffsetY;
    public AnimationCurve VelocityToOffset;

    public Transform[] ShakeOnOffset;
    public Vector2 onShootOffset;
    [Range(0, 1)]
    public float onShootOffsetStrength = 0.1f;
    [Range(0, 0.5f)]
    public float onShootOffsetFade = 0.1f;
    Vector2 previousOffset;

    [Range(0, 1)]
    public float ShakeStrength = 0.00f;
    Vector3 previousShake;
    public AnimationCurve VelocityToShakeStrength;

    void Start() {
        playerRigidbody = Player.GetComponent<Rigidbody2D>();
        Player.OnShoot += shootDirection => onShootOffset = -shootDirection * onShootOffsetStrength;
    }

    void Update() {
        var velocityY = (playerRigidbody != null ? playerRigidbody : Player.GetComponent<Rigidbody2D>()).velocity.y;

        // do velocity-based offset
        {
            if (Application.isPlaying) {
                OffsetY = Mathf.Lerp(OffsetY, VelocityToOffset.Evaluate(velocityY), 0.05f);
            }
            transform.position = transform.position.withY(Player.transform.position.y + OffsetY);
        }
        // add shoot-based offset on top of that
        {
            for (int i = 0; i < ShakeOnOffset.Length; i++) {
                var obj = ShakeOnOffset[i];
                obj.position = obj.position.plusX(-previousOffset.x + onShootOffset.x);
            }
            transform.position = transform.position.plusY(-previousOffset.y + onShootOffset.y);
            previousOffset = onShootOffset;
            onShootOffset = Vector2.Lerp(onShootOffset, Vector2.zero, onShootOffsetFade);
        }
        // add screen shake at the end
        {
            if (Application.isPlaying) {
                ShakeStrength = VelocityToShakeStrength.Evaluate(velocityY);
            }
            if (ShakeStrength > 0.001f) { // compare versus low epsilon because anim curve float values aren't exactly 0 usually
                transform.localPosition -= previousShake;
                Vector3 shake = Random.insideUnitCircle.normalized * ShakeStrength;
                transform.localPosition += shake;
                previousShake = shake;
            } else {
                if (previousShake != Vector3.zero) {
                    transform.localPosition = transform.localPosition - previousShake;
                    previousShake = Vector3.zero;
                }
            }
        }
    }

    void OnDrawGizmos() {
        Gizmos.color = Color.red;
        if (Player) {
            Gizmos.DrawWireSphere(Player.transform.position.withX(transform.position.x).plusY(OffsetY), 1);
        }
    }

    void Shake() {
    }
}