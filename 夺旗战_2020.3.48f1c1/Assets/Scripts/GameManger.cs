using BehaviorDesigner.Runtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManger : MonoBehaviour
{
    private static GameManger instance;
    public static GameManger Instance
    {
        get
        {
            return instance;
        }
    }

    private List<BehaviorTree> TakenFlag = new List<BehaviorTree>();
    private List<BehaviorTree> NotTakenFlag = new List<BehaviorTree>();


    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        BehaviorTree[] bts = FindObjectsOfType<BehaviorTree>();        
        foreach (BehaviorTree bt in bts) 
        {
            if (bt.Group == 1)
            {
                NotTakenFlag.Add(bt);
            }
            else
            {
                TakenFlag.Add(bt);
            }
        }
    }

    public void TakeFlag()
    {
        foreach (BehaviorTree bt in TakenFlag)
        {
            if (BehaviorManager.instance.IsBehaviorEnabled(bt) == false)
            {
                bt.enabled = true;
            }
        }

        foreach (BehaviorTree bt in NotTakenFlag)
        {
            if (BehaviorManager.instance.IsBehaviorEnabled(bt))
            {
                bt.enabled = false;
            }
        }
    }

    public void DropFlag()
    {
        foreach (BehaviorTree bt in TakenFlag)
        {
            if (BehaviorManager.instance.IsBehaviorEnabled(bt))
            {
                bt.enabled = false;
            }
        }

        foreach (BehaviorTree bt in NotTakenFlag)
        {
            if (BehaviorManager.instance.IsBehaviorEnabled(bt) == false)
            {
                bt.enabled = true;
            }
        }
    }


}
