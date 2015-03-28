using System;

using UnityEditor;

using UnityEngine;
using System.Collections;

[Serializable]
public class PlayerPack {
    public string name = "N/A";
    public Color HairColor = Color.white;
    public Color FaceColor = Color.white;
    public Color BodyColor = Color.white;
    public Color FeetColor = Color.white;
    public Sprite WeaponSprite;
    public GameObject BulletPrefab;
}
[ExecuteInEditMode]
public class PlayerPacks : MonoBehaviour {

    public PlayerPack[] Packs;
    public int CurrentPackIndex = 0;
    int previousIndex;

    //Player player;
    //SpriteRenderer hair;
    //SpriteRenderer face;
    //SpriteRenderer body;
    //SpriteRenderer feet;
    //SpriteRenderer weapon;

    void Start() {
        SelectPlayerPack(0);
    }

    void Update() {
        if (CurrentPackIndex != previousIndex) {
            SelectPlayerPack(CurrentPackIndex);
        }
    }

    public void CyclePlayerPack() {
        SelectPlayerPack(CurrentPackIndex + 1);
    }

    public void SelectPlayerPack(int index) {
        CurrentPackIndex = ((index % Packs.Length) + Packs.Length) % Packs.Length;
        previousIndex = CurrentPackIndex;
        var pack = Packs[CurrentPackIndex];

        var player = GetComponent<Player>();
        var hair = player.transform.FindChild("Sprite/Hair").GetComponent<SpriteRenderer>();
        var face = player.transform.FindChild("Sprite/Face").GetComponent<SpriteRenderer>();
        var body = player.transform.FindChild("Sprite/Body").GetComponent<SpriteRenderer>();
        var feet = player.transform.FindChild("Sprite/Feet").GetComponent<SpriteRenderer>();
        var weapon = player.transform.FindChild("WeaponRoot/Weapon").GetComponent<SpriteRenderer>();

        player.BulletPrefab = pack.BulletPrefab;
        hair.color = pack.HairColor;
        face.color = pack.FaceColor;
        body.color = pack.BodyColor;
        feet.color = pack.FeetColor;
        weapon.sprite = pack.WeaponSprite;
    }
}