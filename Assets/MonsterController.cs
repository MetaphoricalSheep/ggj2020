using System.Collections.Generic;
using UnityEngine;

public class MonsterController : MonoBehaviour {
    [SerializeField] public static float disableDistance = 15f;
    [SerializeField] public static float destroyDistance = 7f;
    [SerializeField] public static float moveDistance = 10f;

    private static List<GameObject> monsters = new List<GameObject>();
    private static GameObject player;

    public static List<GameObject> GetMonsters() => monsters;

    public static void AddMonster(GameObject m) {
        monsters.Add(m);
    }

    public static void RemoveMonster(GameObject m) {
        monsters.Remove(m);
    }

    private void Start() {
        player = GameObject.FindWithTag("Player");
    }

    private void Update() {
        DeactivateMonsters();
        MoveMonstersAwayFromPlayer();
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
            Vector3 distVec = m.transform.position - player.transform.position;
            float currentDistance = Vector3.Magnitude(distVec);
            if (currentDistance < moveDistance) {
                Vector3 moveVec = (moveDistance - currentDistance) * distVec.normalized;
                m.transform.position += moveVec;
                CheckMonsterDestory(m);
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
