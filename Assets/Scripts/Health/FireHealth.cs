using UnityEngine;

public class FireHealth : CharacterBar
{
    public float damagePerSecond;

    protected override void Update()
    {
        base.Update();
        base.Remove(Time.deltaTime * damagePerSecond);
        LightManager.AdjustIntensity(currentAmount);
    }
}
