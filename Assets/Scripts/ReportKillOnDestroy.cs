using UnityEngine;
using System.Collections;

public class ReportKillOnDestroy : MonoBehaviour {

    AngelBoost angelBoost;

    void Start() {
        angelBoost = FindObjectOfType<AngelBoost>();
    }

    void OnDestroy() {
        angelBoost.ReportAngelKilled();
    }
}
