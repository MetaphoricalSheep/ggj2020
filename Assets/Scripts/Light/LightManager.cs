using System.Collections.Generic;
using UnityEngine;

public class LightManager : MonoBehaviour
{
    private static List<Light> lights = new List<Light>();
    private static GameObject player;
    private static CharacterHealth characterHealth;
    
    public static List<Light> GetLights() => lights;

    public static void AddLight(Light light)
    {
        lights.Add(light);
    }

    public static void RemoveLight(Light light)
    {
        lights.Remove(light);
    }

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
        characterHealth = player.GetComponent<CharacterHealth>();
    }

    private void Update()
    {
        characterHealth.isSafe = PlayerIsSafe();
    }

    private bool PlayerIsSafe() {
        print(player.transform.position);
        foreach (Light light in lights) {
            print(Vector3.Distance(light.transform.position, player.transform.position));
            if (Vector3.Distance(light.transform.position, player.transform.position) < light.range) {
                print("player in range");
                return true;
            }
        }
        return false;
    }
}
