using System;
using UnityEngine;
using System.Collections.Generic;

using Random = UnityEngine.Random;

public class BrickSpawner : MonoBehaviour
{
    [SerializeField] private BrickGrid _grid;
    [SerializeField] private Brick _brickPrefab;

    public event Action<Brick> OnBrickSpawned;

    public void SpawnBrickRow()
    {
        List<GridObject> gridTopColumn = _grid.GetRowAt(rowIndex: _grid.Height - 1);

        foreach (GridObject gridObject in gridTopColumn)
        {
            int random = Random.Range(0, 2);

            if (random == 1)
            {
                Brick spawnedBrick = Instantiate(_brickPrefab, _grid.GetGridObjectCenter(gridObject.GridIndex), Quaternion.identity);
                OnBrickSpawned?.Invoke(spawnedBrick);
            }
        }
    }
}
