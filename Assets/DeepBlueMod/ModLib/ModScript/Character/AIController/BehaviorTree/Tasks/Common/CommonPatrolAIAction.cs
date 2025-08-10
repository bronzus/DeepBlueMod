using MBT;
using UnityEngine;
using System.Collections.Generic;

namespace kz
{
    [MBTNode("fishAI/CommonPatrolAIAction")]
    public class CommonPatrolAIAction:Leaf
    {
        public override NodeResult Execute()
        {
            return NodeResult.success;
        }
        
    }
}