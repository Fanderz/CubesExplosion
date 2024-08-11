using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private List<Cube> _defaultCubes;
    [SerializeField] private int _minCubesCount = 2;
    [SerializeField] private int _maxCubesCount = 6;

    private int _scaleDivider = 2;

    private void Start()
    {
        foreach (var cube in _defaultCubes)
            cube.OnHitObject += SpawnObjects;
    }

    private void SpawnObjects(Cube hittedObject)
    {
        hittedObject.OnHitObject -= SpawnObjects;

        int cubesCount = Random.Range(_minCubesCount, _maxCubesCount + 1);
        Vector3 currentScale = hittedObject.transform.localScale;

        for (int i = 0; i < cubesCount; i++)
        {
            hittedObject.transform.position = hittedObject.transform.position;
            var newCube = Instantiate(hittedObject, hittedObject.transform.parent);

            newCube.transform.localScale = currentScale / _scaleDivider;

            newCube.UpdateSeparationChance(hittedObject.CurrentSeparationChance / _scaleDivider);
            newCube.SetColor(Random.ColorHSV());
            newCube.OnHitObject += SpawnObjects;
        }
    }
}
