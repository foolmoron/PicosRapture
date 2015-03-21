using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SpriteRenderer))]
public class GhostPulser : MonoBehaviour {

    [Range(0.001f, 3f)]
    public float Duration = 0.5f;
    public Vector2 MoveAmount = new Vector2(0, 0);
    public Vector2 ScaleAmount = new Vector2(0, 0);

    [Range(0.001f, 3f)]
    public float PulseInterval = 0.4f;
    public bool AutoPulse = false;
    float pulseTime;

    public bool OnlyCloneSprite;

    SpriteRenderer spriteRenderer;

    void Awake() {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update() {
        if (AutoPulse) {
            pulseTime += Time.deltaTime;
            if (pulseTime >= PulseInterval) {
                Pulse();
            }
        }
    }
    
    public void Pulse() {
        Pulse(Duration, MoveAmount, ScaleAmount);
    }

    public void Pulse(float duration, Vector2 moveAmount, Vector2 scaleAmount) {
        if (spriteRenderer.enabled == false || spriteRenderer.sprite == null)
            return;

        pulseTime = 0;
        StartCoroutine(PulseThenDestroy(duration, moveAmount, scaleAmount));
    }

    IEnumerator PulseThenDestroy(float duration, Vector2 moveAmount, Vector2 scaleAmount) {
        GameObject newObj = null;
        if (OnlyCloneSprite) {
            newObj = new GameObject(gameObject.name);
            newObj.transform.position = transform.position.plusZ(-0.01f);
            newObj.transform.rotation = transform.rotation;
            var newSprite = newObj.AddComponent<SpriteRenderer>();
            newSprite.sprite = spriteRenderer.sprite;
            newSprite.color = spriteRenderer.color;
        } else {
            newObj = ((GameObject)Instantiate(gameObject, transform.position.plusZ(-0.01f), transform.rotation));
            newObj.GetComponent<GhostPulser>().enabled = false;
        }
        newObj.transform.parent = transform.parent;
        newObj.transform.localScale = transform.localScale;

        Tween.MoveBy(newObj, new Vector3(moveAmount.x, moveAmount.y, 0), duration, Interpolate.EaseType.EaseOutCirc);
        Tween.ScaleBy(newObj, new Vector3(scaleAmount.x, scaleAmount.y, 0), duration, Interpolate.EaseType.EaseOutCirc);
        Tween.ColorTo(newObj, spriteRenderer.color.withAlpha(0), duration, Interpolate.EaseType.Linear);
        yield return new WaitForSeconds(duration);

        Destroy(newObj);
    }
}
