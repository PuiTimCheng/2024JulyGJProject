using UnityEngine;
using UnityEngine.UI;

namespace Battle
{
    public class CellPresenter : MonoBehaviour
    {
        [SerializeField] GridLayoutGroup _group;
        [SerializeField] GameObject _prefab;
        
        public void GenerateCell(FoodData data)
        {
            // Delete all children of GridLayoutGroup
            for (int i = 0; i < _group.transform.childCount; i++)
            {
                Destroy(_group.transform.GetChild(i).gameObject);
            }

            _group.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
            _group.constraintCount = data.GridConfig.Width;

            for (int i = 0; i < data.GridConfig.Count; i++)
            {
                var newCell = Instantiate(_prefab, _group.transform).GetComponent<Cell>();
                Debug.Log($"{data.name}, {i} {data.GridConfig.GetItem(i)}");
                newCell.SetCellState(data.GridConfig.GetItem(i)? CellState.Empty : CellState.Inactive);
            }
        }
    }
}