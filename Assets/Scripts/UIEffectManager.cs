using Battle;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class UIEffectManager : MonoBehaviour
{
    public ParticleSystem BurstObject;
    public TMP_Text text;
    private Sequence _textSequence;

    public void Burst(Vector3 pos, FoodName foodName)
    {
        var effect = Instantiate(BurstObject, transform);
        effect.Play();
        effect.GetComponent<RectTransform>().anchoredPosition = pos;
        text.rectTransform.anchoredPosition = pos;

        ShowText(pos, foodName);
    }

    public void ShowText(Vector3 pos, FoodName foodName)
    {
        text.gameObject.SetActive(true);
        text.text = $"合成！{GetFoodLabel(foodName)}！";

        _textSequence = DOTween.Sequence();
        _textSequence.Append(text.transform.DOScale(1.2f, 0.3f)) // 在0.1秒内放大到1.2倍
            .Append(text.transform.DOScale(1f, 0.1f)) // 然后在0.1秒内缩回到1倍
            .AppendInterval(1f) // 等待1秒
            .OnComplete(() => text.gameObject.SetActive(false));
    }
    public static string GetFoodLabel(FoodName food)
    {
        switch (food)
        {
            case FoodName.RiceAndBeef:
                return "牛肉饭";
            case FoodName.RiceAndEgg:
                return "蛋炒饭";
            case FoodName.ShrimpAndBiscuit:
                return "虾饺";
            case FoodName.WatermelonAndRice:
                return "西瓜拌饭";
            case FoodName.EggAndBiscuit:
                return "蛋饺";
            case FoodName.SausageAndBread:
                return "热狗";
            case FoodName.ShrimpAndNoodle:
                return "海鲜面";
            case FoodName.BeefAndBread:
                return "牛角面包";
            case FoodName.TomatoAndFish:
                return "番茄鱼";
            case FoodName.BreadAndChicken:
                return "三明治";
            case FoodName.ChickenAndBiscuit:
                return "饺子";
            default:
                return food.ToString();
        }
    }
}