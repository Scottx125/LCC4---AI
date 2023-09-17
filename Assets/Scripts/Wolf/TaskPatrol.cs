using BehaviourTree;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class TaskPatrol : Node
{
    private Transform _transform;
    private Vector3[] _waypoints;
    private NavMeshAgent _agent;
    private float _walkSpeed;
    private Animator _animator;

    private int _currentWaypointIndex = 0;
    private float _waitTime = 5f;
    private float _waitCounter = 0f;
    private bool _waiting = false;
    public TaskPatrol(Transform transform, Vector3[] waypoints, NavMeshAgent agent, float walkSpeed, Animator animator, float patrolWaitTime)
    {
        _transform = transform;
        _waypoints = waypoints;
        _agent = agent;
        _walkSpeed = walkSpeed;
        _animator = animator;
        _waitTime = patrolWaitTime;
    }

    public override NodeState Evaluate()
    {
        if (_waiting)
        {
            _waitCounter += Time.deltaTime;
            if (_waitCounter >= _waitTime) 
            { 
                _waiting = false;
                Debug.Log("WAITING");
            }

        }else {
            Vector3 waypoint = _waypoints[_currentWaypointIndex];
            if (Vector3.Distance(_transform.position, waypoint) <= _agent.stoppingDistance)
            {
                Debug.Log("STOP");
                _waitCounter = 0f;
                _waiting = true;
                _animator.SetBool("Walking", false);
                _currentWaypointIndex = (_currentWaypointIndex + 1) % _waypoints.Length;
            }
            else
            {
                Debug.Log("WALKING");
                _agent.speed = _walkSpeed;
                _animator.SetBool("Walking", true);
                _agent.destination = waypoint;
            }
        }



        state = NodeState.RUNNING;
        return state;
    }
}
