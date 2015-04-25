using System;

using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour {

    public event Action OnGameOver = delegate { };

    public Player Player;
    public CharacterSelect CharacterSelect;

    public Collider2D Target;
    public bool CanGameOver;
    new Collider2D collider;

    public AudioClip SplatterSound;

    public ParticleSystem GameOverParticles;
    public Transform ParticleTransform;

    Vector3 originalPlayerPosition;

    float canGameOverDelayTime = 1;
    float canGameOverDelay;

    void Start() {
        collider = GetComponent<Collider2D>();
        originalPlayerPosition = Player.transform.position;
    }
    
    void FixedUpdate() {
        if (CanGameOver) {
            if (collider.IsTouching(Target)) {
                var velocity = Target.GetComponent<Player>().PreviousDownwardsVelocity;
                var newParticles = (ParticleSystem) Instantiate(GameOverParticles, ParticleTransform.position.withX(Target.transform.position.x), ParticleTransform.rotation);
                newParticles.maxParticles = Mathf.FloorToInt(-velocity);

                Player.gameObject.SetActive(false);
                Player.transform.position = originalPlayerPosition;
                Player.WaitUntilMouseReleased = true;
                CharacterSelect.Hidden = false;

                OnGameOver();
                CanGameOver = false;
                canGameOverDelay = 0;
                SplatterSound.Play();
            }
        } else {
            if (Player.gameObject.activeSelf && canGameOverDelay < canGameOverDelayTime) {
                canGameOverDelay += Time.deltaTime;
            } else if (Player.gameObject.activeSelf) {
                if (!collider.IsTouching(Target)) {
                    CanGameOver = true;
                }
            }
        }
    }
}
