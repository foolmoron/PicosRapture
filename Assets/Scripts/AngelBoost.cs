using System.Security.Policy;

using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class AngelBoost : MonoBehaviour {

    public bool HasAngelBoost;
    public GameObject[] AngelBoostObjects;

    [Range(0, 50)]
    public int AngelsToKillPerBoost = 20;
    [Range(0, 50)]
    public int AngelsUntilNextBoost = 20;
    public bool BossExistsAlready;

    public float CurrentHeighest;
    [Range(0, 100)]
    public float FallDistanceToSave = 20;
    [Range(0f, 5000f)]
    public float BoostForce = 1000;

    public AudioClip BoostSound;

    BossEmitter bossEmitter;

    Rigidbody2D targetRigidbody;
    PlayerPacks playerPacks;

    bool playerHasFirstExploded;

    void Start() {
        bossEmitter = FindObjectOfType<BossEmitter>();
        targetRigidbody = GetComponent<Rigidbody2D>();
        playerPacks = GetComponent<PlayerPacks>();
        GetComponent<Player>().OnExploded += explosion => {
            if (!playerHasFirstExploded) {
                AngelsUntilNextBoost = AngelsToKillPerBoost;
                playerHasFirstExploded = true;
            }
        };
        FindObjectOfType<GameOver>().OnGameOver += () => {
            CurrentHeighest = 0;
            playerHasFirstExploded = false;
            HasAngelBoost = false;
            BossExistsAlready = false;
            
            var allBosses = FindObjectsOfType<Boss>();
            for (int i = 0; i < allBosses.Length; i++) {
                allBosses[i].DieSilently = true;
                allBosses[i].GetComponent<DieOnContact>().Die();
            }
        };
    }

    public void ReportAngelKilled() {
        if (!BossExistsAlready && playerHasFirstExploded) {
            AngelsUntilNextBoost--;
        }
        if (AngelsUntilNextBoost <= 0) {
            bossEmitter.EmitBoss(playerPacks, playerPacks.CurrentPackIndex);
            BossExistsAlready = true;
            AngelsUntilNextBoost = AngelsToKillPerBoost;
        }
    }

    public void ReportBossKilled() {
        HasAngelBoost = true;
        BossExistsAlready = false;
    }

    void Update() {
        // toggle stuff based on whether Angel Boost is available
        {
            for (int i = 0; i < AngelBoostObjects.Length; i++) {
                AngelBoostObjects[i].SetActive(HasAngelBoost);
            }
        }

        if (!Application.isPlaying) return;

        // keep track of highest position
        {
            CurrentHeighest = Mathf.Max(CurrentHeighest, targetRigidbody.position.y);
        }
        // activate boost if player falls a certain amount below max
        {
            if (HasAngelBoost && (targetRigidbody.position.y + FallDistanceToSave) <= CurrentHeighest) {
                targetRigidbody.velocity = targetRigidbody.velocity.withY(0);
                targetRigidbody.AddForce(new Vector2(0, BoostForce));
                HasAngelBoost = false;
                BoostSound.Play();
            }
        }
    }

    void OnDrawGizmos() {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position.withY(CurrentHeighest - FallDistanceToSave).plusX(-5), transform.position.withY(CurrentHeighest - FallDistanceToSave).plusX(5));
    }
}
