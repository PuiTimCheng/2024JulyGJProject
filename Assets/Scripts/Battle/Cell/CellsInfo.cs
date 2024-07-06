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

    [Button]
    public void Generate()
    {
        Cells = new T[Width, Height];
    }

    public Vector2 Pivot => new Vector2(Width / 2, this.Width * (Height / 2));

    public CellsInfo(int width, int height, Func<Vector2Int, T> source)
    {
        Width = width;
        Height = height;
        Cells = new T[width, height];
        for (int w = 0; w < width; w++)
        {
            for (int h = 0; h < height; h++)
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

    // public CellsInfo<T> Rotate()
    // {
    //     List<T> cells = new List<T>(this.Cells.Count);
    //     for (int index1 = this.Width - 1; index1 >= 0; --index1)
    //     {
    //         for (int index2 = 0; index2 < this.Height; ++index2)
    //         {
    //             int index3 = index2 * this.Width + index1;
    //             cells.Add(this.Cells[index3]);
    //         }
    //     }
    //
    //     return new CellsInfo<T>(this.Height, this.Width, cells);
    // }

    // public bool TryGetCellWithOffset(int index, int wOffset, int hOffset, out T cell)
    // {
    //     int num1 = this.GetCoordinateW(index) + wOffset;
    //     int num2 = this.GetCoordinateH(index) + hOffset;
    //     if (num1 < 0 || num1 >= this.Width || num2 < 0 || num2 >= this.Height)
    //     {
    //         cell = default(T);
    //         return false;
    //     }
    //
    //     cell = this.GetItem(num1 + num2 * this.Width);
    //     return true;
    // }

    // public CellsInfo<TR> ConvertTo<TR>(Func<T, TR> converter)
    // {
    //     List<TR> cells = new List<TR>(this.Cells.Count);
    //     foreach (T cell in this.Cells)
    //         cells.Add(converter(cell));
    //     return new CellsInfo<TR>(this.Width, this.Height, cells);
    // }
}