using UnityEngine;

public class RegisterMonster : MonoBehaviour {
    private void Start() {
        MonsterController.AddMonster(gameObject);
    }

    private void OnDestroy() {
        MonsterController.RemoveMonster(gameObject);
    }
}
