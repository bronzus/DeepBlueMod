using System;
using UnityEngine;

namespace kz.config
{
    [CreateAssetMenu(fileName = "CharacterAiConfig", menuName = "ScriptableObject/角色AI配置文件", order = 5)]
    public class CharacterAiConfig:ScriptableObject
    {
        [Serializable]
        public class BoidsConfig
        {
            public Vector2 flockMatesAlignWeightRange = new Vector2(0.9f, 1f);
            public Vector2 flockMatesCohesionWeightRange = new Vector2(0.9f, 1f);
            public Vector2 flockMatesSeperateWeightRange = new Vector2(0.9f, 1f);
        }
        public BoidsConfig boidsConfig = new BoidsConfig();
            
        [Tooltip("初始的鱼类的观察半径")]
        public float initialDetectionRadius = 10f;
        [Tooltip("主动攻击的观察视角")]
        public float activeAttackViewAngle = 120f;
        [Tooltip("主动攻击的最小时间，即便被攻击者已经在主动攻击的范围之外，也要满足这个最小时间的要求")]
        public float activeAttackMinTime = 3f;
        [Tooltip("初始触发主动攻击最小的半径")]
        public float initialTriggerActiveAttackMinRadius;
        [Tooltip("初始触发主动攻击最大等级差系数")]
        public float triggerActiveAttackMaxLevelDiffRatio = 0.5f;
        
        [Tooltip("额外的逃跑距离， 逃跑的距离 = detectionRadius +  extraFleeDistance")]
        public float extraFleeDistance = 10f;
        [Tooltip("额外的主动攻击的距离，追击敌人的距离为 = detectionRadius + extraActiveAttackDistance")]
        public float extraActiveAttackDistance = 5f;
        [Tooltip("攻击的耐心，失去耐心就不会去攻击")]
        public float attackPatienceSeconds = 5f;
        [Tooltip("普通攻击的CD")]
        public float normalAttackCD = 1.5f;
    }
}