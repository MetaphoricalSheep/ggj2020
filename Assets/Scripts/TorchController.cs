using System;
using UnityEngine;

public class TorchController : MonoBehaviour, IPickable, IBurnable, IPlaceable
{
    [SerializeField] private float _torchCost = 10;
    [SerializeField] private CustomCharacterController  _character;
    [SerializeField] private FireHealth  _fire;
    [SerializeField] private float _dps = 1;
    
    private GameObject _gameObject;
    private Transform _transform;
    
    private float _torchFuel;

    // TODO: Subscribe to IBurnable
    public float GetBurningPower() => _torchFuel;
    
    public static TorchController CraftTorch(Transform torch, CustomCharacterController character)
    {
        var transform = Instantiate(torch, character.transform.position, Quaternion.identity);
        var controller = transform.GetComponent<TorchController>();
        
        // TODO: hide, but do not disable. or hide and set hide time
        transform.gameObject.SetActive(false);
        
        // TODO: reduce fire
        //_fire.Take(_torchCost);

        return controller;
    }

    public void Place(Vector2 position)
    {
        // TODO: torch needs to burn while being carried
        _gameObject.SetActive(true);
        _transform.position = position;
    }

    public void Pick()
    {
        _gameObject.SetActive(false);
        // TODO: Toggle torch on player
        // TODO: Keep track of fuel
        throw new NotImplementedException();
    }

    private void Awake()
    {
        _gameObject = gameObject;
        _transform = transform;
        _torchFuel = _torchCost;
    }

    private void Update()
    {
        _torchFuel -= Math.Abs(Time.deltaTime * _dps);

        if (_torchFuel <= 0)
        {
            Destroy(_gameObject);
        }
    }
}
