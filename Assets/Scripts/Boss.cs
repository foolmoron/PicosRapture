using UnityEngine;

public class Boss : MonoBehaviour {

    public GameObject BulletPrefab;
    public Transform TargetX;
    public Transform TargetY;
    public Transform AimingTarget;
    [Range(0, 100)]
    public float AimingVelocity = 10;
    [Range(0, 100)]
    public float AimingVariance = 20;
    [Range(0f, 100f)]
    public float BulletSpeed = 40;
    [Range(0, 100)]
    public float Acceleration = 10;
    [SplitRange(0f, 0.1f, 1f)]
    public float DampingFactor = 0.01f;

    [Range(0, 5)]
    public float TargetRandomness = 1f;
    Vector2 targetRandom;
    float timeSinceLastRandom;

    [Range(0, 10)]
    public float ShootInterval = 2f;
    [Range(0, 10)]
    public float TimeToShoot = 3f;

    new Rigidbody2D rigidbody;
    WeaponRoot weaponRoot;
    
    void Start() {
        rigidbody = GetComponent<Rigidbody2D>();
        rigidbody.velocity = new Vector2(0, 0);
        weaponRoot = GetComponentInChildren<WeaponRoot>();
    }

    void FixedUpdate() {
        // rotate weapon to mouse
        {
            var vectorToTarget = AimingTarget.position - transform.position;
            weaponRoot.Rotation = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
        }
        // redo target randomness
        {
            timeSinceLastRandom += Time.deltaTime;
            if (timeSinceLastRandom >= 1) {
                targetRandom = (Random.insideUnitCircle * TargetRandomness).normalized;
                timeSinceLastRandom = 0;
            }
        }
        // accelerate towards target position with slight randomness to get a sort of flying look
        {
            var target = new Vector2(TargetX.position.x, TargetY.position.y) + targetRandom;
            var vectorToTarget = target - transform.position.to2();
            rigidbody.velocity += vectorToTarget.normalized * (Acceleration * Time.deltaTime);
            rigidbody.velocity *= 1 - DampingFactor;
        }
        // shoot every interval
        {
            TimeToShoot -= Time.deltaTime;
            if (TimeToShoot <= 0) {
                // fire
                {
                    var newBulletObj = (GameObject) Instantiate(BulletPrefab, transform.position, Quaternion.identity);

                    Vector2 shootDirection = (AimingTarget.position - transform.position).normalized;
                    shootDirection = shootDirection.normalized;
                    shootDirection = shootDirection.Rotate((Random.value - 0.5f) * AimingVariance);

                    newBulletObj.GetComponent<Rigidbody2D>().velocity = shootDirection * BulletSpeed;
                    newBulletObj.transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(shootDirection.y, shootDirection.x) * Mathf.Rad2Deg);
                    weaponRoot.kickback = -shootDirection * weaponRoot.kickbackStrength;
                }
                TimeToShoot = ShootInterval;
            }
        }
    }
}