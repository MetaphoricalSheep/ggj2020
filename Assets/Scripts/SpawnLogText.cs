using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnLogText : MonoBehaviour {
    public GameObject returnText;
    private bool isQuitting;

    void OnApplicationQuit() {
        isQuitting = true;
    }

    private void OnDestroy() {
        if (isQuitting) {
            return;
        }
        GameObject tt = Instantiate(returnText);
        tt.transform.position = this.transform.position;
        Destroy(tt, 5f);
    }
}
