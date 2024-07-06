using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

/// <summary>
/// This script is used for food to store its information
/// </summary>
/// <typeparam name="T"></typeparam>

public class CellsInfo<T>
{
    public int Width;
    public int Height;

    [TableMatrix(HorizontalTitle = "Cells", SquareCells = true)]
    public T[,] Cells;

    public int Count => Cells.Length;

    [Button]
    public void Generate()
    {
        Cells = new T[Width, Height];
    }

    public CellsInfo(int width, int height, Func<Vector2Int, T> source)
    {
        Width = width;
        Height = height;
        Cells = new T[width, height];
        for (int h = 0; h < height; h++)
        {
            for (int w = 0; w < width; w++)
            {
                Cells[w, h] = source(new Vector2Int(w, h));
            }
        }
    }

    public Vector2Int GetCoordinate(int index) => new Vector2Int(index % this.Width, index / this.Width);

    public T GetItem(Vector2Int cor)
    {
        return Cells[cor.x, cor.y];
    }

    public T GetItem(int index)
    {
        return GetItem(GetCoordinate(index));
    }

    public void SetItem(Vector2Int cor, T item) => this.Cells[cor.x, cor.y] = item;
}