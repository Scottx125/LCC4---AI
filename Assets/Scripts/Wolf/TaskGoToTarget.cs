using BehaviourTree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TaskGoToTarget : Node
{
    private Transform _transform;
    private NavMeshAgent _agent;
    private float _runSpeed;
    private Animator _animator;
    private Vector3 _targetPosition;
    public TaskGoToTarget(Transform transform, NavMeshAgent agent, float runSpeed, Animator animator)
    {
        _transform = transform;
        _agent = agent;
        _runSpeed = runSpeed;
        _animator = animator;
    }

    public override NodeState Evaluate()
    {
        Transform target = (Transform)GetData("target");

        if (Vector3.Distance(_transform.position, target.position) > 0.01f)
        {
            NavMeshHit hit;
            if (NavMesh.SamplePosition(target.position, out hit, 1000f, -1))
            {
                _agent.speed = _runSpeed;
                _animator.SetBool("Running", true);
                _agent.destination = hit.position;
            }
        }

        state = NodeState.RUNNING;
        return state;
    }
}
