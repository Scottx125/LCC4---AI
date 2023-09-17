using BehaviourTree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WolfBT : Tree
{
    [SerializeField]
    Animator _animator;
    [SerializeField]
    private NavMeshAgent _agent;
    [SerializeField]
    private Transform[] _waypoints;
    private List<Vector3> _navMeshWaypoints = new List<Vector3>();
    [SerializeField]
    private string _preyLayerMask = "";
    [SerializeField]
    private float _fovRange = 6f;
    [SerializeField]
    private float _walkSpeed = 2f;
    [SerializeField]
    private float _runSpeed = 6f;
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
                new CheckPreyInRange(transform, _fovRange, _preyLayerMask),
                new TaskGoToTarget(transform, _agent, _runSpeed, _animator),
            }),
            new TaskPatrol(transform, _navMeshWaypoints.ToArray(), _agent, _walkSpeed, _animator),
        });

        return root;
    }
}