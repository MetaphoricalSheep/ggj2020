using UnityEngine;
using UnityEngine.UI;

public class CharacterHealth : MonoBehaviour {
    public bool isSafe = true;
    public float damagePerSecond;
    public Image healthBar;

    public float currentAmount;
    public float maxAmount;

    private void Start()
    {
        currentAmount = maxAmount;
    }

    private void Update()
    {
        SoundManager.Instance.ChangeHeartBeatVolume(1f - currentAmount / maxAmount);

        if (isSafe)
        {
            Add(damagePerSecond * Time.deltaTime);
        }
        else
        {
            Remove(damagePerSecond * Time.deltaTime);
        }

        if (currentAmount <= 0 && GameController.instance.gameState != GameState.GameOver)
        {
            GameController.instance.gameState = GameState.GameOver;
        }

        if (healthBar != null)
        {
            healthBar.fillAmount = currentAmount / maxAmount;
        }
    }

    public virtual void Add(float amount)
    {
        float amountBefore = currentAmount;
        currentAmount += Mathf.Abs(amount);

        if (currentAmount > maxAmount)
        {
            currentAmount = maxAmount;
        }
    }

    public virtual void Remove(float amount)
    {
        float amountBefore = currentAmount;
        currentAmount -= Mathf.Abs(amount);

        if (currentAmount < 0)
        {
            currentAmount = 0;
        }
    }
}
