using NUnit.Framework;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class EnemyStateMachine : StateMachine
{
    [field: SerializeField] public Transform Transform { get; private set; }

    [field: SerializeField] public Transform[] PlayersTransform { get; private set; }

    [field: SerializeField] public Animator Animator { get; private set; }

    [field: SerializeField] public NavMeshAgent NavMeshAgent { get; private set; }

    [field: SerializeField] public LayerMask PlayerLayerMask { get; private set; }

    private void Awake()
    {
        //MainPlayerTransform = GameObject.FindGameObjectWithTag("MainPlayer").transform;

    }
    private void Start()
    {
        SwitchState(new EnemyPatrolingState(this));
    }

}
