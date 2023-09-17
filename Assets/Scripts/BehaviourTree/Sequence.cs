using BehaviourTree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sequence : Node
{
    public Sequence() : base() {}
    public Sequence(List<Node> children) : base (children) { }

    public override NodeState Evaluate()
    {
        bool anyChildIsRunning = false;
        // Check the state of each child.
        // If something fails everything stops, is we succeed keep going.
        // If we a child is running set running to true.
        // This does everything in sequence.
        foreach (Node node in children)
        {
            switch (node.Evaluate())
            {
                case NodeState.FAILURE:
                    state = NodeState.FAILURE;
                    return state;
                case NodeState.SUCCESS:
                    continue;
                case NodeState.RUNNING:
                    anyChildIsRunning = true;
                    continue;
                default:
                    state = NodeState.SUCCESS;
                    return state;
            }
        }
        state = anyChildIsRunning ? NodeState.RUNNING : NodeState.SUCCESS;
        return state;
    }
}
