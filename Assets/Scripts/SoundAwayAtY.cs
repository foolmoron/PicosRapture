using UnityEngine;
using System.Collections;

public class SoundAwayAtY : MonoBehaviour {

    public Rigidbody2D Target;
    public float SoundAwayY;
    [Range(0.001f, 300)]
    public float SoundAwayPeriod = 100;
    public float SoundAwayYVelocity;
    [Range(0.001f, 300)]
    public float SoundAwayVelocityPeriod = 100;
    AudioSource source;

    void Start() {
        source = GetComponent<AudioSource>();
    }

    void Update() {
        var currentY = Target.position.y;
        var normalizedAmountAboveSoundAwayPoint = (currentY - SoundAwayY) / SoundAwayPeriod;
        var currentYVelocity = Target.velocity.y;
        var normalizedVelocityValue = 1 - Mathf.Clamp01((-currentYVelocity + SoundAwayYVelocity) / SoundAwayVelocityPeriod);
        Debug.Log(normalizedVelocityValue);
        source.volume = 1 - Mathf.Clamp01(Mathf.Min(normalizedAmountAboveSoundAwayPoint, normalizedVelocityValue));
    }
}