using System.Collections;

using UnityEngine;

public class BossEmitter : MonoBehaviour {

    public GameObject BossPrefab;
    public Transform TargetX;
    public Transform TargetY;
    public Transform AimingTarget;

    void Start() {
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
        boss.TargetX = TargetX;
        boss.TargetY = TargetY;
        boss.AimingTarget = AimingTarget;

        var randomIndex = Mathf.FloorToInt(Random.value * (packs.Packs.Length - 1));
        if (randomIndex == packToExclude)
            randomIndex = packs.Packs.Length - 1;
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