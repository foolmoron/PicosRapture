using System;

using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour {

    public event Action OnGameOver = delegate { };

    public Collider2D Target;
    public bool CanGameOver;
    new Collider2D collider;

    void Start() {
        collider = GetComponent<Collider2D>();
    }
    
    void Update() {
        if (CanGameOver) {
            if (collider.IsTouching(Target)) {
                OnGameOver();
                CanGameOver = false;
            }
        } else {
            if (!collider.IsTouching(Target)) {
                CanGameOver = true;
            }
        }
    }
}
