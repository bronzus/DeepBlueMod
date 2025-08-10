using System;
using MBT;
using UnityEngine;
using System.Collections.Generic;

namespace kz
{
    [MBTNode("fishAI/CommonUseSkillAIAction")]
    public class CommonUseSkillAIAction:Leaf
    {
        public GameObjectReference enemy = new GameObjectReference(VarRefMode.DisableConstant);
        public int useSkillIndex = 0;
        
        public override NodeResult Execute()
        {
            return NodeResult.success;
        }
    }
}