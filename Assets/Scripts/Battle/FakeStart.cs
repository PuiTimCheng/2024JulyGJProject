using System;
using UnityEngine;

public class FakeStart : MonoBehaviour
{
    void Start()
    {
        StomachManager.Instance.GenerateCells(new CellsInfo<bool>(5, 5, _ => true));
    }
}