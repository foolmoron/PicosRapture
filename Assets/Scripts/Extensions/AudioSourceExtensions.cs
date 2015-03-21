using UnityEngine;
using System.Collections;

public static class AudioClipExtensions {

    public static void Play(this AudioClip audioClip) {
        audioClip.PlayClipAtPoint(Vector3.zero, 1.0f, 1.0f, 0.0f);
    }

    public static void PlayClipAtPoint(this AudioClip audioClip, Vector3 position) {
        audioClip.PlayClipAtPoint(position, 1.0f, 1.0f, 0.0f);
    }

    public static void PlayClipAtPoint(this AudioClip audioClip, Vector3 position, float volume) {
        audioClip.PlayClipAtPoint(position, volume, 1.0f, 0.0f);
    }

    public static void PlayClipAtPoint(this AudioClip audioClip, Vector3 position, float volume, float pitch, float pan) {
        float originalTimeScale = Time.timeScale;
        Time.timeScale = 1.0f;	// ensure that all audio plays

        GameObject go = new GameObject("One-shot audio");
        AudioSource goSource = go.AddComponent<AudioSource>();
        goSource.clip = audioClip;
        go.transform.position = position;
        goSource.volume = volume;
        goSource.pitch = pitch;
        goSource.panStereo = pan;

        goSource.Play();
        Object.Destroy(go, audioClip.length);

        Time.timeScale = originalTimeScale;
    }
}
