using System;

using UnityEngine;
using System.Collections;

[Serializable]
public class PlayerPack {
    public string name = "N/A";
    public Sprite CharacterSprite;
    public Sprite LockedCharacterSprite;
    public Sprite WeaponSprite;
    public GameObject BulletPrefab;
    public float MinHeight;
    public bool Unlocked;
}
[ExecuteInEditMode]
public class PlayerPacks : MonoBehaviour {
    public event Action<PlayerPack, int> OnCharacterUnlocked = delegate { };

    public PlayerPack[] Packs;
    public int CurrentPackIndex = 0;
    int previousIndex;

    void Awake() {
        PlayerPrefs.DeleteAll(); // for debug
        PlayerPrefs.SetInt(Packs[0].name, 1);
        for (int i = 0; i < Packs.Length; i++) {
            Packs[i].Unlocked = PlayerPrefs.GetInt(Packs[i].name, 0) != 0;
        }
        if (Application.isPlaying) {
            gameObject.SetActive(false);
        }
    }

    void Update() {
        if (CurrentPackIndex != previousIndex) {
            SelectPlayerPack(CurrentPackIndex);
        }
    }

    public void UnlockPlayer(int index) {
        PlayerPrefs.SetInt(Packs[index].name, 1);
        Packs[index].Unlocked = true;
        OnCharacterUnlocked(Packs[index], index);
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