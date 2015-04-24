using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class FadeInAtY : MonoBehaviour {

    public float FadeInY;
    [Range(0.001f, 100)]
    public float FadeInPeriod = 50;
    SpriteRenderer spriteRenderer;

    void Start() {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update() {
        var currentY = transform.position.y;
        var normalizedAmountAboveFadePoint = (currentY - FadeInY) / FadeInPeriod;
        spriteRenderer.color = spriteRenderer.color.withAlpha(Mathf.Clamp01(normalizedAmountAboveFadePoint));
    }
}