using System;
using UnityEngine;

public class Cube : MonoBehaviour
{

    [SerializeField] private int _currentSeparationChance = 100;
    [SerializeField] private Renderer _renderer;
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField]private float _explosionForce = 50f;
    [SerializeField]private float _explosionRadius = 500f;

    private Camera _camera;
    private Ray _clickRay;
    private System.Random _random = new System.Random();

    public event Action<Cube> OnHitObject;

    public int CurrentSeparationChance => _currentSeparationChance;

    private void Start()
    {
        _camera = Camera.main;
    }

    private void OnMouseDown()
    {
        int maxSeparationChance = 100;

        _clickRay = _camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(_clickRay, out hit, Mathf.Infinity))
        {
            var hittedObject = hit.transform.GetComponent<Cube>();

            Destroy(hittedObject.gameObject, Time.deltaTime);

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
