using BehaviourTree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class WolfBT : Tree
{
    [SerializeField]
    Animator _animator;
    [SerializeField]
    AudioSource _wolfAudio;
    [SerializeField]
    AudioClip _wolfHowl;
    [SerializeField]
    private NavMeshAgent _agent;
    [SerializeField]
    private Transform[] _waypoints;
    private List<Vector3> _navMeshWaypoints = new List<Vector3>();
    [SerializeField]
    private string _preyLayerMask = "";
    [SerializeField]
    private float _fovRadius = 6f;
    [SerializeField]
    private float _attackRange = 4f;
    [SerializeField]
    private float _walkSpeed = 2f;
    [SerializeField]
    private float _runSpeed = 6f;
    [SerializeField]
    private float _patrolWaitTime = 3f;
    [SerializeField]
    private float _attackDelay = 1f;
    [SerializeField]
    private int _damage = 1;
    protected override Node SetupTree()
    {
        foreach(var waypoint in _waypoints)
        {
            NavMeshHit hit;
            if (NavMesh.SamplePosition(waypoint.position, out hit, 1000f, -1)){
                _navMeshWaypoints.Add(hit.position);
            }
        }

        Node root = new Selector(new List<Node>
        {
            new Sequence(new List<Node>
            {
                new CheckEnemyInAttackRange(transform, _animator, _attackRange),
                new TaskAttack(_attackDelay, _animator, _damage),
            }),
            new Sequence(new List<Node>
            {
                new CheckEnemyInFOV(transform, _fovRadius, _preyLayerMask, _wolfAudio, _wolfHowl),
                new TaskGoToTarget(transform, _agent, _runSpeed, _animator),
            }),
            new TaskPatrol(transform, _navMeshWaypoints.ToArray(), _agent, _walkSpeed, _animator, _patrolWaitTime),
        });

        return root;
    }

    private void OnDrawGizmosSelected()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _fovRadius);
    }
}
