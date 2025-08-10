using UnityEngine;

namespace kz.config
{
    [CreateAssetMenu(fileName = "CharacterSwimBehaviourConfig", menuName = "ScriptableObject/CharacterSwimBehaviourConfig", order = 40)]
    public class CharacterSwimBehaviourConfig:ScriptableObject
    {
        public bool hasOutWaterDamage = true; // 是否有离水伤害
        public float outWaterDamageRatio = 0.2f; // 离水伤害的比例
        public float outWaterDamageInterval = 1f; // 离水伤害的间隔
        public float outWaterDamageDelay = 10f; // 离水伤害的延迟

        public float maxCanJumpBodyAngel = 180f; 
        public float jumpAngel = 45f;
        public float initialJumpVelocitySize = 3f;
        public float jumpVelocitySizeGrowRatio = 0.2f;
    }
}