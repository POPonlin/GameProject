using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CubeData   //地基方块数据类
{
    [Header("当前建筑")]
    public GameObject currentTower;
    [Header("所需建造花费")]
    public float originCost;
    [Header("升级版本建筑")]
    public GameObject upTower;
    [Header("升级所需花费")]
    public float upLevelCost;
}
