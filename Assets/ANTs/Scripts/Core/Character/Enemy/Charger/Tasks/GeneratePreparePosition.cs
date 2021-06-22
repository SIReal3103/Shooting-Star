using ANTs.Template;
using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class GeneratePreparePosition : Action
{
    [SerializeField] SharedTransform prepareZone_ANTsPolygon;
    [SerializeField] SharedVector2 preparePosition;

    private ANTsPolygon prepareZone;

    public override void OnAwake()
    {
        prepareZone = prepareZone_ANTsPolygon.Value.GetComponent<ANTsPolygon>();
    }

    public override void OnStart()
    {
        preparePosition.SetValue(prepareZone.GetRandomPointOnSurface());
    }

	public override TaskStatus OnUpdate()
	{
		return TaskStatus.Success;
	}
}