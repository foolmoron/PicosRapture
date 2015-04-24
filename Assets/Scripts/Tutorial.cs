using UnityEngine;
using System.Collections;

public class Tutorial : MonoBehaviour {

    [Range(0, 1)]
    public float Alpha = 1;
    public bool Hide;

    [Range(0, 5)]
    public float ShowDelay;
    float showDelay;

    SpriteRenderer[] sprites;
    Animator animator;

    void Awake() {
        sprites = GetComponentsInChildren<SpriteRenderer>();
        animator = GetComponent<Animator>();

        FindObjectOfType<CharacterSelect>().OnCharacterSelect += () => {
            Hide = false;
            if (PlayerPrefs.GetFloat("highest", 0) < 20f) {
                ShowDelay = 0;
            } else {
                ShowDelay = 2;
            }
            showDelay = 0;
        };
        FindObjectOfType<Player>().OnExploded += (explosion) => {
            Hide = true;
        };

    }

    void Start() {
        if (Hide) {
            Alpha = 0;
            for (int i = 0; i < sprites.Length; i++) {
                sprites[i].color.withAlpha(0);
            }
        }
    }

    void Update() {
        if (Hide) {
            Alpha = Mathf.Lerp(Alpha, 0, 0.1f);
            animator.enabled = false;
            showDelay = 0;
        } else {
            if (showDelay < ShowDelay) {
                showDelay += Time.deltaTime;
            } else {
                Alpha = Mathf.Lerp(Alpha, 1, 0.1f);
                animator.enabled = true;
            }
        }
        for (int i = 0; i < sprites.Length; i++) {
            sprites[i].color = sprites[i].color.withAlpha(Alpha);
        }
    }
}