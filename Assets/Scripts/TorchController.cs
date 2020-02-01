using System;
using UnityEngine;

public class TorchController : MonoBehaviour, IPickable, IBurnable, IPlaceable
{
    [SerializeField] private float _dps;
    
    private GameObject _gameObject;
    private Transform _transform;
    
    private float _torchFuel;

    public float GetBurningPower() => _torchFuel;
    
    public static TorchController Craft(Transform torch, Transform characterTransform, float fuel)
    {
        var transform = Instantiate(torch, characterTransform.position, Quaternion.identity, characterTransform);
        var controller = transform.GetComponent<TorchController>();
        controller._torchFuel = fuel;
        transform.gameObject.SetActive(false);

        return controller;
    }

    public void Place(Vector3 position)
    {
        // TODO: torch needs to burn while being carried
        _gameObject.SetActive(true);
        _transform.position = position;
    }

    public void Pick()
    {
        //_gameObject.SetActive(false);
    }

    private void Awake()
    {
        _gameObject = gameObject;
        _transform = transform;
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
