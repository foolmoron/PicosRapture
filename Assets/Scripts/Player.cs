using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    public GameObject BulletPrefab;
    [Range(0f, 100f)]
    public float BulletSpeed = 40;
    [Range(0f, 1000f)]
    public float ShootForce = 200;
    [Range(0f, 100f)]
    public float MaxVerticalVelocity = 5;

    new Rigidbody2D rigidbody2D;

    void Start() {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    void Update() {
        // handle shooting bullet
        if (Input.GetMouseButtonDown(0)) {
            var newBulletObj = (GameObject) Instantiate(BulletPrefab, transform.position, Quaternion.identity);

            var mouseDir = (Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position)).normalized;
            newBulletObj.GetComponent<Rigidbody2D>().velocity = mouseDir * BulletSpeed;

            if (mouseDir.y < 0)
                rigidbody2D.velocity = rigidbody2D.velocity.withY(0);
            rigidbody2D.AddForce(-mouseDir * ShootForce);
        }
    }
}
