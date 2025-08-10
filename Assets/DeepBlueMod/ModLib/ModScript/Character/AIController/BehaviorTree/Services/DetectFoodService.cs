using System;
using MBT;
using System.Collections.Generic;
using UnityEngine;

namespace kz
{
    [MBTNode("fishAI/Detect Food Service")]
    public class DetectFoodService:Service
    {
        public GameObjectReference foodToSet = new GameObjectReference(VarRefMode.DisableConstant);
        public BoolReference spiltFoodToSet = new BoolReference(VarRefMode.DisableConstant);
        public bool detectDirectlyEatCharacter = true;
        public bool detectRealFood = true;
        public bool detectFakeFood = true;
        
        public override void Task()
        {
        }
    }
}