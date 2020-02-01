using UnityEngine;

public class DestroyAfterN : MonoBehaviour {
    public float seconds;

    void Start() {
        Destroy(gameObject, seconds);
    }
}
