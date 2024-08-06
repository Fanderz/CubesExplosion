using System;
using UnityEngine;
using UnityEngine.UI;

public class Cube : MonoBehaviour
{
    [SerializeField] private Ray _ray;
    [SerializeField] private int _currentSeparationChance = 100;
    [SerializeField] private Renderer _renderer;
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField]private float _explosionForce = 100;
    [SerializeField]private float _explosionRadius = 100;

    private Camera _camera;
    private System.Random _random = new System.Random();

    public event Action<GameObject> OnHitObject;

    public int CurrentSeparationChance => _currentSeparationChance;

    private void Start()
    {
        _camera = Camera.main;
    }

    private void OnMouseDown()
    {
        int maxSeparationChance = 100;

        _ray = _camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(_ray, out hit, Mathf.Infinity))
        {
            var hittedObject = hit.transform.gameObject;

            Destroy(hittedObject, Time.deltaTime);

            if (_currentSeparationChance == maxSeparationChance)
                OnHitObject?.Invoke(hittedObject);
            else if (_random.Next(maxSeparationChance / _currentSeparationChance) == 1)
                OnHitObject?.Invoke(hittedObject);
        }
    }

    public void UpdateSeparationChance(int chance) =>
        _currentSeparationChance = chance;

    public void SetColor(Color color) =>
        _renderer.material.color = color;

    public void AddForce(Vector3 position) =>
        _rigidbody.AddExplosionForce(_explosionForce, position, _explosionRadius);
}
