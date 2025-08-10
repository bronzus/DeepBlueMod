using System;
using MBT;
using System.Collections.Generic;
using kz.config;
using UnityEngine;

namespace kz
{
    [MBTNode("fishAI/CommonDetectSkillUseService")]
    public class CommonDetectSkillUseService:Service
    {
        public GameObjectReference enemy = new GameObjectReference(VarRefMode.DisableConstant);
        public BoolReference useSkill = new BoolReference(VarRefMode.DisableConstant);
        public int skillIndex;
        public DefaultAISkillUseConfig skillUseConfig = null;

        public override void Task()
        {
        }
    }
}