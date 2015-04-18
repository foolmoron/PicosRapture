using System;

using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour {

    public event Action OnGameOver = delegate { };

    public Collider2D Target;
    public bool CanGameOver;
    new Collider2D collider;

    public ParticleSystem GameOverParticles;
    public Vector3 ParticlesOffset;
    public Vector3 ParticlesRotation;

    void Start() {
        collider = GetComponent<Collider2D>();
    }
    
    void FixedUpdate() {
        if (CanGameOver) {
            if (collider.IsTouching(Target)) {
                var velocity = Target.GetComponent<Player>().PreviousDownwardsVelocity;
                var newParticles = (ParticleSystem) Instantiate(GameOverParticles, transform.position.withX(Target.transform.position.x) + ParticlesOffset, Quaternion.Euler(ParticlesRotation));
                newParticles.maxParticles = Mathf.FloorToInt(-velocity);

                OnGameOver();
                CanGameOver = false;
            }
        } else {
            if (!collider.IsTouching(Target)) {
                CanGameOver = true;
            }
        }
    }
}
