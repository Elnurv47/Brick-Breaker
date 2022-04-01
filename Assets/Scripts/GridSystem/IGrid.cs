using System.Collections.Generic;
using UnityEngine;

public interface IGrid
{
    int Width { get; }
    int Height { get; }
    Vector3 GetGridObjectPosition(GridIndex point);
    Vector3 GetGridObjectCenter(GridIndex point);
    GridObject GetGridObject(Vector3 cellWorldPosition);
    List<GridObject> GetColumnAt(int colIndex);
    void SetGridObject(Vector3 cellWorldPosition, GridObject gridObject);
    void UpdateVisual();
    TextMesh[,] GetDebugArray();
}
