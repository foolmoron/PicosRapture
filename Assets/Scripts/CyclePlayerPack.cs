using UnityEngine;
using System.Collections;

using UnityEngine.UI;

public class CyclePlayerPack : MonoBehaviour {

    PlayerPacks packs;
    Text text;

    void Start() {
        packs = FindObjectOfType<PlayerPacks>();
        text = GetComponentInChildren<Text>();
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.R))
            Cycle();
    }

    public void Cycle() {
        packs.CyclePlayerPack();
        var pack = packs.Packs[packs.CurrentPackIndex];
        text.text = "Character: " + pack.name.ToUpper() + " (press here or R to cycle)";
    }
}