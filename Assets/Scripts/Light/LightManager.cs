using System.Collections.Generic;
using UnityEngine;

public class LightManager : MonoBehaviour
{
    private static List<Light> lights = new List<Light>();
    private static GameObject player;
    private static CharacterHealth characterHealth;
    public static float disableDistance = 15f;

    public static List<Light> GetLights() => lights;

    public static void AddLight(Light light)
    {
        lights.Add(light);
        MonsterController.CheckMonstersDestroy();
    }

    public static void RemoveLight(Light light)
    {
        lights.Remove(light);
    }

    public static void AdjustIntensity(float intensity)
    {
        foreach (var light in lights)
        {
            light.intensity = intensity;
            light.range = intensity / 15;
        }
    }

    public static float GetClosestLightDistanceToPosition(Vector3 pos) {
        float minDist = -1f;

        foreach (Light l in lights) {
            float currDist = Vector3.Distance(pos, l.transform.position);
            if (minDist < 0.0f || currDist < minDist) {
                minDist = currDist;
            }
        }

        return minDist;
    }

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
        characterHealth = player.GetComponent<CharacterHealth>();
    }

    private void Update()
    {
        DeactivateLights();
        characterHealth.isSafe = PlayerIsSafe();
    }

    private static void DeactivateLights()
    {
        foreach (var light in lights)
        {
            var distance = Vector3.Distance(light.transform.position, player.transform.position);
            light.enabled = distance < light.range + disableDistance;
        }
    }
    
    private static bool PlayerIsSafe()
    {
        foreach (var light in lights)
        {
            var distance = Vector3.Distance(light.transform.position, player.transform.position);

            if (light.enabled && distance < light.range)
            {
                return true;
            }
        }

        return false;
    }
}
