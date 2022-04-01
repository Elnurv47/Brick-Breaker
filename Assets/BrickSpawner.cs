using UnityEngine;
using System.Collections.Generic;

public class BrickSpawner : MonoBehaviour
{
    [SerializeField] private IGrid _grid;
    [SerializeField] private GameObject _brickPrefab;

    private void SpawnBrickRow()
    {
        List<GridObject> gridTopColumn = _grid.GetColumnAt(colIndex: _grid.Height - 1);
    }
}
