using BehaviourTree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPreyInRange : Node
{
    private Transform _transform;
    private float _fovRadius = 1;
    private LayerMask _preyLayerMask;
    public CheckPreyInRange(Transform transform, float fovRadius, string preyLayerMask)
    {
        _transform = transform;
        _fovRadius = fovRadius;
        _preyLayerMask = LayerMask.GetMask(preyLayerMask);
    }

    public override NodeState Evaluate()
    {
        object t = GetData("target");

        if (t == null)
        {
            Collider[] colliders = Physics.OverlapSphere(_transform.position, _fovRadius, _preyLayerMask);
            if (colliders.Length > 0)
            {
                parent.parent.SetData("target", colliders[0].transform);
                state = NodeState.SUCCESS;
                return state;
            }
            state = NodeState.FAILURE;
            return state;
        }
        state = NodeState.SUCCESS;
        return state;
    }
}
