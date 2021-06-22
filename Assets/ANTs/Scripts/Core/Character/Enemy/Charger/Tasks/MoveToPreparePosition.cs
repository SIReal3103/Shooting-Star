using ANTs.Core;
using ANTs.Template;
using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEditor;

public class MoveToPreparePosition : Action
{    
    [SerializeField] SharedVector2 preparePosition;

    private EnemyChargerBehaviour charger;
    private bool isArrive;

    public override void OnAwake()
    {
        charger = GetComponent<EnemyChargerBehaviour>();
    }

    public override void OnStart()
	{
        charger.OnArrivedEvent += OnMoverArrived;

        isArrive = false;
        charger.MoveTo(preparePosition.Value);
	}
    public override void OnEnd()
    {
        charger.OnArrivedEvent -= OnMoverArrived;
    }

    public override TaskStatus OnUpdate()
	{
        return isArrive ? TaskStatus.Success : TaskStatus.Running;
	}

    public void OnMoverArrived()
    {
        isArrive = true;
    }
}