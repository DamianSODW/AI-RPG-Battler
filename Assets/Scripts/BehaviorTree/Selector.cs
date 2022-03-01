using System.Collections.Generic;

namespace BehaviorTree
{
    public class Selector : Node
    {
        #region Constructors
        public Selector() : base() { }
        public Selector(List<Node> children) : base(children) { }
        #endregion
        public override NodeState Evaluate()
        {
            foreach (Node child in children)
            {
                switch (child.Evaluate())
                {
                    case NodeState.Failure:
                        continue;
                    case NodeState.Success:
                        state = NodeState.Success;
                        return state;
                    case NodeState.Running:
                        state = NodeState.Running;
                        return state;
                    default:
                        continue;
                }
            }

            state = NodeState.Failure;
            return state;
        }

    }

}
