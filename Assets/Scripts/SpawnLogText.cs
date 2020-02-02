using UnityEngine;

public class SpawnLogText : MonoBehaviour {
    public GameObject returnText;
    private bool isQuitting;

    void OnApplicationQuit() {
        isQuitting = true;
    }

    private void OnDestroy() {
        if (isQuitting || GameController.instance.gameState == GameState.GameOver) 
        {
            return;
        }
        
        var tt = Instantiate(returnText);
        tt.transform.position = transform.position;
        Destroy(tt, 5f);
    }
}
