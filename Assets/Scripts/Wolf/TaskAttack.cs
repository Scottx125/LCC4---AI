using BehaviourTree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskAttack : Node
{
    private Transform _lastTarget;
    private Animator _animator;
    private EnemyManager _enemyManager;
    private float _attackDelay = 1f;
    private float _attackCounter = 0f;
    private int _damage = 1;
    public TaskAttack(float attackDelay, Animator animator, int damage)
    {
        _attackDelay = attackDelay;
        _animator = animator;
        _damage = damage;
    }

    public override NodeState Evaluate()
    {
        Transform target = (Transform)GetData("target");
        if (target != _lastTarget)
        {
            _enemyManager = target.GetComponent<EnemyManager>();
            _lastTarget = target;
        }

        _attackCounter += Time.deltaTime;
        if (_attackCounter >= _attackDelay)
        {
            _animator.SetFloat("AttackingAnim", (float)Random.Range(0,1));
            bool enemyIsDead = _enemyManager.TakeHit(_damage);
            if (enemyIsDead)
            {
                ClearData("target");
                _animator.SetBool("Walking", true);
                _animator.SetBool("Attacking", false);

            } else
            {
                _attackCounter = 0f;
            }
        }

        state = NodeState.RUNNING;
        return state;
    }
}
