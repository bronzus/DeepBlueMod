using UnityEngine;

namespace kz.config
{
    [CreateAssetMenu(fileName = "CharacterSwimBehaviourConfig", menuName = "ScriptableObject/CharacterSwimBehaviourConfig", order = 40)]
    public class CharacterSwimBehaviourConfig:ScriptableObject
    {
        public bool outWaterCanSwim = false;
        [Tooltip("是否有离水伤害")]
        public bool hasOutWaterDamage = true;
        [Tooltip("离水伤害的比例")]
        public float outWaterDamageRatio = 0.2f;
        [Tooltip("离水伤害的间隔")]
        public float outWaterDamageInterval = 1f;
        [Tooltip("离水伤害的延迟")]
        public float outWaterDamageDelay = 10f;

        [Tooltip("最大可以跳跃的身体角度")]
        public float maxCanJumpBodyAngel = 180f; 
        [Tooltip("跳跃角度")]
        public float jumpAngel = 45f;
        [Tooltip("初始的跳跃速度大小")]
        public float initialJumpVelocitySize = 3f;
        [Tooltip("跳跃速度大小成长系数")]
        public float jumpVelocitySizeGrowRatio = 0.2f;
    }
}