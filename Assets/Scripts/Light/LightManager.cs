using System.Collections.Generic;
using UnityEngine;

public class LightManager : MonoBehaviour {
    private static List<Light> lights = new List<Light>();
    private static GameObject player;
    private static CharacterHealth characterHealth;
    public static float disableDistance = 15f;

    public static List<Light> GetLights() => lights;

    public static void AddLight(Light light) {
        lights.Add(light);
    }

    public static void RemoveLight(Light light) {
        lights.Remove(light);
    }

    private void Start() {
        player = GameObject.FindWithTag("Player");
        characterHealth = player.GetComponent<CharacterHealth>();
    }

    private void Update() {
        DeactivateLights();
        characterHealth.isSafe = PlayerIsSafe();
    }

    private static void DeactivateLights() {
        foreach (var light in lights) {
            float distance = Vector3.Distance(light.transform.position, player.transform.position);
            light.enabled = distance < light.range + disableDistance;
        }
    }

    private static bool PlayerIsSafe() {

        foreach (var light in lights) {
            float distance = Vector3.Distance(light.transform.position, player.transform.position);


            if (light.enabled && distance < light.range) {
                return true;
            }
        }

        return false;
    }
}
