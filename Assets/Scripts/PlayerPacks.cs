using System;

using UnityEngine;
using System.Collections;

[Serializable]
public class PlayerPack {
    public string name = "N/A";
    public Sprite CharacterSprite;
    public Sprite WeaponSprite;
    public GameObject BulletPrefab;
}
[ExecuteInEditMode]
public class PlayerPacks : MonoBehaviour {

    public PlayerPack[] Packs;
    public int CurrentPackIndex = 0;
    int previousIndex;

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
        var characterSprite = player.transform.FindChild("Sprite").GetComponent<SpriteRenderer>();
        var weapon = player.transform.FindChild("WeaponRoot/Weapon").GetComponent<SpriteRenderer>();

        player.BulletPrefab = pack.BulletPrefab;
        characterSprite.sprite = pack.CharacterSprite;
        weapon.sprite = pack.WeaponSprite;
    }
}