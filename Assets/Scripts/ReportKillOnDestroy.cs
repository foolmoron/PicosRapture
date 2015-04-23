using UnityEngine;
using System.Collections;

public class ReportKillOnDestroy : MonoBehaviour {

    public AngelBoost AngelBoost;
    
    void OnDestroy() {
        if (AngelBoost && AngelBoost.gameObject.activeSelf) {
            AngelBoost.ReportAngelKilled();
        }
    }
}
