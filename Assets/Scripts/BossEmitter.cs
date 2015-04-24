using System.Collections;

using UnityEngine;

public class BossEmitter : MonoBehaviour {

    public Player Player;
    public GameObject BossPrefab;
    public Transform TargetX;
    public Transform TargetY;
    public Transform AimingTarget;

    Rigidbody2D playerRigidbody;
    AngelBoost angelBoost;

    void Start() {
        playerRigidbody = Player.GetComponent<Rigidbody2D>();
        angelBoost = Player.GetComponent<AngelBoost>();
    }

    public void EmitBoss(PlayerPacks packs, int packToExclude) {
        if (this)
            StartCoroutine(EmitNextFrame(packs, packToExclude));
    }

    IEnumerator EmitNextFrame(PlayerPacks packs, int packToExclude) {
        yield return new WaitForEndOfFrame();

        var bossObj = (GameObject) Instantiate(BossPrefab, transform.position.plusX((Random.value - 0.5f) * 8), transform.rotation);
        bossObj.transform.parent = transform.parent;

        var boss = bossObj.GetComponent<Boss>();
        boss.PlayerRigidbody = playerRigidbody;
        boss.AngelBoost = angelBoost;
        boss.TargetX = TargetX;
        boss.TargetY = TargetY;
        boss.AimingTarget = AimingTarget;


        var currentPlayerY = playerRigidbody.transform.position.y;
        var allowedIndexes = new int[packs.Packs.Length];
        var allowedIndexCount = 0;
        for (int i = 0; i < packs.Packs.Length; i++) {
            if (i != packToExclude && packs.Packs[i].MinHeight <= currentPlayerY) {
                allowedIndexes[allowedIndexCount] = i;
                allowedIndexCount++;
            }
        }

        var randomIndex = allowedIndexes[Mathf.FloorToInt(Random.value * allowedIndexCount)];
        var pack = packs.Packs[randomIndex];

        boss.PlayerPacks = packs;
        boss.PlayerPackIndex = randomIndex;
        boss.BulletPrefab = pack.BulletPrefab;
        bossObj.transform.FindChild("Sprite").GetComponent<SpriteRenderer>().sprite = pack.CharacterSprite;
        bossObj.transform.FindChild("WeaponRoot/Weapon").GetComponent<SpriteRenderer>().sprite = pack.WeaponSprite;
    }

    void Update() {
    }

    void OnDrawGizmos() {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(new Vector3(TargetX.position.x, TargetY.position.y), 1);
    }
}