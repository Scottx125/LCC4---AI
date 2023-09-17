using BehaviourTree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CheckEnemyInAttackRange : Node
{
    private Transform _transform;
    private Animator _animator;
    private LayerMask _preyLayerMask;
    private float _attackRange;
    public CheckEnemyInAttackRange(Transform transform, Animator animator, string preyLayerMask, float attackRange)
    {
        _transform = transform;
        _animator = animator;
        _preyLayerMask = LayerMask.GetMask(preyLayerMask);
        _attackRange = attackRange;
    }

    public override NodeState Evaluate()
    {
        object t = GetData("target");

        if (t == null)
        {
            state = NodeState.FAILURE;
            return state;
        }

        Transform target = (Transform)t;
        if (Vector3.Distance(_transform.position, target.position) <= _attackRange)
        {
            _animator.SetBool("Attacking", true);
            _animator.SetBool("Walking", false);
            _animator.SetBool("Running", false);
            state = NodeState.SUCCESS;
            return state;
        }


        state = NodeState.FAILURE;
        return state;
    }
}
