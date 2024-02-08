using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine.AI;

public class Defend : Action
{
	public SharedFloat defendSpeed;
	public SharedFloat defendAngularSpeed;

	public SharedFloat viewDistance;
	public SharedFloat FOV;

	public SharedGameObject target;
	private SharedTransform targetTra;
	public NavMeshAgent agent;

	private SharedFloat sqrDis;

    public override void OnAwake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    public override void OnStart()
	{
		targetTra = target.Value.transform;
		sqrDis = viewDistance.Value * viewDistance.Value;

		agent.enabled = true;
		//agent.speed = defendSpeed.Value;
		//agent.angularSpeed = defendAngularSpeed.Value;
		agent.destination = targetTra.Value.position;
	}



    public override TaskStatus OnUpdate()
	{
		if (targetTra is null && targetTra.Value is null)
		{
			return TaskStatus.Failure;
		}
		float tmp = (targetTra.Value.position - transform.position).sqrMagnitude;
		float angle = Vector3.Angle(transform.forward, targetTra.Value.position - transform.position);
		if(tmp <= sqrDis.Value && angle <= defendAngularSpeed.Value * 0.5f)
		{
			if (agent.destination != targetTra.Value.position)
			{
				agent.destination = targetTra.Value.position;
			}
			return TaskStatus.Running;
		}
		else
		{
			return TaskStatus.Success;
		}
	}
}