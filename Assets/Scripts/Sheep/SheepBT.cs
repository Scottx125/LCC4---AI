using BehaviourTree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class SheepBT : Tree
{
    [SerializeField]
    Animator _animator;
    [SerializeField]
    private NavMeshAgent _agent;
    [SerializeField]
    private Transform[] _waypoints;
    private List<Vector3> _navMeshWaypoints = new List<Vector3>();
    [SerializeField]
    private float _walkSpeed = 2f;
    [SerializeField]
    private float _patrolWaitTime = 3f;
    protected override Node SetupTree()
    {
        foreach(var waypoint in _waypoints)
        {
            NavMeshHit hit;
            if (NavMesh.SamplePosition(waypoint.position, out hit, 1000f, -1)){
                _navMeshWaypoints.Add(hit.position);
            }
        }
        for(int i = 0; i < _navMeshWaypoints.Count; i++)
        {
            Vector3 temp = _navMeshWaypoints[i];
            int rand = Random.Range(i, _navMeshWaypoints.Count);
            _navMeshWaypoints[i] = _navMeshWaypoints[rand];
            _navMeshWaypoints[rand] = temp;
        }

        Node root = new Selector(new List<Node>
        {
            new TaskPatrol(transform, _navMeshWaypoints.ToArray(), _agent, _walkSpeed, _animator, _patrolWaitTime),
        });

        return root;
    }
}
