using ANTs.Game;
using ANTs.Template;
using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEditor;

public class MoveToPrepareZone : Action
{    
    [SerializeField] SharedVector2 preparePosition;

    private EnemyControlFacade control;
    private bool isArrive;

    public override void OnAwake()
    {
        control = GetComponent<EnemyControlFacade>();
    }

    public override void OnStart()
	{
        control.onMoverArrivedEvent += OnMoverArrived;

        isArrive = false;
        Debug.Log(preparePosition.Value);
        control.StartMovingTo(preparePosition.Value);
	}

    public override void OnEnd()
    {
        control.onMoverArrivedEvent -= OnMoverArrived;
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