using System;
using MBT;

namespace kz
{
    [MBTNode("fishAI/RestorePatienceService")]
    public class RestorePatienceService:Service
    {
        public FloatReference patienceTimer = new FloatReference(VarRefMode.DisableConstant);
        public BoolReference findEnemyToActiveAttack = new BoolReference(VarRefMode.DisableConstant);
        
        public override void Task()
        {
        }
    }
}