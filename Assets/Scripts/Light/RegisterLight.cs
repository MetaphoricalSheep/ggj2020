using UnityEngine;

public class RegisterLight : MonoBehaviour
{
    private Light _light;
    
    private void Start()
    {
        // LightManager lightManager= GameObject.FindGameObjectWithTag("LightManager").GetComponent<LightManager>();
        _light = GetComponent<Light>();

        if (_light != null)
        {
            LightManager.AddLight(_light);
        }
    }

    private void OnDestroy()
    {
        if (_light != null)
        {
            LightManager.RemoveLight(_light);
        }
    }
}
