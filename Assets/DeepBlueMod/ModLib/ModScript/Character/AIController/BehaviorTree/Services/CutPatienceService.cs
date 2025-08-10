using System;
using MBT;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace kz
{
    [MBTNode("fishAI/CutPatienceService")]
    public class CutPatienceService:Service
    {
        public FloatReference patienceTimer = new FloatReference(VarRefMode.DisableConstant);
        public GameObjectReference enemy = new GameObjectReference(VarRefMode.DisableConstant);
        

        public override void Task()
        {
        }
    }
}