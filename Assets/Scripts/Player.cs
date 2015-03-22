using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    public GameObject BulletPrefab;
    [Range(0f, 100f)]
    public float BulletSpeed = 40;
    [Range(0f, 1000f)]
    public float ShootHorizontalForce = 200;
    [Range(0f, 3f)]
    public float ShootWhenFallingVelocity = 1f;

    public bool CanBeExploded = true;
    int framesSinceLastExplosion;
    [Range(0f, 5000f)]
    public float ExplosionForce = 1000;
    public Vector2 ForceMultipier = new Vector2(0.5f, 1f);

    public bool ScreenRelativeControls;

    new Rigidbody2D rigidbody2D;

    void Start() {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    void Update() {
        // handle shooting bullet
        if (Input.GetMouseButtonDown(0)) {
            var newBulletObj = (GameObject) Instantiate(BulletPrefab, transform.position, Quaternion.identity);

            Vector2 shootDirection;
            if (ScreenRelativeControls) {
                shootDirection = (Camera.main.ScreenToViewportPoint(Input.mousePosition).to2() - (0.5f * Vector2.one)).normalized; // dir from center of screen to click
                shootDirection = shootDirection.withY(-Mathf.Abs(shootDirection.y)); // pretend click is always in bottom half of screen
                shootDirection = shootDirection.withY(Mathf.Clamp(shootDirection.y, -0.5f, -1f)); // pretend click is always far enough towards bottom of screen
            } else {
                shootDirection = (Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position)).normalized;
            }
            newBulletObj.GetComponent<Rigidbody2D>().velocity = shootDirection * BulletSpeed;

            rigidbody2D.AddForce(new Vector2(-shootDirection.x * ShootHorizontalForce, 0));
            if (rigidbody2D.velocity.y < 0 && shootDirection.y < 0) {
                rigidbody2D.velocity = rigidbody2D.velocity.withY(ShootWhenFallingVelocity);
            }
        }
    }

    void FixedUpdate() {
        // refresh explodability
        {
            if (framesSinceLastExplosion < 5) {
                framesSinceLastExplosion++;
            } else {
                CanBeExploded = true;
            }
        }
    }

    public void CaughtInExplosion(Explosion explosion) {
        if (CanBeExploded) {
            var vectorFromExplosion = (transform.position - explosion.transform.position);
            var multipliedNormalizedFromExplosion = Vector2.Scale(vectorFromExplosion, ForceMultipier).normalized;
            rigidbody2D.AddForce(multipliedNormalizedFromExplosion * ExplosionForce);
            CanBeExploded = false;
            framesSinceLastExplosion = 0;
        }
    }
}
