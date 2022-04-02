using System.Collections.Generic;
using UnityEngine;

public interface IGrid
{
    int Width { get; }
    int Height { get; }
    Vector3 GetGridObjectPosition(GridIndex gridIndex);
    Vector3 GetGridObjectCenter(GridIndex gridIndex);
    GridObject GetGridObject(Vector3 cellWorldPosition);
    void SetGridObject(Vector3 cellWorldPosition, GridObject gridObject);
    void UpdateVisual();
    List<GridObject> GetRowAt(int colIndex);
    Vector3 GetGridObjectCenter(Vector3 cellWorldPosition);
    GridIndex GetGridIndex(Vector3 cellWorldPosition);
}
