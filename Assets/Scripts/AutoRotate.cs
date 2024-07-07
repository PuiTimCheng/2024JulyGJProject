using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class AutoRotate : MonoBehaviour
{
    public RectTransform rectTransform;
    public float speed;
    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    private void Update()
    {
        rectTransform.RotateAround(Vector3.forward, speed * Time.deltaTime);
    }
}
