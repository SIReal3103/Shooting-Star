using ANTs.Template;
using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class GeneratePreparePosition : Action
{
    [SerializeField] ANTsPolygon prepareZone;
    [SerializeField] SharedVector2 preparePosition;

    public override void OnStart()
    {
        preparePosition.SetValue(prepareZone.GetRandomPointOnSurface());
    }

	public override TaskStatus OnUpdate()
	{
		return TaskStatus.Success;
	}
}