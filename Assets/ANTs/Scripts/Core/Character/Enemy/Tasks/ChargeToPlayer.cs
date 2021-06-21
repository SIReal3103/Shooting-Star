using ANTs.Game;
using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class ChargeToPlayer : Action
{
    [SerializeField] SharedTransform player;

    private EnemyControlFacade control;

    private bool isArrive;

    public override void OnAwake()
    {
        control = GetComponent<EnemyControlFacade>();
    }

    public override void OnStart()
    {
        control.onMoverArrivedEvent += OnMoverArrived;
        Revaluate();
    }

    private void Revaluate()
    {
        isArrive = false;
        Vector2 playerPosition = (Vector2)player.Value.position;
        control.StartMovingTo(playerPosition);
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