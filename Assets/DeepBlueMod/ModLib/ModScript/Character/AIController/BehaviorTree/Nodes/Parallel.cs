using MBT;

namespace kz
{
    [MBTNode(name = "Parallel")]
    public class Parallel:Composite
    {
        public int minimalSuccessfulNodeCount = 1;
        
        private int index;
        private int successCount;
        
        public override void OnAllowInterrupt()
        {
            if (random)
            {
                ShuffleList(children);
            }
        }
        
        public override void OnEnter()
        {
            index = 0;
            successCount = 0;
        }

        public override void OnBehaviourTreeAbort()
        {
            // Do not continue from last index
            index = 0;
            successCount = 0;
        }

        public override NodeResult Execute()
        {
            while (index < children.Count)
            {
                Node child = children[index];
                switch (child.status)
                {
                    case Status.Success:
                        index += 1;
                        successCount += 1;
                        continue;
                    case Status.Failure:
                        index += 1;
                        continue;
                }
                return child.runningNodeResult;
            }
            if (this.successCount >= minimalSuccessfulNodeCount)
                return NodeResult.success;
            return NodeResult.failure;
        }
    }
}