using UnityEngine;

public class Boss : MonoBehaviour {

    public GameObject BulletPrefab;
    public string BulletLayer;
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
    public GameObject BloodPrefab;

    AngelBoost angelBoost;
    new Rigidbody2D rigidbody;
    WeaponRoot weaponRoot;
    GameObject graphic;
    
    void Start() {
        angelBoost = FindObjectOfType<AngelBoost>();
        rigidbody = GetComponent<Rigidbody2D>();
        rigidbody.velocity = new Vector2(0, 0);
        weaponRoot = GetComponentInChildren<WeaponRoot>();
        graphic = transform.FindChild("Sprite").gameObject;
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
                    newBulletObj.layer = LayerMask.NameToLayer(BulletLayer);
                    newBulletObj.GetComponent<Collider2D>().isTrigger = true;
                    newBulletObj.GetComponent<ExplodeOnContact>().ExplosionPrefab = BloodPrefab;
                    newBulletObj.GetComponent<ExplodeOnContact>().ExplosionOffset = new Vector3(0, 0, -50);

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
        // rotate based on velocity
        {
            float targetAngle = Mathf.Acos(Mathf.Clamp(rigidbody.velocity.x * 0.15f, -1, 1)) * Mathf.Rad2Deg - 90;
            var currentAngle = graphic.transform.localRotation.eulerAngles.z;
            graphic.transform.localRotation = Quaternion.Euler(0, 0, Mathf.LerpAngle(currentAngle.rotationNormalizedDeg(), targetAngle.rotationNormalizedDeg(), 0.25f));
        }
    }

    void OnDestroy() {
        angelBoost.ReportBossKilled();
    }
}