using System;

using UnityEngine;
using System.Collections;

public class CharacterSelect : MonoBehaviour {

    public Camera MainCamera;
    public Player Player;
    public PlayerPacks PlayerPacks;

    public int CurrentCharacter;
    public GameObject[] Characters;
    public SpriteRenderer[] CharacterSprites;
    public Vector3 CharacterScale;
    [Range(0, 5)]
    public float CharacterSpacing;
    [Range(0, 1)]
    public float LerpSpeed = 0.1f;

    public Collider2D SelectCollider;
    public Collider2D LeftCollider;
    public Collider2D RightCollider;
    public GameObject UpArrow;
    public GameObject Arrows;

    public bool Hidden;
    [Range(0, 5)]
    public float HiddenDelay = 1.5f;
    float hiddenDelay;
    public float HideYOffset = -2.5f;
    [Range(0, 1)]
    public float HideLerpSpeed = 0.1f;
    float originalY;

    public GameObject Logo;
    public float LogoHideYOffset = 4f;
    float logoOriginalY;

    WrapAround wrapAround;
    
    void Start() {
        wrapAround = GetComponent<WrapAround>();
        wrapAround.HorizontalWrapRadius = CharacterSpacing * PlayerPacks.Packs.Length / 2;

        originalY = transform.position.y;
        logoOriginalY = Logo.transform.localPosition.y;

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
            CurrentCharacter = index;
        };
    }

    void Update() {
        // check "button" collisions
        {
            if (!Hidden && Input.GetMouseButtonDown(0)) {
                var mouseWorld = MainCamera.ScreenToWorldPoint(Input.mousePosition);
                if (SelectCollider.OverlapPoint(mouseWorld)) {
                    var packIndex = Mathf.Clamp(CurrentCharacter, 0, PlayerPacks.Packs.Length - 1);
                    if (PlayerPacks.Packs[packIndex].Unlocked) {
                        PlayerPacks.SelectPlayerPack(packIndex);
                        Player.gameObject.SetActive(true);
                        Hidden = true;
                    }
                } else if (LeftCollider.OverlapPoint(mouseWorld)) {
                    CurrentCharacter = (CurrentCharacter - 1 + PlayerPacks.Packs.Length) % PlayerPacks.Packs.Length;
                } else if (RightCollider.OverlapPoint(mouseWorld)) {
                    CurrentCharacter = (CurrentCharacter + 1 + PlayerPacks.Packs.Length) % PlayerPacks.Packs.Length;
                }
            }
        }
        // toggle up arrow to indicate selected character locked/unlocked
        {
            var packIndex = Mathf.Clamp(CurrentCharacter, 0, PlayerPacks.Packs.Length - 1);
            UpArrow.SetActive(PlayerPacks.Packs[packIndex].Unlocked);
        }
        // smooth lerp to the current selection
        {
            var oldX = Characters[CurrentCharacter].transform.position.x;
            var newX = Mathf.Lerp(oldX, 0, LerpSpeed);
            var diff = newX - oldX;
            for (int i = 0; i < Characters.Length; i++) {
                Characters[i].transform.Translate(diff, 0, 0);
            }
        }
        // smooth lerp in and out of sight
        {
            if (Hidden) {
                hiddenDelay = 0;
                Arrows.gameObject.SetActive(false);
                transform.position = transform.position.withY(Mathf.Lerp(transform.position.y, originalY + HideYOffset, HideLerpSpeed));
                Logo.transform.localPosition = Logo.transform.localPosition.withY(Mathf.Lerp(Logo.transform.localPosition.y, logoOriginalY + LogoHideYOffset, HideLerpSpeed));
            } else {
                if (hiddenDelay < HiddenDelay) {
                    hiddenDelay += Time.deltaTime;
                } else {
                    Arrows.gameObject.SetActive(true);
                    transform.position = transform.position.withY(Mathf.Lerp(transform.position.y, originalY, HideLerpSpeed));
                    Logo.transform.localPosition = Logo.transform.localPosition.withY(Mathf.Lerp(Logo.transform.localPosition.y, logoOriginalY, HideLerpSpeed));
                }
            }
        }
    }
}