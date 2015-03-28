using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class WeaponRoot : MonoBehaviour {

    [Range(0, 360)]
    public float Rotation;
    public Transform Weapon;
    int previousSign = 1;

    public Vector2 kickback;
    [Range(0, 1)]
    public float kickbackStrength = 0.1f;
    [Range(0, 0.5f)]
    public float kickbackFade = 0.1f;
    Vector2 previousKickback;

    void Update() {
        Rotation = Rotation.rotationNormalizedDeg();
        // calculate real rotation and flipping of weapon sprite
        {
            var actualRotation = Rotation;
            if (Rotation <= 90 || Rotation >= 270) { // in right half of circle
                Weapon.localPosition = Weapon.localPosition.timesX(previousSign * 1);
                Weapon.localScale = Weapon.localScale.timesX(previousSign * 1);
                previousSign = 1;
            } else { // in left half, reflect scale and offset
                Weapon.localPosition = Weapon.localPosition.timesX(previousSign * -1);
                Weapon.localScale = Weapon.localScale.timesX(previousSign * -1);
                previousSign = -1;
                actualRotation -= 180;
            }
            transform.rotation = Quaternion.Euler(0, 0, actualRotation);
        }
        // add kickback offset
        {
            transform.position = transform.position - (Vector3) previousKickback + (Vector3) kickback;
            previousKickback = kickback;
            kickback = Vector2.Lerp(kickback, Vector2.zero, kickbackFade);
        }
    }
}