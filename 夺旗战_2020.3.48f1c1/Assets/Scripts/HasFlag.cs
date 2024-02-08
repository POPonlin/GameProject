using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class HasFlag : Conditional
{
    private Offense flag;
    public override void OnAwake()
    {
        flag = GetComponent<Offense>();
    }

    public override TaskStatus OnUpdate()
    {
        if(flag.haveFlag)
        {
            return TaskStatus.Success;
        }
        return TaskStatus.Failure;
    }
}