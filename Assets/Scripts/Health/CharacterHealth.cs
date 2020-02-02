using UnityEngine;

public class CharacterHealth : CharacterBar {
    public bool isSafe = true;
    public float damagePerSecond;

    protected override void Update() {
        SoundManager.Instance.ChangeHeartBeatVolume(0.6f - currentAmount / maxAmount);
        
        base.Update();
        
        if (isSafe) {
            base.Add(damagePerSecond * Time.deltaTime);
            
            return;
        }

        base.Remove(damagePerSecond * Time.deltaTime);

        if (currentAmount <= 0 && GameController.instance.gameState != GameState.GameOver)
        {
            GameController.instance.gameState = GameState.GameOver;
        }
    }
}
