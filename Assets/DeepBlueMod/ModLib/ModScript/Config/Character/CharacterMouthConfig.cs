using UnityEngine;

namespace kz.config
{
    [CreateAssetMenu(fileName = "CharacterMouthConfig", menuName = "ScriptableObject/角色嘴巴配置文件", order = 20)]
    public class CharacterMouthConfig:ScriptableObject
    {
        public float initialMouthSize = 1f;
        // public float mouthSizeGrowRatio;
        public float eatFoodDuration = 0.5f;
        
        public float initialSuckRadius;
        public float suckRadiusGrowRatio;
    }
}