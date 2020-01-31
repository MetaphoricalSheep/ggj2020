using UnityEngine;

public class FireHealth : CharacterBar {
    public float damagePerSecond;

    public override void Awake() {
        base.Awake();
    }

    protected override void Update() {
        base.Update();
        base.Add(Time.deltaTime * damagePerSecond);
    }
}