using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private List<Cube> _defaultCubes;
    [SerializeField] private Cube _prefab;
    [SerializeField] private int _minCubesCount = 2;
    [SerializeField] private int _maxCubesCount = 7;

    private int _scaleDivider = 2;

    private System.Random _random = new System.Random();

    private void Start()
    {
        foreach (var cube in _defaultCubes)
        {
            cube.OnHitObject += SpawnObjects;
        }
    }

    private void SpawnObjects(GameObject hittedObject)
    {
        hittedObject.GetComponent<Cube>().OnHitObject -= SpawnObjects;

        int cubesCount = _random.Next(_minCubesCount, _maxCubesCount);
        Vector3 currentScale = hittedObject.transform.localScale;

        for (int i = 0; i < cubesCount; i++)
        {
            _prefab.transform.position = hittedObject.transform.position;
            var newCube = Instantiate(_prefab, hittedObject.transform.parent);

            newCube.gameObject.transform.localScale = currentScale / _scaleDivider;
            currentScale = newCube.gameObject.transform.localScale;

            newCube.UpdateSeparationChance(hittedObject.GetComponent<Cube>().CurrentSeparationChance / _scaleDivider);
            newCube.SetColor(Random.ColorHSV());
            newCube.AddForce(hittedObject.GetComponent<Renderer>().bounds.center);
            newCube.OnHitObject += SpawnObjects;
        }


    }
}
