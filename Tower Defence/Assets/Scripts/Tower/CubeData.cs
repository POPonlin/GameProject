using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CubeData   //�ػ�����������
{
    [Header("��ǰ����")]
    public GameObject currentTower;
    [Header("���轨�컨��")]
    public float originCost;
    [Header("�����汾����")]
    public GameObject upTower;
    [Header("�������軨��")]
    public float upLevelCost;
}
