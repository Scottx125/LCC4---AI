using BehaviourTree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CheckEnemyInAttackRange : Node
{
    private Transform _transform;
    private Animator _animator;
    private float _attackRange;
    public CheckEnemyInAttackRange(Transform transform, Animator animator, float attackRange)
    {
        _transform = transform;
        _animator = animator;
        _attackRange = attackRange;
    }

    public override NodeState Evaluate()
    {
        object t = GetData("target");

        if (t == null)
        {
            state = NodeState.FAILURE;
            return state;
            Debug.Log("ATTACK RANGE FAILURE");
        }

        Transform target = (Transform)t;
        if (Vector3.Distance(_transform.position, target.position) <= _attackRange)
        {
            _animator.SetBool("Attacking", true);
            _animator.SetBool("Walking", false);
            _animator.SetBool("Running", false);
            state = NodeState.SUCCESS;
            return state;
            Debug.Log("IN ATTACK RANGE");
        }


        state = NodeState.FAILURE;
        return state;
    }
}
