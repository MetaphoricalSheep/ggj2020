using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightManager: MonoBehaviour {
    private static List<Light> lights;
    private static GameObject player;

    public static void AddLight(Light light) {
        lights.Add(light);
    }

    public static List<Light> GetLights() {
        return lights;
    }

    private void Start() {
        player = GameObject.FindWithTag("Player");
    }

    private void Update() {
        if (!PlayerIsSafe()) {
            // player
        }
    }

    private bool PlayerIsSafe() {
        foreach (Light light in lights) {
            if (Vector3.Distance(light.transform.position, player.transform.position) < light.range) {
                return true;
            }
        }
        return false;
    }
}
