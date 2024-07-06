using Battle;
using UnityEngine;
using UnityEngine.UI;

public class Plate : MonoBehaviour
{
    [SerializeField] Food _food;
    [SerializeField] CellPresenter _cellPresenter;
    [SerializeField] Image _plate;
    
    public void Init(FoodData data)
    {
        _food.InitFood(data);
        _cellPresenter.GenerateCell(data);
        _plate.sprite = data.plateSprite;
    }

    public static Plate BuildPlate(FoodData foodData)
    {
        var plate = Instantiate(GameManager.Instance.platePrefab).GetComponent<Plate>();
        plate.Init(foodData);
        return plate;
    }

    public void ClearCellPresenter()
    {
        _cellPresenter.gameObject.SetActive(false);
    }
}
