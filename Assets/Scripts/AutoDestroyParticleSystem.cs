using UnityEngine;
using System.Collections;

public class AutoDestroyParticleSystem : MonoBehaviour {
    ParticleSystem theParticleSystem;

    public void Start() {
        theParticleSystem = theParticleSystem ?? GetComponent<ParticleSystem>();
    }

    public void Update() {
        if (theParticleSystem && !theParticleSystem.IsAlive())
            Destroy(gameObject);
    }
}