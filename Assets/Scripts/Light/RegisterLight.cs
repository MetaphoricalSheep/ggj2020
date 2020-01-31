using UnityEngine;

public class RegisterLight : MonoBehaviour {
    // Start is called before the first frame update
    void Start() {
        // LightManager lightManager= GameObject.FindGameObjectWithTag("LightManager").GetComponent<LightManager>();
        Light light = GetComponent<Light>();
        if (light != null) {
            LightManager.AddLight(light);
        }
    }

    // Update is called once per frame
    void Update() {

    }
}
