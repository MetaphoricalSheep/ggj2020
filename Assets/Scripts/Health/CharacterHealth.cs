using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterHealth : CharacterBar {
    public bool isSafe = true;
    public float damagePerSecond = 0.0f;

    protected override void Update() {
        base.Update();
        if (isSafe) {
            base.Add(maxAmount);
        } else {
            base.Remove(damagePerSecond * Time.deltaTime);
        }
    }
}
