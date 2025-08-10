using UnityEngine;


namespace kz.config
{
    [CreateAssetMenu(fileName = "CharacterGrowConfig", menuName = "ScriptableObject/角色成长配置文件", order = 6)]
    public class CharacterGrowConfig:ScriptableObject
    {
        [Tooltip("大小的成长系数")]
        public float scaleGrowRatio = 0.1f;
        [Tooltip("最大生命值的成长系数")]
        public float maxHpGrowRatio = 0.17f;
        [Tooltip("攻击力的成长系数")]
        public float attackPowerGrowRatio = 0.16f;
        [Tooltip("速度的成长系数")]
        public float speedGrowRatio = 0.03f;
        [Tooltip("加速系数的成长系数")]
        public float sprintFactorGrowRatio = -0.01f;
        [Tooltip("转弯速度的成长系数")]
        public float turnSmoothingGrowRatio = -0.01f;
        [Tooltip("AI相关，观察半径的成长系数")]
        public float detectionRadiusGrowRatio = 0.08f;
        
        [Tooltip("AI相关，触发主动攻击最小的半径的成长系数")]
        public float triggerActiveAttackMinRadiusGrowRatio = 0.08f;
    }
}