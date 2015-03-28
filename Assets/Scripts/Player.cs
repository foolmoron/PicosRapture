using System;

using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    public event Action<Vector2> OnShoot = delegate { };

    public GameObject BulletPrefab;
    [Range(0f, 100f)]
    public float BulletSpeed = 40;
    [Range(0f, 10f)]
    public float ShootHorizontalForce = 2;
    [Range(0f, 50f)]
    public float ShootDiveForce = 10;
    [Range(0f, 3f)]
    public float ShootHoverVelocity = 1f;

    public bool CanBeExploded = true;
    int framesSinceLastExplosion;
    [Range(0f, 5000f)]
    public float ExplosionForce = 1000;

    [Range(0f, 1f)]
    public float AutoFireTimerMax = 0.2f;
    [Range(0f, 1f)]
    public float AutoFireTimer;
    [Range(0f, 1f)]
    public float ShootInterval = 0.1f;
    [Range(0f, 1f)]
    public float TimeToShoot;
    [Range(1, 0)]
    public float x;

    new Rigidbody2D rigidbody2D;
    WeaponRoot weaponRoot;

    void Start() {
        rigidbody2D = GetComponent<Rigidbody2D>();
        weaponRoot = GetComponentInChildren<WeaponRoot>();
    }

    void Update() {
        // rotate weapon to mouse
        {
            var vectorToMouse = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
            weaponRoot.Rotation = Mathf.Atan2(vectorToMouse.y, vectorToMouse.x) * Mathf.Rad2Deg;
        }
        // handle shooting bullet
        {
            if (Input.GetMouseButton(0) && AutoFireTimer <= 0) { 
                TimeToShoot = 0; // fire instantly on click if you're not already in auto fire mode
            } else if (Input.GetMouseButtonUp(0)) { 
                AutoFireTimer = 0; // immediately turn off auto fire when letting go
            }

            if (Input.GetMouseButton(0)) {
                AutoFireTimer = AutoFireTimerMax;
            }
            if (AutoFireTimer > 0) {
                AutoFireTimer -= Time.deltaTime;
                TimeToShoot -= Time.deltaTime;
                if (TimeToShoot <= 0) {
                    // fire
                    {
                        var newBulletObj = (GameObject) Instantiate(BulletPrefab, transform.position, Quaternion.identity);

                        Vector2 shootDirection = (Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position)).normalized;

                        newBulletObj.GetComponent<Rigidbody2D>().velocity = shootDirection * BulletSpeed;
                        newBulletObj.transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(shootDirection.y, shootDirection.x) * Mathf.Rad2Deg);
                        weaponRoot.kickback = -shootDirection * weaponRoot.kickbackStrength;

                        var newVelocityX = -Mathf.Sign(shootDirection.x) * (shootDirection.x * shootDirection.x) * ShootHorizontalForce; // simple movement when aiming in a direction, no acceleration
                        var newVelocityY = rigidbody2D.velocity.y;
                        if (newVelocityY < 0 && shootDirection.y < 0) {
                            newVelocityY = Mathf.Lerp(newVelocityY, ShootHoverVelocity, Mathf.Pow(-shootDirection.y, x));
                        }
                        rigidbody2D.velocity = new Vector2(newVelocityX, newVelocityY);
                        OnShoot(shootDirection);
                    }
                    TimeToShoot += ShootInterval;
                }
            } else {
                rigidbody2D.velocity = rigidbody2D.velocity.withX(Mathf.Lerp(rigidbody2D.velocity.x, 0, 0.05f)); // decelerate X movement when not firing
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
            rigidbody2D.velocity = rigidbody2D.velocity.withY(0); // reset Y so explosion jump height is very predictable
            rigidbody2D.AddForce(new Vector2(0, ExplosionForce)); // explodes upwards no matter what
            CanBeExploded = false;
            framesSinceLastExplosion = 0;
        }
    }
}
