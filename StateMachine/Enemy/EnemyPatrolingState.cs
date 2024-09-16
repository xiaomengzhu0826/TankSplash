
using Autodesk.Fbx;
using UnityEngine;
using UnityEngine.AI;

public class EnemyPatrolingState : EnemyBaseState
{
    private float _SightRange=2f;
    private float _WalkPointRange=30f;
    private bool _InSightRange, _WalkPointSet;
    private NavMeshHit _Hit;
    private float _Timer;
    private float _MaxTime=10f;
    public EnemyPatrolingState(EnemyStateMachine stateMachine) : base(stateMachine)
    {

    }

    public override void Enter()
    {
        Debug.Log("Enemy Patroling State");
    }

    public override void Exit()
    {
        
    }

    public override void FixedTick(float fixedDeltaTime)
    {
        
    }

    public override void Tick(float deltaTime)
    {
        //_InSightRange = Physics.CheckSphere(stateMachine.MainPlayerTransform.position, _SightRange, stateMachine.PlayerLayerMask);
        //if (!_InSightRange )
        //{
        //    Patroling();
        //}
        Patroling();
    }

    private void Patroling()
    {
        if (!_WalkPointSet)
        {
            _Timer = 0;
            SetRandomDestination();
        }
        if (_WalkPointSet)
        {
            float distanceToDestination = (stateMachine.Transform.position - _Hit.position).sqrMagnitude;
            if (distanceToDestination < 2f)
            {
                _WalkPointSet = false;
            }
            _Timer += Time.deltaTime;  
            if (_Timer >= _MaxTime)
            {
                _WalkPointSet = false;
            }
        }
    }

    void SetRandomDestination()
    {
        // 随机生成一个在指定范围内的位置
        //Vector3 randomDirection = Random.insideUnitSphere * 10f;
        //randomDirection += stateMachine.Transform.position;
        float randomZ = Random.Range(-_WalkPointRange, _WalkPointRange);
        float randomX = Random.Range(-_WalkPointRange, _WalkPointRange);
        Vector3 randomDirection = new Vector3(stateMachine.Transform.position.x + randomX, stateMachine.Transform.position.y, stateMachine.Transform.position.z + randomZ);

        // 确保生成的位置在NavMesh上
        if (NavMesh.SamplePosition(randomDirection, out _Hit, 10f, NavMesh.AllAreas))
        {
            _WalkPointSet=true;
            stateMachine.NavMeshAgent.SetDestination(_Hit.position);   
        }
    }
}
