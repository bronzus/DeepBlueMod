using System;
using MBT;
using System.Collections.Generic;
using Mirror;
using UnityEngine;
using UnityEngine.Serialization;

namespace kz
{
    [MBTNode("fishAI/Detect Enemies Service")]
    public class DetectEnemiesService:Service
    {
        public bool detectEnemyToActiveAttack = true;
        public bool detectEnemyToPassiveAttack = true;
        public bool detectEnemyToFlee = true;
        
        public GameObjectReference enemyToFlee = new GameObjectReference(VarRefMode.DisableConstant);
        public GameObjectReference enemyToActiveAttack = new GameObjectReference(VarRefMode.DisableConstant);
        public GameObjectReference enemyToPassiveAttack = new GameObjectReference(VarRefMode.DisableConstant);
        public BoolReference findEnemyToActiveAttack = new BoolReference(VarRefMode.EnableConstant);
        public FloatReference patienceTimer = new FloatReference(VarRefMode.DisableConstant);
        
        public override void Task()
        {
        }
    }
}