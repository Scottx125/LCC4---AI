using BehaviourTree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckEnemyInFOV : Node
{
    private Transform _transform;
    private float _fovRadius = 1;
    private LayerMask _preyLayerMask;
    private AudioSource _wolfAudio;
    private AudioClip _wolfHowl;
    public CheckEnemyInFOV(Transform transform, float fovRadius, string preyLayerMask, AudioSource wolfAudio, AudioClip wolfHowl)
    {
        _transform = transform;
        _fovRadius = fovRadius;
        _preyLayerMask = LayerMask.GetMask(preyLayerMask);
        _wolfAudio = wolfAudio;
        _wolfHowl = wolfHowl;
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
                _wolfAudio.PlayOneShot(_wolfHowl);
                return state;
            }
            state = NodeState.FAILURE;
            return state;
        }
        state = NodeState.SUCCESS;
        return state;
    }
}
