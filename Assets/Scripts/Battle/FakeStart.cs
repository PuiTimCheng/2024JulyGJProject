using System;
using Sirenix.OdinInspector;
using UnityEngine;

public class FakeStart : SerializedMonoBehaviour
{
    public CellsInfo<bool> FakeCellsInfo;
    
    void Start()
    {
        StomachManager.Instance.GenerateCells(FakeCellsInfo);
    }
}