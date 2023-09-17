using System.Collections;
using System.Collections.Generic;

namespace BehaviourTree
{
    public enum NodeState
    {
        RUNNING,
        SUCCESS,
        FAILURE
    }
    public class Node
    {
        public Node parent;

        protected NodeState state;
        protected List<Node> children = new List<Node>();

        // For shared data.
        private Dictionary<string, object> _dataContext = new Dictionary<string, object>();

        public virtual NodeState Evaluate() => NodeState.FAILURE;

        public Node()
        {
            parent = null;
        }

        public Node(List<Node> children)
        {
            foreach (Node child in children)
            {
                Attatch(child);
            }
        }

        public void SetData(string key, object value)
        {
            _dataContext[key] = value;
        }

        public object GetData(string key)
        {
            // Search through not only the children but the entire tree for the data we're looking for.
            object value = null;
            if (_dataContext.TryGetValue(key, out value))
            {
                return value;
            }
            // This is recursive until we eventually return null if we can't
            // find the data via the key.
            Node node = parent;
            while (node != null)
            {
                value = node.GetData(key);
                if (value != null)
                {
                    return value;
                }
                node = node.parent;
            }
            return null;
        }

        public bool ClearData(string key)
        {
            // Search through not only the children but the entire tree for the data we're looking for.
            object value = null;
            if (_dataContext.ContainsKey(key))
            {
                _dataContext.Remove(key);
                return true;
            }
            // This is recursive until we eventually return null if we can't
            // find the data via the key.
            Node node = parent;
            while (node != null)
            {
                bool cleared = node.ClearData(key);
                if (cleared)
                {
                    return true;
                }
                node = node.parent;
            }
            return false;
        }

        private void Attatch(Node node)
        {
            // Whenever a child calls to attatch to this node.
            // set this as it's parent and add the node to children.
            node.parent = this;
            children.Add(node);
        }


    }
}

