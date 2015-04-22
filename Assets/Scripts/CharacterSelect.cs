using System;

using UnityEngine;
using System.Collections;

public class CharacterSelect : MonoBehaviour {

    public Player Player;
    public PlayerPacks PlayerPacks;

    public GameObject[] Characters;
    public SpriteRenderer[] CharacterSprites;
    public Vector3 CharacterScale;
    [Range(0, 5)]
    public float CharacterSpacing;

    WrapAround wrapAround;
    
    void Start() {
        wrapAround = GetComponent<WrapAround>();
        wrapAround.HorizontalWrapRadius = CharacterSpacing * PlayerPacks.Packs.Length / 2;

        Characters = new GameObject[PlayerPacks.Packs.Length];
        CharacterSprites = new SpriteRenderer[PlayerPacks.Packs.Length];
        wrapAround.ObjectsToWrapAround = new Transform[PlayerPacks.Packs.Length];
        for (int i = 0; i < PlayerPacks.Packs.Length; i++) {
            var pack = PlayerPacks.Packs[i];
            
            var newCharacter = new GameObject(pack.name + "Option");
            newCharacter.transform.parent = transform;
            newCharacter.transform.localPosition = new Vector3(CharacterSpacing * i, 0, 0);
            newCharacter.transform.localScale = CharacterScale;
            Characters[i] = newCharacter;

            var sprite = newCharacter.AddComponent<SpriteRenderer>();
            sprite.sprite = pack.Unlocked ? pack.CharacterSprite : pack.LockedCharacterSprite;
            CharacterSprites[i] = sprite;

            wrapAround.ObjectsToWrapAround[i] = newCharacter.transform;
        }

        PlayerPacks.OnCharacterUnlocked += (pack, index) => {
            CharacterSprites[index].sprite = pack.CharacterSprite;
            var offsetToCenter = -Characters[index].transform.position.x;
            for (int i = 0; i < Characters.Length; i++) {
                Characters[i].transform.Translate(offsetToCenter, 0, 0);
            }
        };
    }
}