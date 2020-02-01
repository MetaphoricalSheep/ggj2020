using UnityEngine;

public enum Holdable {Nothing, Wood, Torch}

public class CharacterHands : MonoBehaviour
{
    [SerializeField] GameObject holdingWood;
    [SerializeField] GameObject holdingTorch;
    
    Holdable _currentlyHolding = Holdable.Nothing;
    public Holdable currentlyHolding => _currentlyHolding;

    FireController _fireController;
    private SoundManager _soundManager;

    private void Start()
    {
        GameObject fireObject = GameObject.FindWithTag("Fire");
        
        if (fireObject == null)
        {
            Debug.LogError("Please put a fire in the scene :3");
            return;
        }
        
        _fireController = GameObject.FindWithTag("Fire").GetComponent<FireController>();
        _soundManager = SoundManager.Instance;
    }

    public void SetHolding(Holdable newHoldable)
    {
        var previousHoldable = _currentlyHolding;
        
        if (Holdable.Wood == newHoldable)
        {
            holdingTorch.SetActive(false);
            holdingWood.SetActive(true);
            _soundManager.PlayCollectLog();
            _currentlyHolding = newHoldable;
            
            return;
        }

        if (Holdable.Torch == newHoldable)
        {
            holdingWood.SetActive(false);
            holdingTorch.SetActive(true);
            _currentlyHolding = newHoldable;
            
            return;
        }

        if (previousHoldable == Holdable.Wood)
        {
            _soundManager.PlayDropLog();
        }

        if (previousHoldable == Holdable.Torch)
        {
            _soundManager.PlayPlaceTorch();
        }
        
        holdingTorch.SetActive(false);
        holdingWood.SetActive(false);
        _currentlyHolding = newHoldable;
    }

    public void AddWoodToFire()
    {
        Debug.Log($"AddWoodToFire");
        SetHolding(Holdable.Nothing);
        _fireController.AddWood();
    }

    public float PickTorch()
    {
        Debug.Log($"PickTorch");
        SetHolding(Holdable.Torch);
        _fireController.PickTorch();

        return _fireController.burnPowerPerWood;
    }
}
