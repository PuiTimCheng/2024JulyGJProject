using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using ImprovedTimers;
using TimToolBox.Extensions;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Random = System.Random;

/// <summary>
/// Spawn food based on seed and also move food along
/// </summary>
public class FoodConveyor : MonoBehaviour
{
    public Transform spawnedFoodParent;
    public RectTransform spawnRectTransform;
    public RectTransform despawnRectTransform;
    public RawImage conveyorRawImage;
    
    public int randomSeed;
    public float conveyorSpeed;
    public float uvRectSpeedMultiplier;
    public float foodSpeedMultiplier;
    [FormerlySerializedAs("spawnFoodPerSecond")] public float spawnFoodInterval;

    private bool _conveyorStarted;
    private Random _random;
    private CountdownTimer _spawnTimer = new CountdownTimer(1);
    public void Initiate(int randomSeed)
    {
        _conveyorStarted = true;
        this.randomSeed = randomSeed;
        _random = new Random(this.randomSeed);
        _spawnTimer = new CountdownTimer(spawnFoodInterval/ conveyorSpeed);
    }

    public void Update()
    {
        if (!_conveyorStarted) return;
        
        //update conveyor movement
        var uvRect = conveyorRawImage.uvRect;
        var newX = (uvRect.x + Time.deltaTime * conveyorSpeed * uvRectSpeedMultiplier) % 1f;
        conveyorRawImage.uvRect = new Rect(newX, uvRect.y, uvRect.width, uvRect.height);
        
        //spawn food
        if (_spawnTimer.IsFinished)
        {
            //TODO move this to a factory

            var keys = GameManager.Instance.FoodNameToConfigs.Keys.ToList();
            var index = _random.Next(0, keys.Count);
            var plate = Plate.BuildPlate(GameManager.Instance.FoodNameToConfigs[keys[index]]);
            
            var yOffset = Mathf.Lerp(0, spawnRectTransform.rect.height, (float)_random.NextDouble()) - spawnRectTransform.rect.height/2f;
            plate.transform.position = spawnRectTransform.position.Offset(y:yOffset);
            plate.transform.SetParent(spawnedFoodParent);
            
            _spawnTimer.Reset(spawnFoodInterval/ conveyorSpeed);    
            _spawnTimer.Start();    
        }
        
        //move all food
        foreach (var food in spawnedFoodParent.Children())
        {
            food.position = food.position.Offset(x: Time.deltaTime * -conveyorSpeed * foodSpeedMultiplier);
        }
        
        //despawn food
        var deleteList = new List<Transform>();
        foreach (var food in spawnedFoodParent.Children())
        {
            if (despawnRectTransform.rect.Contains(despawnRectTransform.InverseTransformPoint(food.position)))
            {
                deleteList.Add(food);
            }
        }
        for (int i = 0; i < deleteList.Count; i++)
        {
            Destroy(deleteList[i].gameObject);
        }
    }
    
    
}
