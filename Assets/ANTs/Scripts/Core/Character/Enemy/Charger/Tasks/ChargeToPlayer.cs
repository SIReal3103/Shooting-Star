using ANTs.Core;
using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class ChargeToPlayer : Action
{
    [SerializeField] SharedTransform player;

    private EnemyChargerBehaviour charger;

    private bool isArrive;

    public override void OnAwake()
    {
        charger = GetComponent<EnemyChargerBehaviour>();
    }
    private void Revaluate()
    {
        isArrive = false;
        Vector2 playerPosition = player.Value.position;
        charger.ChargeTo(playerPosition);
    }

    public override void OnStart()
    {
        charger.OnArrivedEvent += OnMoverArrived;

        Revaluate();
    }
    public override void OnEnd()
    {
        charger.OnArrivedEvent -= OnMoverArrived;
    }

    public void OnMoverArrived()
    {
        isArrive = true;
    }

    public override TaskStatus OnUpdate()
    {
        return isArrive ? TaskStatus.Success : TaskStatus.Running;
    }
}