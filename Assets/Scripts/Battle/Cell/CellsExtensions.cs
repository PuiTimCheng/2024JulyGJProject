using System.Collections.Generic;
using UnityEngine;

namespace Battle.Cell
{
    public static class CellsExtensions
    {
        public static bool CanPlaced(
            CellsInfo<bool> target, // Food
            CellsInfo<global::Cell> backGround, // Stomach
            Vector2Int placingCor, // The cor in Stomach
            out List<Vector2Int> eligibleCells)
        {
            bool result = true;

            eligibleCells = new List<Vector2Int>();

            for (var i = 0; i < target.Cells.Length; i++)
            {
                var pivotW = target.Width / 2;
                var pivotH = target.Height / 2;
                var wOffset = i % target.Width - pivotW;
                var hOffset = i / target.Width - pivotH;

                var foodCor = target.GetCoordinate(i);
                var newCor = placingCor + new Vector2Int(wOffset, hOffset);

                bool inBound = IsInBound(backGround, newCor);

                if (inBound)
                {
                    var foodCellIsValid = target.GetItem(i);
                    var cell = backGround.GetItem(newCor);

                    if (!foodCellIsValid)
                    {
                        continue;
                    }

                    eligibleCells.Add(newCor);

                    if (cell.CellState != CellState.Empty)
                    {
                        result = false;
                    }
                }
                else
                {
                    result = false;
                }
            }

            return result;
        }

        private static bool IsInBound<T>(this CellsInfo<T> cells, Vector2Int cor)
        {
            return cor.x >= 0 && cor.x < cells.Width && cor.y >= 0 && cor.y < cells.Height;
        }
    }
}