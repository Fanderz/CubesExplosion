using UnityEngine;
using UnityEngine.UI;

public class Cube : MonoBehaviour
{
    [SerializeField] private Cube _prefab;
    [SerializeField] private Ray _ray;
    [SerializeField] private int _minCubesCount = 2;
    [SerializeField] private int _maxCubesCount = 7;

    private Camera _camera;
    private int _scaleDivider = 2;
    private int _currentSeparationChance = 100;
    private float _explosionForce = 100;

    private System.Random _random = new System.Random();

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
                SpawnObjects(hittedObject);
            else if (_random.Next(maxSeparationChance / _currentSeparationChance) == 1)
                SpawnObjects(hittedObject); 
        }
    }

    private void SpawnObjects(GameObject hittedObject)
    {
        int cubesCount = _random.Next(_minCubesCount, _maxCubesCount);
        Vector3 currentScale = hittedObject.transform.localScale;

        for (int i = 0; i < cubesCount; i++)
        {
            _prefab.transform.position = hittedObject.transform.position;
            var newCube = Instantiate(_prefab);

            newCube.gameObject.transform.localScale = currentScale / _scaleDivider;
            currentScale = newCube.gameObject.transform.localScale;

            newCube._currentSeparationChance = hittedObject.GetComponent<Cube>()._currentSeparationChance / _scaleDivider;
            newCube.gameObject.GetComponent<Renderer>().material.color = Random.ColorHSV();
            newCube.GetComponent<Rigidbody>().AddExplosionForce(_explosionForce, hittedObject.GetComponent<Renderer>().bounds.center, 100);
        }
    }
}
