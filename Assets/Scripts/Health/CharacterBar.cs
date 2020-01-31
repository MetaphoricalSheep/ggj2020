using UnityEngine;

public class CharacterBar : MonoBehaviour {
    private SpriteRenderer spriteRenderer;

    public float maxAmount = 100f;
    public enum DrawPosition { abovePlayer, globallyTopLeft, globallyTopRight,
        dontDraw, globallyBottomCenter };

    // Public
    public Texture2D textureBack; // back segment
    public Texture2D textureCurrent; // front segment
    public Texture2D texturePrevious; // draining segment
    public DrawPosition drawPosition;
    public float positionXPercent;
    public float positionYPercent;
    public float barWidthPercent;
    public float barHeightPercent;
    public float borderWidth;
    public bool drawPlusChange;

    // Settings visual
    protected readonly float drawLastChangeTime = 4f;
    protected float barWidth;
    protected float barHeight;
    protected float positionX;
    protected float positionY;

    // Settings logic
    protected readonly float visualDrainSpeed = 1.0f;

    // Logic
    protected float currentAmount;
    protected float previousAmount;
    protected float lastChangeTime = -10f;
    protected float lastRemoveTime = -10f;
    protected float lastAddTime = -10f;


    public virtual void Awake() {
        currentAmount = maxAmount;
        previousAmount = maxAmount;
    }

    public virtual void Start() {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public virtual void Add(float amount) {
        float amountBefore = currentAmount;
        currentAmount += Mathf.Abs(amount);

        if (currentAmount > maxAmount) {
            currentAmount = maxAmount;
        }

        if (!Mathf.Approximately(amountBefore, currentAmount)) {
            lastChangeTime = Time.time;
            lastAddTime = Time.time;
        }
    }

    public virtual void Remove(float amount) {
        float amountBefore = currentAmount;
        currentAmount -= Mathf.Abs(amount);

        if (currentAmount < 0) {
            currentAmount = 0;
        }

        if (!Mathf.Approximately(amountBefore, currentAmount)) {
            lastChangeTime = Time.time;
            lastRemoveTime = Time.time;
        }
    }

    protected virtual void Update() {
        // Change bar over time
        if (previousAmount > currentAmount) {
            previousAmount -= (maxAmount / visualDrainSpeed) * Time.deltaTime;

            if (previousAmount < currentAmount) {
                previousAmount = currentAmount;
            }
        } else if (previousAmount < currentAmount) {
            previousAmount += (maxAmount / visualDrainSpeed) * Time.deltaTime;

            if (previousAmount > currentAmount) {
                previousAmount = currentAmount;
            }
        }
    }

    public virtual bool HasMoreThan(float amount) {
        return currentAmount >= amount;
    }

    void OnGUI() {
        barHeight = Screen.height * barHeightPercent;
        barWidth = Screen.width * barWidthPercent;
        positionX = Screen.width * positionXPercent;
        positionY = Screen.height * positionYPercent;

        float previousLength = Mathf.Max((previousAmount * barWidth) / maxAmount, 0.0f);
        float currentLength = Mathf.Max((currentAmount * barWidth) / maxAmount, 0.0f);

        Rect back = Rect.zero;
        Rect previous = Rect.zero;
        Rect current = Rect.zero;
        switch (drawPosition) {
            case DrawPosition.abovePlayer:
                if (lastChangeTime + drawLastChangeTime < Time.time) {
                    break;
                }

                Vector2 playerPosition = transform.position;
                playerPosition.y += positionYPercent;

                Vector2 currPosition = Camera.main.WorldToScreenPoint(playerPosition);
                currPosition.y = Screen.height - currPosition.y;
                currPosition.x -= (borderWidth + barWidth) * 0.5f;

                back = new Rect(currPosition.x - borderWidth, currPosition.y - borderWidth, barWidth + (borderWidth * 2), barHeight + (borderWidth * 2));
                previous = new Rect(currPosition.x + Mathf.Min(currentLength, previousLength), currPosition.y, Mathf.Abs(currentLength - previousLength), barHeight);
                current = new Rect(currPosition.x, currPosition.y, currentLength, barHeight);
                break;
            case DrawPosition.globallyTopLeft:
                back = new Rect(positionX - borderWidth, positionY - borderWidth, barWidth + (borderWidth * 2), barHeight + (borderWidth * 2));
                previous = new Rect(positionX + Mathf.Min(currentLength, previousLength), positionY, Mathf.Abs(currentLength - previousLength), barHeight);
                current = new Rect(positionX, positionY, currentLength, barHeight);
                break;
            case DrawPosition.globallyTopRight:
                back = new Rect(Screen.width - positionX - borderWidth - barWidth, positionY - borderWidth, barWidth + (borderWidth * 2), barHeight + (borderWidth * 2));
                previous = new Rect(Screen.width - positionX - barWidth + Mathf.Min(currentLength, previousLength), positionY, Mathf.Abs(currentLength - previousLength), barHeight);
                current = new Rect(Screen.width - positionX - barWidth, positionY, currentLength, barHeight);
                break;
            case DrawPosition.globallyBottomCenter:
                back = new Rect((Screen.width * 0.5f) - (barWidth * 0.5f) - borderWidth, Screen.height - (positionY + borderWidth + barHeight), barWidth + (borderWidth * 2), barHeight + (borderWidth * 2));
                previous = new Rect((Screen.width * 0.5f) - ((barWidth) * 0.5f) + Mathf.Min(currentLength, previousLength), Screen.height - (positionY + barHeight), Mathf.Abs(currentLength - previousLength), barHeight);
                current = new Rect((Screen.width * 0.5f) - ((barWidth) * 0.5f), Screen.height - (positionY + barHeight), currentLength, barHeight);
                break;
            default:
                return;
        }

        if (previousLength < currentLength && drawPlusChange) {
            print("draw plus");
            GUI.DrawTexture(back, textureBack);
            GUI.DrawTexture(current, textureCurrent);
            GUI.DrawTexture(previous, texturePrevious);
        } else {
            print("draw minus");
            GUI.DrawTexture(back, textureBack);
            GUI.DrawTexture(previous, texturePrevious);
            GUI.DrawTexture(current, textureCurrent);
        }
    }
}
