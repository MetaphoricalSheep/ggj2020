using System;
using UnityEngine;

public class TorchController : MonoBehaviour, IPickable, IBurnable, IPlaceable
{
    [SerializeField] private float _dps;
    [SerializeField] private GameObject _model;
    [SerializeField] private GameObject _smallParticle;
    [SerializeField] private GameObject _fireParticle;
    
    private GameObject _gameObject;
    private Transform _transform;
    
    private float _torchFuel;

    public float GetBurningPower() => _torchFuel;
    
    public static TorchController Craft(Transform torch, Transform characterTransform, float fuel)
    {
        var forward = characterTransform.forward;
        var right = characterTransform.right;
        var position = characterTransform.position + forward + forward - right - right;
        var transform = Instantiate(torch, position, Quaternion.identity, characterTransform);
        var controller = transform.GetComponent<TorchController>();
        controller._torchFuel = fuel;
        controller.Hide();

        return controller;
    }

    public void Place(Vector3 position)
    {
        Show();
        _transform.position = position;
    }

    public void Pick()
    {
        //Hide();
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

    private void Show()
    {
        _model.SetActive(true);
        _fireParticle.SetActive(true);
        _smallParticle.SetActive(true);
    }

    private void Hide()
    {
        _model.SetActive(false);
        _fireParticle.SetActive(false);
        _smallParticle.SetActive(false);
    }
}
