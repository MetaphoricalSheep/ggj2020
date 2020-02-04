using System.Collections.Generic;
using UnityEngine;

public class MonsterController : MonoBehaviour {
    [SerializeField] public static float disableDistance = 18f;
    [SerializeField] public static float destroyDistance = 8f;
    [SerializeField] public static float moveDistance = 12f;

    private static List<GameObject> monsters = new List<GameObject>();
    private static GameObject player;
    private static CustomCharacterController characterController;

    public static List<GameObject> GetMonsters() => monsters;

    public static void AddMonster(GameObject m) {
        monsters.Add(m);
    }

    public static void RemoveMonster(GameObject m) {
        monsters.Remove(m);
    }

    private void Start() {
        player = GameObject.FindWithTag("Player");
        characterController = player.GetComponent<CustomCharacterController>();
    }

    private void Update() {
        DeactivateMonsters();

        if (characterController.HoldingFire()) {
            MoveMonstersAwayFromPlayer();
        }
    }

    private static void DeactivateMonsters() {
        foreach (var m in monsters) {
            var mActive = Vector3.Distance(m.transform.position, player.transform.position) < disableDistance;

            if (mActive != m.activeSelf) {
                m.SetActive(mActive);
            }
        }
    }

    private static void MoveMonstersAwayFromPlayer() {
        foreach (GameObject m in monsters) {
            if (m.activeSelf) {
                Vector3 distVec = m.transform.position - player.transform.position;
                float currentDistance = Vector3.Magnitude(distVec);
                Vector3 moveVec = (moveDistance - currentDistance) * distVec.normalized;
                moveVec.y = 0.0f;

                if (moveDistance < currentDistance) {
                    moveVec = moveVec.normalized * Time.deltaTime * 3f;
                }

                if (moveVec.magnitude > 0.05f && moveVec.magnitude < 10f) {
                    m.transform.position += moveVec;
                    CheckMonsterDestory(m);
                }
            }
        }
    }

    public static void CheckMonstersDestroy() {
        foreach (GameObject m in monsters) {
            CheckMonsterDestory(m);
        }
    }

    public static void CheckMonsterDestory(GameObject m) {
        if (LightManager.GetClosestLightDistanceToPosition(m.transform.position) < destroyDistance) {
            Destroy(m);
        }

    }
}
