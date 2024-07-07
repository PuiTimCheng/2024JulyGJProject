using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UI;
using UnityEngine;

public class FloatScoreManager : MonoBehaviour
{
    public static FloatScoreManager Instance { get; private set; }
    
    public GameObject _floatScorePrefab;

    private List<FloatScore> _pool = new List<FloatScore>();

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayScore(Vector3 position, int score, bool add)
    {
        if (_pool.Any(_ => _.Available))
        {
            _pool.First(_ => _.Available).PlayScore(position, score, add);
        }
        else
        {
            var newFloat = Instantiate(_floatScorePrefab, transform);
            if (newFloat.TryGetComponent<FloatScore>(out var newScore))
            {
                _pool.Add(newScore);
                newScore.PlayScore(position, score, add);
            }
        }
    }
}
