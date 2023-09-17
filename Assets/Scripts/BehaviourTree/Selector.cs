using BehaviourTree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.VisualScripting.Metadata;

public class Selector : Node
{
    public Selector() : base() { }
    public Selector(List<Node> children) : base(children) { }

    public override NodeState Evaluate()
    {
        // Check the state of each child.
        // If child has succeeded or is running. Return. This is like an or gate.
        foreach (Node node in children)
        {
            switch (node.Evaluate())
            {
                case NodeState.FAILURE:
                    state = NodeState.FAILURE;
                    continue;
                case NodeState.SUCCESS:
                    state = NodeState.SUCCESS;
                    return state;
                case NodeState.RUNNING:
                    state = NodeState.RUNNING;
                    return state;
                default:
                    continue;
            }
        }
        state = NodeState.FAILURE;
        return state;
    }
}
